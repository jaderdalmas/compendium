using Compendium;
using Compendium.Extension;
using Compendium.Properties;
using Compendium.Repository.DB2;
using Compendium.Setting;
using Microsoft.Extensions.Options;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTests.Repository.DB2
{
  [ExcludeFromCodeCoverage]
  public class SyscatRepositoryTest : BaseRepositoryTest, IClassFixture<TestWebApplicationFactory<Startup>>
  {
    public SyscatRepositoryTest(TestWebApplicationFactory<Startup> factory) : base(factory) { }

    [Fact]
    public async Task GetTable()
    {
      //Arrange | Act
      var result = await GetSyscatRepository.GetTable().ConfigureAwait(false);

      //Assert
      Assert.NotNull(result);
      Assert.False(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));
    }

    [Fact]
    public async Task GetColumn()
    {
      //Arrange | Act
      var result = await GetSyscatRepository.GetColumn().ConfigureAwait(false);

      //Assert
      Assert.NotNull(result);
      Assert.False(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));
    }

    [Fact]
    public void NotificationNull()
    {
      //Arrange | Act
      var repository = new SyscatRepository(GetDB2Settings, null);

      //Assert
      Assert.NotNull(repository);
    }

    [Fact]
    public void DB2SettingsNull()
    {
      //Arrange | Act
      var repository = new SyscatRepository(null, GetNotification);

      //Assert
      Assert.True(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));

      var notifications = GetNotification.GetNotification(LogLevelExtension.ReturnLevel);
      Assert.Equal(Resources.DB2SettingMissing, notifications.First().Message);
    }

    [Fact]
    public void DB2SettingsEmpty()
    {
      //Arrange
      var settings = new Mock<IOptions<DB2Setting>>();
      settings.Setup(c => c.Value).Returns(new DB2Setting());

      //Act
      var repository = new SyscatRepository(settings.Object, GetNotification);

      //Assert
      Assert.True(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));

      var notifications = GetNotification.GetNotification(LogLevelExtension.ReturnLevel);
      Assert.Equal(Resources.DB2SettingMissing, notifications.First().Message);
    }
  }
}
