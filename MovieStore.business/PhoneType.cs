using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Business
{
    public class PhoneType : Base<PhoneType>
    {
        /// <summary>
        /// Add Phone Type
        /// </summary>
        /// <returns></returns>
        public override async Task addRecord()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                MovieStore.data.PhoneType phoneType = new MovieStore.data.PhoneType();

                phoneType.NAME = this.Name;

                db.PhoneTypes.Add(phoneType);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Get Phone Type
        /// </summary>
        /// <returns></returns>
        public override async Task<PhoneType> getRecord()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                return await db.PhoneTypes.Where(w => w.ID == this.Id).Select(s => new PhoneType
                {
                    Name = s.NAME
                }).FirstOrDefaultAsync();
            }
        }

        /// <summary>
        /// Get Phone Types
        /// </summary>
        /// <returns></returns>
        public override async Task<List<PhoneType>> getRecords()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                return await db.PhoneTypes.Select(s => new PhoneType { Id = s.ID, Name = s.NAME }).ToListAsync();
            }
        }

        /// <summary>
        /// Update Phone Type
        /// </summary>
        /// <returns></returns>
        public override async Task updateRecord()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                var phoneType = await db.PhoneTypes.Where(w => w.ID == this.Id).Select(s => s).FirstOrDefaultAsync();

                phoneType.NAME = this.Name;

                await db.SaveChangesAsync();
            }
        }
    }
}
