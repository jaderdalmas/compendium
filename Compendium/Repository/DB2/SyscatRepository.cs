using Compendium.Notify;
using Compendium.Repository.Interfaces;
using Compendium.Repository.Model;
using Compendium.Setting;
using Dapper;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Compendium.Repository.DB2
{
  public class SyscatRepository : DB2Repository, ISyscatRepository
  {
    public SyscatRepository(IOptions<DB2Setting> db2Options, INotification notification) : base(db2Options, notification) { }

    public Task<IEnumerable<SyscatColumn>> GetColumn()
    {
      var query = "SELECT TabName, ColName, ColNo, TypeName, Length, Default, Nulls, Identity FROM SYSCAT.COLUMNS WHERE TabSchema='TMWIN' ORDER BY 1,3";

      using var cnn = GetConnection();
      return cnn.QueryAsync<SyscatColumn>(query, commandTimeout: cnn.ConnectionTimeout);
    }

    public Task<IEnumerable<SyscatTable>> GetTable()
    {
      var query = "SELECT TableId, TabName, Type, ColCount FROM SYSCAT.TABLES WHERE TabSchema='TMWIN' and Type = 'T' ORDER BY 2";

      using var cnn = GetConnection();
      return cnn.QueryAsync<SyscatTable>(query, commandTimeout: cnn.ConnectionTimeout);
    }
  }
}
