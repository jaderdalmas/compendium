using Compendium.Notify;
using Compendium.Properties;
using Compendium.Setting;
using IBM.Data.DB2.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Data;

namespace Compendium.Repository.DB2
{
  public abstract class DB2Repository
  {
    private readonly string _cnn;
    private readonly INotification _notification;

    public DB2Repository(IOptions<DB2Setting> db2Settings, INotification notification)
    {
      if (notification != null) _notification = notification;

      if (db2Settings?.Value?.GetBuilder != null)
        _cnn = db2Settings.Value.GetBuilder.ConnectionString;
      else if (_notification != null)
        _notification.AddNotification(Resources.DB2SettingMissing, "ArgumentNull", LogLevel.Error);
    }

    protected IDbConnection GetConnection()
    {
      try
      {
        return new DB2Connection(_cnn);
      }
      catch (DB2Exception e)
      {
        if (_notification != null)
          _notification.AddNotification(e.Message, "DB2 connection error.", LogLevel.Error);
        throw;
      }
      catch (InvalidOperationException e)
      {
        if (_notification != null)
          _notification.AddNotification(e.Message, "DB2 connection error. Connection already open?", LogLevel.Error);
        throw;
      }
      catch (Exception e)
      {
        if (_notification != null)
          _notification.AddNotification(e.Message, "General Exception while connecting to DB2.", LogLevel.Error);
        throw;
      }
    }
  }
}
