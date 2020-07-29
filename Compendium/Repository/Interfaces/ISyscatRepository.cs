using Compendium.Repository.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Compendium.Repository.Interfaces
{
  public interface ISyscatRepository
  {
    /// <summary>
    /// Get Syscat Table
    /// </summary>
    /// <returns>Syscat Table List</returns>
    Task<IEnumerable<SyscatTable>> GetTable();

    /// <summary>
    /// Get Syscat Column
    /// </summary>
    /// <returns>Syscat Column List</returns>
    Task<IEnumerable<SyscatColumn>> GetColumn();
  }
}
