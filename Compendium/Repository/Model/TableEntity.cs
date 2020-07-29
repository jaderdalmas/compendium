using Compendium.Models;
using System;
using System.Linq;

namespace Compendium.Repository.Model
{
  public class TableEntity : TableExternalEntity
  {
    public const string keyQuery = "cid=@Id";
    public const string fieldsQuery = "cid as id, csyncid as syncid, cname as tablename, ctype as type, cdescription as description";
    public const string insertQuery = "csyncid, cname, ctype, cdescription";
    public const string valuesQuery = "@SyncId, @TableName, @Type, @Description";
    public TableEntity() : base() { }

    public TableEntity(SyscatTable syscatTable) : base(syscatTable) { }

    public TableEntity(TableViewModel viewModel) : base(viewModel)
    {
      if (viewModel != null)
      {
        Id = viewModel.Id;
        Description = viewModel.Description;
      }
    }

    public TableEntity(TableDescViewModel viewModel) : base()
    {
      if (viewModel != null)
      {
        Id = viewModel.Id;
        Description = viewModel.Description;
      }
    }

    public long Id { get; set; }
    public string Description { get; set; }

    public TableViewModel GetViewModel => new TableViewModel(this);

    public override string FillQuery(string query)
    {
      return base.FillQuery(query)
                 .Replace("@Description", $"'{Description}'", StringComparison.InvariantCulture)
                 .Replace("@Id", $"{Id}", StringComparison.InvariantCulture);
    }
  }

  public class TableExternalEntity : SyncEntity
  {
    public const string tableQuery = "public.ttable";
    public const string orderQuery = "Order By cname";
    public const string updateQuery = "cname=@TableName, ctype=@Type";
    public TableExternalEntity() : base() { }

    public TableExternalEntity(SyscatTable syscatTable) : base(syscatTable)
    {
      if (syscatTable != null)
        Type = syscatTable.Type;
    }

    protected TableExternalEntity(TableViewModel viewModel) : base(viewModel)
    {
      if (viewModel != null)
        Type = !string.IsNullOrWhiteSpace(viewModel.Type) ? viewModel.Type.First() : ' ';
    }

    public char Type { get; set; }

    public override string FillQuery(string query)
    {
      return base.FillQuery(query)
                 .Replace("@Type", $"'{Type}'", StringComparison.InvariantCulture);
    }
  }
}
