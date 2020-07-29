using Compendium.Models;
using Compendium.Repository.Model;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Xunit;
using XUnitTests.Extension;

namespace XUnitTests.Models
{
  [ExcludeFromCodeCoverage]
  public class ColumnDescViewModelTest
  {
    [Fact]
    public void Valid()
    {
      var model = new ColumnDescViewModel()
      {
        Description = "Test"
      };

      var result = model.Validate();
      Assert.False(result.Any());
    }

    [Fact]
    public void Required()
    {
      var model = new ColumnDescViewModel();

      var result = model.Validate();
      Assert.True(result.Any());

      Assert.Equal("Description", result.ElementAtOrDefault(0).MemberNames.FirstOrDefault());
      Assert.Equal("The Description field is required.", result.ElementAtOrDefault(0).ErrorMessage);
    }

    [Fact]
    public void RangeOrLength()
    {
      var model = new ColumnDescViewModel()
      {
        Description = "Test",
        Id = -1
      };

      var result = model.Validate();
      Assert.True(result.Any());

      Assert.Equal("Id", result.ElementAtOrDefault(0).MemberNames.FirstOrDefault());
      Assert.Equal("The field Id must be between 0 and 9,223372036854776E+18.", result.ElementAtOrDefault(0).ErrorMessage);
    }


    [Fact]
    public void ToEntity()
    {
      var model = new ColumnDescViewModel()
      {
        Description = "Test",
        Id = 1
      };

      var result = model.Validate();
      Assert.False(result.Any());

      var entity = model.GetEntity();
      Assert.Equal(model.Description, entity.Description);
      Assert.Equal(model.Id, entity.Id);
    }

    [Fact]
    public void FromEntity()
    {
      var entity = new ColumnEntity()
      {
        Description = "Test",
        Id = 1
      };
      var model = new ColumnDescViewModel(entity);

      var result = model.Validate();
      Assert.False(result.Any());

      Assert.Equal(entity.Description, model.Description);
      Assert.Equal(entity.Id, model.Id);
    }
  }
}
