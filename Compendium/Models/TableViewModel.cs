using Compendium.Repository.Model;
using System.ComponentModel.DataAnnotations;

namespace Compendium.Models
{
  public class TableViewModel
  {
    public TableViewModel() { }

    public TableViewModel(TableEntity entity)
    {
      if (entity != null)
      {
        Id = entity.Id;
        Description = entity.Description;

        Name = entity.TableName;
        Type = $"{entity.Type}";
      }
    }

    [Range(0, long.MaxValue)]
    public long Id { get; set; }
    [Required, StringLength(500, MinimumLength = 1)]
    public string Description { get; set; }


    [Required, StringLength(128, MinimumLength = 1)]
    public string Name { get; set; }
    [Required, StringLength(1, MinimumLength = 1), Display(Name = "Type of Data")]
    public string Type { get; set; }

    public TableEntity GetEntity() => new TableEntity(this);
  }

  public class TableDescViewModel
  {
    public TableDescViewModel() { }

    public TableDescViewModel(TableEntity entity)
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

    public TableEntity GetEntity() => new TableEntity(this);
  }
}
