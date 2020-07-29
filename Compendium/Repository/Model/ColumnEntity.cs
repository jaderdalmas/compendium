using Compendium.Models;
using System;
using System.Globalization;
using System.Linq;

namespace Compendium.Repository.Model
{
  public class ColumnEntity : ColumnExternalEntity
  {
    public const string keyQuery = "cid=@Id";
    public const string fieldsQuery = "cid as id, csyncid as syncid, ctable as tablename, cname as name, cnumber as number, ctype as type, clength as length, cdefault as default, cisnull as isnull, cisidentity as isidentity, cdescription as description";
    public const string insertQuery = "csyncid, ctable, cname, cnumber, ctype, clength, cdefault, cisnull, cisidentity, cdescription";
    public const string valuesQuery = "@SyncId, @TableName, @Name, @Number, @Type, @Length, @Default, @IsNull, @IsIdentity, @Description";
    public ColumnEntity() : base() { }

    public ColumnEntity(SyscatColumn syscatColumn) : base(syscatColumn) { }

    public ColumnEntity(ColumnViewModel viewModel) : base(viewModel)
    {
      if (viewModel != null)
      {
        Id = viewModel.Id;
        Description = viewModel.Description;
      }
    }

    public ColumnEntity(ColumnDescViewModel viewModel) : base()
    {
      if (viewModel != null)
      {
        Id = viewModel.Id;
        Description = viewModel.Description;
      }
    }

    public long Id { get; set; }
    public string Description { get; set; }

    public ColumnViewModel GetViewModel => new ColumnViewModel(this);

    public override string FillQuery(string query)
    {
      return base.FillQuery(query)
                 .Replace("@Description", $"'{Description}'", StringComparison.InvariantCulture)
                 .Replace("@Id", $"{Id}", StringComparison.InvariantCulture);
    }
  }

  public class ColumnExternalEntity : SyncEntity
  {
    public const string tableQuery = "public.tcolumn";
    public const string orderQuery = "Order By ctable, cnumber";
    public const string updateQuery = "ctable=@TableName, cname=@Name, cnumber=@Number, ctype=@Type, clength=@Length, cdefault=@Default, cisnull=@IsNull, cisidentity=@IsIdentity";
    public ColumnExternalEntity() : base()
    {
      Name = string.Empty;
    }

    public ColumnExternalEntity(SyscatColumn syscatColumn) : base(syscatColumn)
    {
      if (syscatColumn != null)
      {
        Name = syscatColumn.ColName;
        Number = syscatColumn.ColNo;
        Type = syscatColumn.TypeName;
        Length = syscatColumn.Length;
        Default = syscatColumn.Default;
        IsNull = syscatColumn.Nulls;
        IsIdentity = syscatColumn.Identity;
      }
    }

    protected ColumnExternalEntity(ColumnViewModel viewModel) : base(viewModel)
    {
      if (viewModel != null)
      {
        Name = !string.IsNullOrWhiteSpace(viewModel.Name) ? viewModel.Name : string.Empty;
        Number = viewModel.Number;
        Type = viewModel.Type;
        Length = viewModel.Length;
        Default = viewModel.Default;
        IsNull = !string.IsNullOrWhiteSpace(viewModel.IsNull) ? viewModel.IsNull.First() : char.MinValue;
        IsIdentity = !string.IsNullOrWhiteSpace(viewModel.IsIdentity) ? viewModel.IsIdentity.First() : ' ';
      }
    }

    private string _columnName;
    public string Name { get => _columnName.Trim().ToUpper(CultureInfo.InvariantCulture); set => _columnName = value; }
    public short Number { get; set; }
    public string Type { get; set; }
    public int Length { get; set; }
    public string Default { get; set; }
    public char IsNull { get; set; }
    public char IsIdentity { get; set; }

    public override string FillQuery(string query)
    {
      return base.FillQuery(query)
                 .Replace("@Name", $"'{Name}'", StringComparison.InvariantCulture)
                 .Replace("@Number", $"{Number}", StringComparison.InvariantCulture)
                 .Replace("@Type", $"'{Type}'", StringComparison.InvariantCulture)
                 .Replace("@Length", $"{Length}", StringComparison.InvariantCulture)
                 .Replace("@Default", Default is null ? "null" : $"'{Default.Replace("'", "''", StringComparison.InvariantCulture)}'", StringComparison.InvariantCulture)
                 .Replace("@IsNull", $"'{IsNull}'", StringComparison.InvariantCulture)
                 .Replace("@IsIdentity", $"'{IsIdentity}'", StringComparison.InvariantCulture);
    }
  }
}