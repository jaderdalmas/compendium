using Compendium.Extension;
using Compendium.Notify;
using Compendium.Repository.Interfaces;
using Compendium.Repository.Model;
using Compendium.Setting;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compendium.Repository.Postgre
{
  public class TableRepository : PostgreRepository, ITableRepository
  {
    public TableRepository(IOptions<PostgreSetting> postgreSettings, INotification notification) : base(postgreSettings, notification) { }

    private readonly string deleteQuery = $"Delete From {TableEntity.tableQuery} Where {TableEntity.keyQuery};";
    public async Task<bool> Delete(long id)
    {
      using var cnn = GetConnection();
      return await cnn.ExecuteAsync(deleteQuery, new { Id = id }, commandTimeout: cnn.ConnectionTimeout).ConfigureAwait(false) == 1;
    }

    public async Task<bool> Delete(IEnumerable<long> ids)
    {
      StringBuilder sb = new StringBuilder();
      foreach (long id in ids.AsParallel())
        sb.AppendLine(deleteQuery.Replace("@Id", $"{id}", StringComparison.InvariantCulture));

      using var cnn = GetConnection();
      return await cnn.ExecuteAsync(sb.ToString(), commandTimeout: cnn.ConnectionTimeout).ConfigureAwait(false) == ids.Count();
    }

    public async Task<bool> Exists(long id)
    {
      var query = $"Select 1 From {TableEntity.tableQuery} Where {TableEntity.keyQuery};";

      using var cnn = GetConnection();
      return await cnn.ExecuteAsync(query, new { Id = id }, commandTimeout: cnn.ConnectionTimeout).ConfigureAwait(false) == 1;
    }

    private readonly string getQuery = $"Select {TableEntity.fieldsQuery} From {TableEntity.tableQuery}";
    public async Task<TableEntity> GetOne(long id)
    {
      var query = $"{getQuery} Where {TableEntity.keyQuery};";

      using var cnn = GetConnection();
      return await cnn.QueryFirstOrDefaultAsync<TableEntity>(query, new { Id = id }, commandTimeout: cnn.ConnectionTimeout).ConfigureAwait(false);
    }

    public async Task<IEnumerable<TableEntity>> GetAll(string tablename)
    {
      var query = $"{getQuery} Where cname like @TableName {TableEntity.orderQuery};";

      using var cnn = GetConnection();
      return await cnn.QueryAsync<TableEntity>(query, new { TableName = tablename.LikeParam() }, commandTimeout: cnn.ConnectionTimeout).ConfigureAwait(false);
    }

    public async Task<IEnumerable<TableEntity>> GetAll()
    {
      var query = $"{getQuery} {TableEntity.orderQuery};";

      using var cnn = GetConnection();
      return await cnn.QueryAsync<TableEntity>(query, commandTimeout: cnn.ConnectionTimeout).ConfigureAwait(false);
    }

    private readonly string insertQuery = $"INSERT INTO {TableEntity.tableQuery}({TableEntity.insertQuery}) VALUES({TableEntity.valuesQuery});";
    public async Task<long> Insert(TableEntity entity)
    {
      using var cnn = GetConnection();
      return await cnn.ExecuteAsync(insertQuery, entity, commandTimeout: cnn.ConnectionTimeout).ConfigureAwait(false);
    }

    public async Task<bool> Insert(IEnumerable<TableEntity> entities)
    {
      StringBuilder sb = new StringBuilder();
      foreach (TableExternalEntity entity in entities.AsParallel())
        sb.AppendLine(entity.FillQuery(insertQuery));

      using var cnn = GetConnection();
      return await cnn.ExecuteAsync(sb.ToString(), commandTimeout: cnn.ConnectionTimeout).ConfigureAwait(false) == entities.Count();
    }

    private readonly string updateQuery = $"UPDATE {TableEntity.tableQuery} SET @Params WHERE @Where;";
    public async Task<bool> Update(TableEntity entity)
    {
      var query = updateQuery.Replace("@Params", "cdescription=@Description", StringComparison.InvariantCulture);
      query = query.Replace("@Where", TableEntity.keyQuery, StringComparison.InvariantCulture);

      using var cnn = GetConnection();
      return await cnn.ExecuteAsync(query, entity, commandTimeout: cnn.ConnectionTimeout).ConfigureAwait(false) == 1;
    }

    public async Task<bool> Update(IEnumerable<TableExternalEntity> entities)
    {
      var query = updateQuery.Replace("@Params", $"{TableEntity.updateQuery}", StringComparison.InvariantCulture);
      query = query.Replace("@Where", TableEntity.syncQuery, StringComparison.InvariantCulture);

      StringBuilder sb = new StringBuilder();
      foreach (TableExternalEntity entity in entities.AsParallel())
        sb.AppendLine(entity.FillQuery(query));

      using var cnn = GetConnection();
      return await cnn.ExecuteAsync(sb.ToString(), commandTimeout: cnn.ConnectionTimeout).ConfigureAwait(false) == entities.Count();
    }
  }
}