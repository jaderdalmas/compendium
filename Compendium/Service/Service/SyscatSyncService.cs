using Compendium.Notify;
using Compendium.Repository.Interfaces;
using Compendium.Service.Interfaces;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Compendium.Service.Service
{
  public class SyscatSyncService : ISyscatSyncService
  {
    private readonly INotification _notification;

    private readonly ISyscatRepository _syscatRepository;
    private readonly IColumnRepository _columnRepository;
    private readonly ITableRepository _tableRepository;

    public SyscatSyncService(INotification notification, ISyscatRepository syscatRepository, IColumnRepository columnRepository, ITableRepository tableRepository)
    {
      _notification = notification;

      _syscatRepository = syscatRepository;
      _columnRepository = columnRepository;
      _tableRepository = tableRepository;
    }

    public async Task<bool> ColumnSync()
    {
      try
      {
        var syscat = await _syscatRepository.GetColumn().ConfigureAwait(false);
        var columns = await _columnRepository.GetAll().ConfigureAwait(false);

        if (syscat is null || columns is null) return false;
        columns = columns.Where(x => x.SyncId != null);

        var inserts = syscat.AsParallel().Where(x => x.IsInsert(columns)).Select(x => x.GetEntityInsert);
        var updates = syscat.AsParallel().Where(x => x.IsUpdate(columns)).Select(x => x.GetEntityUpdate);
        return (await Task.WhenAll(
          new Task<bool>[] {
          _columnRepository.Insert(inserts)
          ,_columnRepository.Update(updates)
          }).ConfigureAwait(false)).All(x => x);
      }
      catch (ExternalException e)
      {
        _notification.AddNotification(e.StackTrace, e.Message, LogLevel.Error);
        return false;
      }
    }

    public async Task<bool> Sync()
    {
      return (await Task.WhenAll(
        new Task<bool>[] {
          TableSync(),
          ColumnSync()
        }).ConfigureAwait(false)).All(x => x);
    }

    public async Task<bool> TableSync()
    {
      try
      {
        var syscat = await _syscatRepository.GetTable().ConfigureAwait(false);
        var tables = await _tableRepository.GetAll().ConfigureAwait(false);

        if (syscat is null || tables is null) return false;
        tables = tables.Where(x => x.SyncId != null);

        var inserts = syscat.AsParallel().Where(x => x.IsInsert(tables)).Select(x => x.GetEntityInsert);
        var updates = syscat.AsParallel().Where(x => x.IsUpdate(tables)).Select(x => x.GetEntityUpdate);
        return (await Task.WhenAll(
          new Task<bool>[] {
          _tableRepository.Insert(inserts)
          ,_tableRepository.Update(updates)
          }).ConfigureAwait(false)).All(x => x);
      }
      catch (ExternalException e)
      {
        _notification.AddNotification(e.StackTrace, e.Message, LogLevel.Error);
        return false;
      }
    }
  }
}