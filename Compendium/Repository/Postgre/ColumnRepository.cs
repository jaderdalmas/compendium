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
  public class ColumnRepository : PostgreRepository, IColumnRepository
  {
    public ColumnRepository(IOptions<PostgreSetting> postgreSettings, INotification notification) : base(postgreSettings, notification) { }

    private readonly string deleteQuery = $"Delete From {ColumnEntity.tableQuery} Where {ColumnEntity.keyQuery};";
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
      var query = $"Select 1 From {ColumnEntity.tableQuery} Where {ColumnEntity.keyQuery};";

      using var cnn = GetConnection();
      return await cnn.ExecuteAsync(query, new { Id = id }, commandTimeout: cnn.ConnectionTimeout).ConfigureAwait(false) == 1;
    }

    private readonly string getQuery = $"Select {ColumnEntity.fieldsQuery} From {ColumnEntity.tableQuery}";
    public async Task<ColumnEntity> GetOne(long id)
    {
      var query = $"{getQuery} Where {ColumnEntity.keyQuery};";

      using var cnn = GetConnection();
      return await cnn.QueryFirstOrDefaultAsync<ColumnEntity>(query, new { Id = id }, commandTimeout: cnn.ConnectionTimeout).ConfigureAwait(false);
    }

    public async Task<IEnumerable<ColumnEntity>> GetAll(string tableName, string name)
    {
      var filter = string.IsNullOrWhiteSpace(name) ? "ctable = @TableName" : "cname like @Name";
      var query = $"{getQuery} Where {filter} {ColumnEntity.orderQuery};";

      using var cnn = GetConnection();
      return await cnn.QueryAsync<ColumnEntity>(query, new { TableName = tableName, Name = name.LikeParam() }, commandTimeout: cnn.ConnectionTimeout).ConfigureAwait(false);
    }

    public async Task<IEnumerable<ColumnEntity>> GetAll()
    {
      var query = $"{getQuery} {ColumnEntity.orderQuery};";

      using var cnn = GetConnection();
      return await cnn.QueryAsync<ColumnEntity>(query, commandTimeout: cnn.ConnectionTimeout).ConfigureAwait(false);
    }

    private readonly string insertQuery = $"INSERT INTO {ColumnEntity.tableQuery}({ColumnEntity.insertQuery}) VALUES({ColumnEntity.valuesQuery});";
    public async Task<long> Insert(ColumnEntity entity)
    {
      using var cnn = GetConnection();
      return await cnn.ExecuteAsync(insertQuery, entity, commandTimeout: cnn.ConnectionTimeout).ConfigureAwait(false);
    }

    public async Task<bool> Insert(IEnumerable<ColumnEntity> entities)
    {
      var pageSize = 250000;
      var pages = (decimal)entities.Count() / pageSize;

      List<Task<int>> result = new List<Task<int>>();
      for (int pageNumber = 0; pageNumber < pages; pageNumber++)
      {
        int pageStart = pageSize * pageNumber;
        StringBuilder sb = new StringBuilder();
        foreach (ColumnEntity entity in entities.Skip(pageStart).Take(pageSize).AsParallel())
          sb.AppendLine(entity.FillQuery(insertQuery));

        using var cnn = GetConnection();
        result.Add(cnn.ExecuteAsync(sb.ToString(), commandTimeout: cnn.ConnectionTimeout));
      }

      return (await Task.WhenAll(result.ToArray()).ConfigureAwait(false)).All(x => x > 0);
    }

    private readonly string updateQuery = $"UPDATE {ColumnEntity.tableQuery} SET @Params WHERE @Where;";
    public async Task<bool> Update(ColumnEntity entity)
    {
      var query = updateQuery.Replace("@Params", "cdescription=@Description", StringComparison.InvariantCulture);
      query = query.Replace("@Where", ColumnEntity.keyQuery, StringComparison.InvariantCulture);

      using var cnn = GetConnection();
      return await cnn.ExecuteAsync(query, entity, commandTimeout: cnn.ConnectionTimeout).ConfigureAwait(false) == 1;
    }

    public async Task<bool> Update(IEnumerable<ColumnExternalEntity> entities)
    {
      var query = updateQuery.Replace("@Params", ColumnEntity.updateQuery, StringComparison.InvariantCulture);
      query = query.Replace("@Where", ColumnEntity.syncQuery, StringComparison.InvariantCulture);

      StringBuilder sb = new StringBuilder();
      foreach (ColumnExternalEntity entity in entities.AsParallel())
        sb.AppendLine(entity.FillQuery(query));

      using var cnn = GetConnection();
      return await cnn.ExecuteAsync(sb.ToString(), commandTimeout: cnn.ConnectionTimeout).ConfigureAwait(false) == entities.Count();
    }
  }
}
