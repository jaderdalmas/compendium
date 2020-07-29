using Compendium;
using Compendium.Extension;
using Compendium.Properties;
using Compendium.Repository.Postgre;
using Compendium.Setting;
using Microsoft.Extensions.Options;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTests.Repository.PostGreSql
{
  [ExcludeFromCodeCoverage]
  public class ColumnRepositoryTest : BaseRepositoryTest, IClassFixture<TestWebApplicationFactory<Startup>>
  {
    public ColumnRepositoryTest(TestWebApplicationFactory<Startup> factory) : base(factory) { }

    [Fact]
    public async Task GetAll()
    {
      //Arrange | Act
      var result = await GetColumnRepository.GetAll().ConfigureAwait(false);

      //Assert
      Assert.NotNull(result);
      Assert.False(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));
    }

    [Fact]
    public void NotificationNull()
    {
      //Arrange | Act
      var repository = new ColumnRepository(GetPostgreSettings, null);

      //Assert
      Assert.NotNull(repository);
    }

    [Fact]
    public void PostgreSettingsNull()
    {
      //Arrange | Act
      var repository = new ColumnRepository(null, GetNotification);

      //Assert
      Assert.True(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));

      var notifications = GetNotification.GetNotification(LogLevelExtension.ReturnLevel);
      Assert.Equal(Resources.PostGreSettingMissing, notifications.First().Message);
    }

    [Fact]
    public void PostgreSettingsEmpty()
    {
      //Arrange
      var settings = new Mock<IOptions<PostgreSetting>>();
      settings.Setup(c => c.Value).Returns(new PostgreSetting());

      //Act
      var repository = new ColumnRepository(settings.Object, GetNotification);

      //Assert
      Assert.True(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));

      var notifications = GetNotification.GetNotification(LogLevelExtension.ReturnLevel);
      Assert.Equal(Resources.PostGreSettingMissing, notifications.First().Message);
    }
  }
}
