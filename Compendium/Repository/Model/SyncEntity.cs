using Compendium.Models;
using System;
using System.Globalization;

namespace Compendium.Repository.Model
{
  public class SyncEntity
  {
    public const string syncQuery = "csyncid=@SyncId";

    public SyncEntity()
    {
      TableName = string.Empty;
    }

    protected SyncEntity(SyscatSync syscatSync)
    {
      if (syscatSync != null)
      {
        SyncId = syscatSync.SyncId;
        TableName = syscatSync.TabName;
      }
    }

    protected SyncEntity(TableViewModel viewModel)
    {
      if (viewModel != null)
        TableName = !string.IsNullOrWhiteSpace(viewModel.Name) ? viewModel.Name : string.Empty;
    }

    protected SyncEntity(ColumnViewModel viewModel)
    {
      if (viewModel != null)
        TableName = !string.IsNullOrWhiteSpace(viewModel.TableName) ? viewModel.TableName : string.Empty;
    }

    public string SyncId { get; set; }

    private string _tableName;
    public string TableName { get => _tableName.Trim().ToUpper(CultureInfo.InvariantCulture); set => _tableName = value; }

    public virtual string FillQuery(string query)
    {
      return string.IsNullOrWhiteSpace(query) ? string.Empty :
          query.Replace("@SyncId", $"'{SyncId}'", StringComparison.InvariantCulture)
               .Replace("@TableName", $"'{TableName}'", StringComparison.InvariantCulture);
    }
  }
}
