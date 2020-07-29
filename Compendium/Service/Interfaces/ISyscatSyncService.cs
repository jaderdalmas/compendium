using System.Threading.Tasks;

namespace Compendium.Service.Interfaces
{
  public interface ISyscatSyncService
  {
    /// <summary>
    /// Sync All
    /// </summary>
    /// <returns>done?</returns>
    Task<bool> Sync();

    /// <summary>
    /// Sync Tables
    /// </summary>
    /// <returns>done?</returns>
    Task<bool> TableSync();

    /// <summary>
    /// Sync Columns
    /// </summary>
    /// <returns>done?</returns>
    Task<bool> ColumnSync();
  }
}
