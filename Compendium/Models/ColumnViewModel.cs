using Compendium.Repository.Model;
using System.ComponentModel.DataAnnotations;

namespace Compendium.Models
{
  public class ColumnViewModel
  {
    public ColumnViewModel() { }

    public ColumnViewModel(ColumnEntity entity)
    {
      if (entity != null)
      {
        Id = entity.Id;
        Description = entity.Description;

        TableName = entity.TableName;
        Name = entity.Name;
        Number = entity.Number;
        Type = entity.Type;
        Length = entity.Length;
        Default = entity.Default;
        IsNull = $"{entity.IsNull}";
        IsIdentity = $"{entity.IsIdentity}";
      }
    }

    [Range(0, long.MaxValue)]
    public long Id { get; set; }
    [Required, StringLength(500, MinimumLength = 1)]
    public string Description { get; set; }


    [Required, StringLength(128, MinimumLength = 1), Display(Name = "Table Name")]
    public string TableName { get; set; }
    [Required, StringLength(128, MinimumLength = 1)]
    public string Name { get; set; }
    [Range(0, short.MaxValue)]
    public short Number { get; set; }
    [Required, StringLength(128, MinimumLength = 1)]
    public string Type { get; set; }
    [Range(0, int.MaxValue)]
    public int Length { get; set; }
    [StringLength(128, MinimumLength = 1)]
    public string Default { get; set; }
    [Required, StringLength(1, MinimumLength = 1), Display(Name = "Is Null?")]
    public string IsNull { get; set; }
    [Required, StringLength(1, MinimumLength = 1), Display(Name = "Is Identity?")]
    public string IsIdentity { get; set; }

    public ColumnEntity GetEntity() => new ColumnEntity(this);
  }

  public class ColumnDescViewModel
  {
    public ColumnDescViewModel() { }

    public ColumnDescViewModel(ColumnEntity entity)
    {
      if (entity != null)
      {
        Id = entity.Id;
        Description = entity.Description;
      }
    }

    [Range(0, long.MaxValue)]
    public long Id { get; set; }
    [Required, StringLength(500, MinimumLength = 1)]
    public string Description { get; set; }

    public ColumnEntity GetEntity() => new ColumnEntity(this);
  }
}
