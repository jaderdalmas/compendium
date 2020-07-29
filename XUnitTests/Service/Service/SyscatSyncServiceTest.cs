using Compendium;
using Compendium.Extension;
using Compendium.Repository.Interfaces;
using Compendium.Repository.Model;
using Compendium.Service.Service;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTests.Service.Service
{
  [ExcludeFromCodeCoverage]
  public class SyscatSyncServiceTest : BaseServiceTest, IClassFixture<TestWebApplicationFactory<Startup>>
  {
    public SyscatSyncServiceTest(TestWebApplicationFactory<Startup> factory) : base(factory, ETestType.Empty) { }

    [Fact]
    public async Task Column()
    {
      //Arrange | Act
      var result = await GetSyscatSyncService.ColumnSync().ConfigureAwait(false);

      //Assert
      Assert.True(result);
      Assert.False(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));
    }

    [Fact]
    public async Task Table()
    {
      //Arrange | Act
      var result = await GetSyscatSyncService.TableSync().ConfigureAwait(false);

      //Assert
      Assert.True(result);
      Assert.False(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));
    }

    [Fact]
    public async Task Sync()
    {
      //Arrange | Act
      var result = await GetSyscatSyncService.Sync().ConfigureAwait(false);

      //Assert
      Assert.True(result);
      Assert.False(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));
    }

    [Fact]
    public async Task ColumnMoq()
    {
      //Arrange Syscat Column
      var insertSyscatColumn = new SyscatColumn()
      {
        ColName = "Test",
        ColNo = 1,
        Default = "'False'",
        Identity = 'N',
        Length = 1,
        Nulls = 'Y',
        TabName = "Insert",
        TypeName = "Ins"
      };

      var updateSyscatColumn = new SyscatColumn()
      {
        ColName = "Test",
        ColNo = 1,
        Default = "'False'",
        Identity = 'N',
        Length = 1,
        Nulls = 'Y',
        TabName = "Update",
        TypeName = "Upd"
      };

      var listSyscatColumn = new List<SyscatColumn>() { insertSyscatColumn, updateSyscatColumn };

      //Arrange Syscat Repository
      var moqSyscatRepository = new Mock<ISyscatRepository>();
      moqSyscatRepository
          .Setup(c => c.GetColumn())
          .ReturnsAsync(listSyscatColumn);

      //Arrange Column Repository
      var updateColumnEntity = new ColumnEntity()
      {
        Default = "'False'",
        IsIdentity = 'N',
        IsNull = 'Y',
        Length = 1,
        Name = "Test",
        Number = 1,
        SyncId = "Update.Test",
        TableName = "Update",
        Type = "Upd"
      };

      var listColumnEntity = new List<ColumnEntity>() { updateColumnEntity };

      //Arrange Column Repository
      var moqColumnRepository = new Mock<IColumnRepository>();
      moqColumnRepository
          .Setup(c => c.GetAll())
          .ReturnsAsync(listColumnEntity);
      moqColumnRepository
          .Setup(c => c.Insert(It.IsAny<IEnumerable<ColumnEntity>>()))
          .ReturnsAsync(true);
      moqColumnRepository
          .Setup(c => c.Update(It.IsAny<IEnumerable<ColumnExternalEntity>>()))
          .ReturnsAsync(true);

      //Arrange
      var syncService = new SyscatSyncService(GetNotification, moqSyscatRepository.Object, moqColumnRepository.Object, GetTableRepository);

      //Act
      var result = await syncService.ColumnSync().ConfigureAwait(false);

      //Assert
      Assert.True(result);
      Assert.False(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));
    }

    [Fact]
    public async Task TableMoq()
    {
      //Arrange Syscat Table
      var insertSyscatTable = new SyscatTable()
      {
        ColCount = 1,
        TabName = "Insert",
        Type = 'I'
      };

      var updateSyscatTable = new SyscatTable()
      {
        ColCount = 1,
        TabName = "Update",
        Type = 'U'
      };

      var listSyscatTable = new List<SyscatTable>() { insertSyscatTable, updateSyscatTable };

      //Arrange Syscat Repository
      var moqSyscatRepository = new Mock<ISyscatRepository>();
      moqSyscatRepository
          .Setup(c => c.GetTable())
          .ReturnsAsync(listSyscatTable);

      //Arrange Syscat Table
      var updateTableEntity = new TableEntity()
      {
        SyncId = "Update",
        TableName = "Update",
        Type = 'I'
      };

      var listTableEntity = new List<TableEntity>() { updateTableEntity };

      //Arrange Table Repository
      var moqTableRepository = new Mock<ITableRepository>();
      moqTableRepository
          .Setup(c => c.GetAll())
          .ReturnsAsync(listTableEntity);
      moqTableRepository
          .Setup(c => c.Insert(It.IsAny<IEnumerable<TableEntity>>()))
          .ReturnsAsync(true);
      moqTableRepository
          .Setup(c => c.Update(It.IsAny<IEnumerable<TableExternalEntity>>()))
          .ReturnsAsync(true);

      //Arrange
      var syncService = new SyscatSyncService(GetNotification, moqSyscatRepository.Object, GetColumnRepository, moqTableRepository.Object);

      //Act
      var result = await syncService.TableSync().ConfigureAwait(false);

      //Assert
      Assert.True(result);
      Assert.False(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));
    }
  }

  [ExcludeFromCodeCoverage]
  public class SyscatSyncServiceTestNull : BaseServiceTest, IClassFixture<TestWebApplicationFactory<Startup>>
  {
    public SyscatSyncServiceTestNull(TestWebApplicationFactory<Startup> factory) : base(factory, ETestType.Null) { }

    [Fact]
    public async Task Column()
    {
      //Arrange | Act
      var result = await GetSyscatSyncService.ColumnSync().ConfigureAwait(false);

      //Assert
      Assert.False(result);
      Assert.False(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));
    }

    [Fact]
    public async Task Table()
    {
      //Arrange | Act
      var result = await GetSyscatSyncService.TableSync().ConfigureAwait(false);

      //Assert
      Assert.False(result);
      Assert.False(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));
    }

    [Fact]
    public async Task Sync()
    {
      //Arrange | Act
      var result = await GetSyscatSyncService.Sync().ConfigureAwait(false);

      //Assert
      Assert.False(result);
      Assert.False(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));
    }
  }

  [ExcludeFromCodeCoverage]
  public class SyscatSyncServiceTestException : BaseServiceTest, IClassFixture<TestWebApplicationFactory<Startup>>
  {
    public SyscatSyncServiceTestException(TestWebApplicationFactory<Startup> factory) : base(factory, ETestType.Exception) { }

    [Fact]
    public async Task Column()
    {
      //Arrange | Act
      var result = await GetSyscatSyncService.ColumnSync().ConfigureAwait(false);

      //Assert
      Assert.False(result);
      Assert.True(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));
    }

    [Fact]
    public async Task Table()
    {
      //Arrange | Act
      var result = await GetSyscatSyncService.TableSync().ConfigureAwait(false);

      //Assert
      Assert.False(result);
      Assert.True(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));
    }

    [Fact]
    public async Task Sync()
    {
      //Arrange | Act
      var result = await GetSyscatSyncService.Sync().ConfigureAwait(false);

      //Assert
      Assert.False(result);
      Assert.True(GetNotification.HasNotification(LogLevelExtension.ReturnLevel));
    }
  }
}