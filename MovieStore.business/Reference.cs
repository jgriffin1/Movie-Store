using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Business
{
    // Generic Abstract Class
    public abstract class Base<T>
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Date Created
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Date Updated
        /// </summary>
        public DateTime? DateUpdated { get; set; }

        /// <summary>
        /// Add Record
        /// </summary>
        /// <returns></returns>
        public abstract Task addRecord();

        /// <summary>
        /// Update Record
        /// </summary>
        /// <returns></returns>
        public abstract Task updateRecord();

        /// <summary>
        /// Get Records
        /// </summary>
        /// <returns></returns>
        public abstract Task<List<T>> getRecords();

        /// <summary>
        /// Get Record
        /// </summary>
        /// <returns></returns>
        public abstract Task<T> getRecord();
    }
}
