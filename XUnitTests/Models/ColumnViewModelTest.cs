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
  public class ColumnViewModelTest
  {
    [Fact]
    public void Valid()
    {
      var model = new ColumnViewModel()
      {
        Description = "Test",
        IsIdentity = "N",
        IsNull = "Y",
        Name = "Test",
        TableName = "Test",
        Type = "Test"
      };

      var result = model.Validate();
      Assert.False(result.Any());
    }

    [Fact]
    public void Required()
    {
      var model = new ColumnViewModel();

      var result = model.Validate();
      Assert.True(result.Any());

      Assert.Equal("Description", result.ElementAtOrDefault(0).MemberNames.FirstOrDefault());
      Assert.Equal("The Description field is required.", result.ElementAtOrDefault(0).ErrorMessage);

      Assert.Equal("TableName", result.ElementAtOrDefault(1).MemberNames.FirstOrDefault());
      Assert.Equal("The Table Name field is required.", result.ElementAtOrDefault(1).ErrorMessage);

      Assert.Equal("Name", result.ElementAtOrDefault(2).MemberNames.FirstOrDefault());
      Assert.Equal("The Name field is required.", result.ElementAtOrDefault(2).ErrorMessage);

      Assert.Equal("Type", result.ElementAtOrDefault(3).MemberNames.FirstOrDefault());
      Assert.Equal("The Type field is required.", result.ElementAtOrDefault(3).ErrorMessage);

      Assert.Equal("IsNull", result.ElementAtOrDefault(4).MemberNames.FirstOrDefault());
      Assert.Equal("The Is Null? field is required.", result.ElementAtOrDefault(4).ErrorMessage);

      Assert.Equal("IsIdentity", result.ElementAtOrDefault(5).MemberNames.FirstOrDefault());
      Assert.Equal("The Is Identity? field is required.", result.ElementAtOrDefault(5).ErrorMessage);
    }

    [Fact]
    public void RangeOrLength()
    {
      var model = new ColumnViewModel()
      {
        Default = string.Empty,
        Description = "Test",
        Id = -1,
        IsIdentity = "No",
        IsNull = "Yes",
        Length = -1,
        Name = "Test",
        Number = -1,
        TableName = "Test",
        Type = "Test"
      };

      var result = model.Validate();
      Assert.True(result.Any());

      Assert.Equal("Id", result.ElementAtOrDefault(0).MemberNames.FirstOrDefault());
      Assert.Equal("The field Id must be between 0 and 9,223372036854776E+18.", result.ElementAtOrDefault(0).ErrorMessage);

      Assert.Equal("Number", result.ElementAtOrDefault(1).MemberNames.FirstOrDefault());
      Assert.Equal("The field Number must be between 0 and 32767.", result.ElementAtOrDefault(1).ErrorMessage);

      Assert.Equal("Length", result.ElementAtOrDefault(2).MemberNames.FirstOrDefault());
      Assert.Equal("The field Length must be between 0 and 2147483647.", result.ElementAtOrDefault(2).ErrorMessage);

      Assert.Equal("Default", result.ElementAtOrDefault(3).MemberNames.FirstOrDefault());
      Assert.Equal("The field Default must be a string with a minimum length of 1 and a maximum length of 128.", result.ElementAtOrDefault(3).ErrorMessage);

      Assert.Equal("IsNull", result.ElementAtOrDefault(4).MemberNames.FirstOrDefault());
      Assert.Equal("The field Is Null? must be a string with a minimum length of 1 and a maximum length of 1.", result.ElementAtOrDefault(4).ErrorMessage);

      Assert.Equal("IsIdentity", result.ElementAtOrDefault(5).MemberNames.FirstOrDefault());
      Assert.Equal("The field Is Identity? must be a string with a minimum length of 1 and a maximum length of 1.", result.ElementAtOrDefault(5).ErrorMessage);
    }

    [Fact]
    public void ToEntity()
    {
      var model = new ColumnViewModel()
      {
        Default = "Test",
        Description = "Test",
        Id = 1,
        IsIdentity = "N",
        IsNull = "Y",
        Length = 1,
        Name = "Test",
        Number = 1,
        TableName = "Test",
        Type = "Test"
      };

      var result = model.Validate();
      Assert.False(result.Any());

      var entity = model.GetEntity();
      Assert.Equal(model.Default, entity.Default);
      Assert.Equal(model.Description, entity.Description);
      Assert.Equal(model.Id, entity.Id);
      Assert.Equal(model.IsIdentity.First(), entity.IsIdentity);
      Assert.Equal(model.IsNull.First(), entity.IsNull);
      Assert.Equal(model.Length, entity.Length);
      Assert.Equal(model.Name.ToUpper(CultureInfo.InvariantCulture), entity.Name);
      Assert.Equal(model.Number, entity.Number);
      Assert.Equal(model.TableName.ToUpper(CultureInfo.InvariantCulture), entity.TableName);
      Assert.Equal(model.Type, entity.Type);
    }

    [Fact]
    public void FromEntity()
    {
      var entity = new ColumnEntity()
      {
        Default = "Test",
        Description = "Test",
        Id = 1,
        IsIdentity = 'N',
        IsNull = 'Y',
        Length = 1,
        Name = "Test",
        Number = 1,
        TableName = "Test",
        Type = "Test"
      };
      var model = new ColumnViewModel(entity);

      var result = model.Validate();
      Assert.False(result.Any());

      Assert.Equal(entity.Default, model.Default);
      Assert.Equal(entity.Description, model.Description);
      Assert.Equal(entity.Id, model.Id);
      Assert.Equal(entity.IsIdentity, model.IsIdentity.First());
      Assert.Equal(entity.IsNull, model.IsNull.First());
      Assert.Equal(entity.Length, model.Length);
      Assert.Equal(entity.Name, model.Name);
      Assert.Equal(entity.Number, model.Number);
      Assert.Equal(entity.TableName, model.TableName);
      Assert.Equal(entity.Type, model.Type);
    }
  }
}
