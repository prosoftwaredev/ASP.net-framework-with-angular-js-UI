
using PayrollApp.Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PayrollApp.Repository
{
    public partial class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// The _context
        /// </summary>
        private readonly DbContext _context;

        /// <summary>
        /// The _entities
        /// </summary>
        private DbSet<T> _entities;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context">Object context</param>
        public GenericRepository(DbContext context)
        {
            this._context = context;
        }

        #region Asynchronous Calls

        /// <summary>
        /// Get entity by identifier - asynchronous
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public async virtual Task<T> GetByIdAsync(object id)
        {
            //see some suggested performance optimization (not tested)
            //http://stackoverflow.com/questions/11686225/dbset-find-method-ridiculously-slow-compared-to-singleordefault-on-id/11688189#comment34876113_11688189
            return await this.Entities.FindAsync(id);
        }

        /// <summary>
        /// Insert entity - asynchronous
        /// </summary>
        /// <param name="entity">Entity</param>
        public async virtual Task<int> InsertAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this.Entities.Add(entity);

                await this._context.SaveChangesAsync();
                return 1;
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

                var fail = new Exception(msg, dbEx);
                throw fail;

            }
        }


        /// <summary>
        /// Update entity - asynchronous
        /// </summary>
        /// <param name="entity">Entity</param>
        public async virtual Task<int> UpdateAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                await this._context.SaveChangesAsync();
                return 1;
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                var fail = new Exception(msg, dbEx);
                Debug.WriteLine(fail.Message, fail);
                //throw fail;
                return 0;
            }
        }


        /// <summary>
        /// Delete entity - asynchronous
        /// </summary>
        /// <param name="entity">Entity</param>
        public async virtual Task<int> DeleteAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this.Entities.Remove(entity);

                await this._context.SaveChangesAsync();
                return 1;
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                var fail = new Exception(msg, dbEx);
                Debug.WriteLine(fail.Message, fail);
                //throw fail;
                return 0;
            }
        }


        public async virtual Task<int> UpdateAllAsync(List<T> entity)
        {
            try
            {
                await this._context.SaveChangesAsync();
                return 1;
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                var fail = new Exception(msg, dbEx);
                Debug.WriteLine(fail.Message, fail);
                //throw fail;
                return 1;
            }
        }

        /// <summary>
        /// Delete All entities by bulk - asynchronous
        /// </summary>
        /// <param name="entity"></param>
        public async virtual Task<int> DeleteAllAsync(List<T> entity)
        {
            try
            {
                foreach (var item in entity)
                {
                    this.Entities.Remove(item);
                }
                await this._context.SaveChangesAsync();
                return 1;
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                var fail = new Exception(msg, dbEx);
                Debug.WriteLine(fail.Message, fail);
                //throw fail;
                return 1;
            }
        }

        #endregion


        #region Synchronous Calls

        /// <summary>
        /// Get entity by identifier
        /// </summary>
        /// <param name="id">Identifier</param>
        /// <returns>Entity</returns>
        public T GetById(object id)
        {
            return this.Entities.Find(id);
        }

        /// <summary>
        /// Insert entity 
        /// </summary>
        /// <param name="entity">Entity</param>
        public int Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this.Entities.Add(entity);

                this._context.SaveChanges();
                return 1;
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage) + Environment.NewLine;

                var fail = new Exception(msg, dbEx);
                throw fail;

            }
        }

        /// <summary>
        /// Update entity 
        /// </summary>
        /// <param name="entity">Entity</param>
        public int Update(T entity)
        {
             try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this._context.SaveChanges();
                return 1;
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                var fail = new Exception(msg, dbEx);
                Debug.WriteLine(fail.Message, fail);
                //throw fail;
                return 0;
            }
        }

        /// <summary>
        /// Delete entity 
        /// </summary>
        /// <param name="entity">Entity</param>
        public int Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                this.Entities.Remove(entity);

                this._context.SaveChanges();
                return 1;
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                var fail = new Exception(msg, dbEx);
                Debug.WriteLine(fail.Message, fail);
                //throw fail;
                return 0;
            }
        }

        /// <summary>
        /// Delete All entities by bulk 
        /// </summary>
        /// <param name="entity"></param>
        public int DeleteAll(List<T> entity)
        {
            try
            {
                foreach (var item in entity)
                {
                    this.Entities.Remove(item);
                }
                this._context.SaveChanges();
                return 1;
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                var fail = new Exception(msg, dbEx);
                Debug.WriteLine(fail.Message, fail);
                //throw fail;
                return 1;
            }
        }

        #endregion

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<T> Table
        {
            get
            {
                return this.Entities;
            }
        }


        /// <summary>
        /// Gets the entities.
        /// </summary>
        /// <value>
        /// The entities.
        /// </value>
        public virtual DbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();
                return _entities;
            }
        }





    }
}
