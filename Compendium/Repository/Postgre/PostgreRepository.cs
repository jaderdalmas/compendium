using Compendium.Notify;
using Compendium.Properties;
using Compendium.Setting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Data;

namespace Compendium.Repository.Postgre
{
  public abstract class PostgreRepository
  {
    private readonly string _cnn;
    private readonly INotification _notification;

    public PostgreRepository(IOptions<PostgreSetting> postgreSettings, INotification notification)
    {
      if (notification != null) _notification = notification;

      if (postgreSettings?.Value?.GetBuilder != null)
        _cnn = postgreSettings.Value.GetBuilder.ConnectionString;
      else if (_notification != null)
        _notification.AddNotification(Resources.PostGreSettingMissing, "ArgumentNull", LogLevel.Error);
    }

    protected IDbConnection GetConnection()
    {
      try
      {
        return new NpgsqlConnection(_cnn);
      }
      catch (NpgsqlException e)
      {
        if (_notification != null)
          _notification.AddNotification(e.Message, "PostGre connection error.", LogLevel.Error);
        throw;
      }
      catch (InvalidOperationException e)
      {
        if (_notification != null)
          _notification.AddNotification(e.Message, "PostGre connection error. Connection already open?", LogLevel.Error);
        throw;
      }
      catch (Exception e)
      {
        if (_notification != null)
          _notification.AddNotification(e.Message, "General Exception while connecting to PostGre.", LogLevel.Error);
        throw;
      }
    }
  }
}
