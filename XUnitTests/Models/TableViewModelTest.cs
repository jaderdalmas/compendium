using Compendium.Models;
using Compendium.Repository.Model;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using Xunit;
using XUnitTests.Extension;

namespace XUnitTests.Models
{
  [ExcludeFromCodeCoverage]
  public class TableViewModelTest
  {
    [Fact]
    public void Valid()
    {
      var model = new TableViewModel()
      {
        Description = "Test",
        Name = "Test",
        Type = "T"
      };

      var result = model.Validate();
      Assert.False(result.Any());
    }

    [Fact]
    public void Required()
    {
      var model = new TableViewModel();

      var result = model.Validate();
      Assert.True(result.Any());

      Assert.Equal("Description", result.ElementAtOrDefault(0).MemberNames.FirstOrDefault());
      Assert.Equal("The Description field is required.", result.ElementAtOrDefault(0).ErrorMessage);

      Assert.Equal("Name", result.ElementAtOrDefault(1).MemberNames.FirstOrDefault());
      Assert.Equal("The Name field is required.", result.ElementAtOrDefault(1).ErrorMessage);

      Assert.Equal("Type", result.ElementAtOrDefault(2).MemberNames.FirstOrDefault());
      Assert.Equal("The Type of Data field is required.", result.ElementAtOrDefault(2).ErrorMessage);
    }

    [Fact]
    public void RangeOrLength()
    {
      var model = new TableViewModel()
      {
        Description = "Test",
        Id = -1,
        Name = "Test",
        Type = "Test"
      };

      var result = model.Validate();
      Assert.True(result.Any());

      Assert.Equal("Id", result.ElementAtOrDefault(0).MemberNames.FirstOrDefault());
      Assert.Equal("The field Id must be between 0 and 9,223372036854776E+18.", result.ElementAtOrDefault(0).ErrorMessage);

      Assert.Equal("Type", result.ElementAtOrDefault(1).MemberNames.FirstOrDefault());
      Assert.Equal("The field Type of Data must be a string with a minimum length of 1 and a maximum length of 1.", result.ElementAtOrDefault(1).ErrorMessage);
    }

    [Fact]
    public void ToEntity()
    {
      var model = new TableViewModel()
      {
        Description = "Test",
        Id = 1,
        Name = "Test",
        Type = "T"
      };

      var result = model.Validate();
      Assert.False(result.Any());

      var entity = model.GetEntity();
      Assert.Equal(model.Description, entity.Description);
      Assert.Equal(model.Id, entity.Id);
      Assert.Equal(model.Name.ToUpper(CultureInfo.InvariantCulture), entity.TableName);
      Assert.Equal(model.Type.First(), entity.Type);
    }

    [Fact]
    public void FromEntity()
    {
      var entity = new TableEntity()
      {
        Description = "Test",
        Id = 1,
        TableName = "Test",
        Type = 'T'
      };
      var model = new TableViewModel(entity);

      var result = model.Validate();
      Assert.False(result.Any());

      Assert.Equal(entity.Description, model.Description);
      Assert.Equal(entity.Id, model.Id);
      Assert.Equal(entity.TableName, model.Name);
      Assert.Equal(entity.Type, model.Type.First());
    }
  }
}
