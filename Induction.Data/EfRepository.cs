using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Induction.Core;
using Induction.Core.Data;
using System.Data.Entity.Migrations;
using System.Data.Entity.Infrastructure;

namespace Induction.Data
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        #region Fields

        private readonly DbContext _context;
        private IDbSet<T> _entities;

        #endregion

        #region Ctor

        public EfRepository(DbContext context)
        {
            _context = context;
        }

        #endregion

        #region Properties

        // Returns the IQueryable entity (table) so we can execute filtered query on the database.        
        public virtual IQueryable<T> Table
        {
            get
            {
                return Entities;
            }
        }


        // Returns the entity of generic type T.
        protected virtual IDbSet<T> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _context.Set<T>();

                return _entities;
            }
        }

        #endregion

        #region Methods

        public virtual T GetById(object id)
        {
            return Entities.Find(id);
        }

        public virtual void Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                entity.DateCreated = DateTime.Now.ToPstTime();
                entity.DateModified = DateTime.Now.ToPstTime();
                Entities.Add(entity);

                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += string.Format("{0}", validationError.ErrorMessage) + Environment.NewLine;

                var fail = new Exception(msg, dbEx);
                Debug.WriteLine(fail.Message, fail);
                throw fail;
            }
            catch (SqlException sqlEx)
            {
                var msg = new StringBuilder();

                for (var i = 0; i < sqlEx.Errors.Count; i++)
                {
                    msg.Append("Index #" + i + "\n" +
                        "Message: " + sqlEx.Errors[i].Message + "\n" +
                        "LineNumber: " + sqlEx.Errors[i].LineNumber + "\n" +
                        "Source: " + sqlEx.Errors[i].Source + "\n" +
                        "Procedure: " + sqlEx.Errors[i].Procedure + "\n");
                }

                var fail = new Exception(msg.ToString(), sqlEx);
                Debug.WriteLine(fail.Message, fail);
                throw fail;
            }
        }

        public virtual void Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                //_context.Entry(entity).State = EntityState.Modified; //I added this line because Update wasn't updating
                entity.DateModified = DateTime.Now.ToPstTime();
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException dbucx)
            {
                var msg = string.Empty;
                var fail = new Exception(msg, dbucx);
                Debug.WriteLine(fail.Message, fail);
                throw fail;
            }
            catch (DbUpdateException dbux)
            {
                var msg = string.Empty;
                var fail = new Exception(msg, dbux);
                Debug.WriteLine(fail.Message, fail);
                throw fail;
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                var fail = new Exception(msg, dbEx);
                Debug.WriteLine(fail.Message, fail);
                throw fail;
            }
            catch (NotSupportedException nsx)
            {
                var msg = string.Empty;
                var fail = new Exception(msg, nsx);
                Debug.WriteLine(fail.Message, fail);
                throw fail;
            }
            catch (ObjectDisposedException odx)
            {
                var msg = string.Empty;
                var fail = new Exception(msg, odx);
                Debug.WriteLine(fail.Message, fail);
                throw fail;
            }
            catch (InvalidOperationException iox)
            {
                var msg = string.Empty;
                var fail = new Exception(msg, iox);
                Debug.WriteLine(fail.Message, fail);
                throw fail;
            }
        }

        public virtual void AddOrUpdate(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                Entities.AddOrUpdate(entity);

                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                var fail = new Exception(msg, dbEx);
                Debug.WriteLine(fail.Message, fail);
                throw fail;
            }
            catch (InvalidOperationException iox)
            {
                var msg = string.Empty;
                var fail = new Exception(msg, iox);
                Debug.WriteLine(fail.Message, fail);
                throw fail;
            }
        }

        public virtual void Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                Entities.Remove(entity);

                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = string.Empty;

                foreach (var validationErrors in dbEx.EntityValidationErrors)
                    foreach (var validationError in validationErrors.ValidationErrors)
                        msg += Environment.NewLine + string.Format("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);

                var fail = new Exception(msg, dbEx);
                Debug.WriteLine(fail.Message, fail);
                throw fail;
            }
        }

        #endregion
    }
}