using Compendium.Repository.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Compendium.Repository.Interfaces
{
  public interface IColumnRepository
  {
    /// <summary>
    /// Delete Entity
    /// </summary>
    /// <param id="id">Column Entity Id</param>
    /// <returns>done?</returns>
    Task<bool> Delete(long id);

    /// <summary>
    ///  Delete Entities
    /// </summary>
    /// <param name="ids">Column Entity Ids</param>
    /// <returns>done?</returns>
    Task<bool> Delete(IEnumerable<long> ids);

    /// <summary>
    /// Exists Entity
    /// </summary>
    /// <param name="id">Column Entity Id</param>
    /// <returns>Exists?</returns>
    Task<bool> Exists(long id);

    /// <summary>
    /// Get Entity
    /// </summary>
    /// <param name="id">Column Entity Id</param>
    /// <returns>Column Entity</returns>
    Task<ColumnEntity> GetOne(long id);

    /// <summary>
    /// Get Column by name
    /// </summary>
    /// <param name="tableName">Table Name</param>
    /// <param name="name">Column Name</param>
    /// <returns>Column Entity List</returns>
    Task<IEnumerable<ColumnEntity>> GetAll(string tableName, string name);

    /// <summary>
    /// Get All Entities
    /// </summary>
    /// <returns>Column Entity List</returns>
    Task<IEnumerable<ColumnEntity>> GetAll();

    /// <summary>
    /// Insert Entity
    /// </summary>
    /// <param name="entity">Column Entity</param>
    /// <returns>Column Entity id</returns>
    Task<long> Insert(ColumnEntity entity);

    /// <summary>
    /// Insert Entity List
    /// </summary>
    /// <param name="entities">Column Entity List</param>
    /// <returns>done?</returns>
    Task<bool> Insert(IEnumerable<ColumnEntity> entities);

    /// <summary>
    /// Update Entity
    /// </summary>
    /// <param name="entity">Column Entity</param>
    /// <returns>done?</returns>
    Task<bool> Update(ColumnEntity entity);

    /// <summary>
    /// Update Entities
    /// </summary>
    /// <param name="entities">Column Entities</param>
    /// <returns>done?</returns>
    Task<bool> Update(IEnumerable<ColumnExternalEntity> entities);
  }
}
