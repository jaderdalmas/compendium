using Compendium.Repository.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Compendium.Repository.Interfaces
{
  public interface ITableRepository
  {
    /// <summary>
    /// Delete Entity
    /// </summary>
    /// <param id="id">Table Entity Id</param>
    /// <returns>done?</returns>
    Task<bool> Delete(long id);

    /// <summary>
    ///  Delete Entities
    /// </summary>
    /// <param name="ids">Table Entity Ids</param>
    /// <returns>done?</returns>
    Task<bool> Delete(IEnumerable<long> ids);

    /// <summary>
    /// Exists Entity
    /// </summary>
    /// <param name="id">Table Entity Id</param>
    /// <returns>Exists?</returns>
    Task<bool> Exists(long id);

    /// <summary>
    /// Get Entity
    /// </summary>
    /// <param name="id">Table Entity Id</param>
    /// <returns>Table Entity</returns>
    Task<TableEntity> GetOne(long id);

    /// <summary>
    /// Get Tables by name
    /// </summary>
    /// <param name="name">Table Name</param>
    /// <returns>Table Entity List</returns>
    Task<IEnumerable<TableEntity>> GetAll(string name);

    /// <summary>
    /// Get All Entities
    /// </summary>
    /// <returns>Table Entity List</returns>
    Task<IEnumerable<TableEntity>> GetAll();

    /// <summary>
    /// Insert Entity
    /// </summary>
    /// <param name="entity">Table Entity</param>
    /// <returns>Table Entity id</returns>
    Task<long> Insert(TableEntity entity);

    /// <summary>
    /// Insert Entity List
    /// </summary>
    /// <param name="entities">Table Entity List</param>
    /// <returns>done?</returns>
    Task<bool> Insert(IEnumerable<TableEntity> entities);

    /// <summary>
    /// Update Entity
    /// </summary>
    /// <param name="entity">Table Entity</param>
    /// <returns>done?</returns>
    Task<bool> Update(TableEntity entity);

    /// <summary>
    /// Update Entities
    /// </summary>
    /// <param name="entities">Table Entities</param>
    /// <returns>done?</returns>
    Task<bool> Update(IEnumerable<TableExternalEntity> entities);
  }
}
