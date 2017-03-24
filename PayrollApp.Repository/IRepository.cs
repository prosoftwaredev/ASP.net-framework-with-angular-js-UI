using PayrollApp.Core.Data.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollApp.Repository
{
    /// <summary>
    /// Repository
    /// </summary>
    public partial interface IRepository<T> where T : BaseEntity
    {

        #region Asynchronous Calls

        /// <summary>
        /// Get entity by identifier - asynchronous
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        Task<T> GetByIdAsync(object id);

        /// <summary>
        /// Insert entity - asynchronous
        /// </summary>
        /// <param name="entity">Entity</param>
        Task<int> InsertAsync(T entity);

        /// <summary>
        /// Update entity - asynchronous
        /// </summary>
        /// <param name="entity">Entity</param>
        Task<int> UpdateAsync(T entity);

        /// <summary>
        /// Delete entity - asynchronous
        /// </summary>
        /// <param name="entity">Entity</param>
        Task<int> DeleteAsync(T entity);



        Task<int> UpdateAllAsync(List<T> entity);
        /// <summary>
        /// Delete All entities by bulk - asynchronous
        /// </summary>
        /// <param name="entity"></param>
        Task<int> DeleteAllAsync(List<T> entity);

        #endregion


        #region Synchronous Calls

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        T GetById(object id);

        /// <summary>
        /// Insert entity 
        /// </summary>
        /// <param name="entity">Entity</param>
        int Insert(T entity);

        /// <summary>
        /// Update entity 
        /// </summary>
        /// <param name="entity">Entity</param>
        int Update(T entity);

        /// <summary>
        /// Delete entity 
        /// </summary>
        /// <param name="entity">Entity</param>
        int Delete(T entity);

      
        /// <summary>
        /// Delete All entities by bulk 
        /// </summary>
        /// <param name="entity"></param>
        int DeleteAll(List<T> entity);


        #endregion


        /// <summary>
        /// Gets a table
        /// </summary>
        IQueryable<T> Table { get; }


        /// <summary>
        /// Gets the entities.
        /// </summary>
        /// <value>
        /// The entities.
        /// </value>
        DbSet<T> Entities { get; }
    }
}
