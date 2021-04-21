using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Business
{
    public class CreditCardType : Base<CreditCardType>
    {
        /// <summary>
        /// Add Credit Card Type
        /// </summary>
        /// <returns></returns>
        public override async Task addRecord()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                MovieStore.data.CreditCardType creditCardType = new MovieStore.data.CreditCardType();

                creditCardType.NAME = this.Name;

                db.CreditCardTypes.Add(creditCardType);
                await db.SaveChangesAsync();
            }
        }

        public override async Task<CreditCardType> getRecord()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                return await db.CreditCardTypes.Where(w => w.ID == this.Id).Select(s => new CreditCardType { Name = s.NAME }).FirstOrDefaultAsync();
            }
        }

        /// <summary>
        /// Get Credit Card Types
        /// </summary>
        /// <returns></returns>
        public override async Task<List<CreditCardType>> getRecords()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                return await db.CreditCardTypes.Select(s => new CreditCardType { Id = s.ID, Name = s.NAME }).ToListAsync();
            }
        }

        /// <summary>
        /// Update Credit Card Type
        /// </summary>
        /// <returns></returns>
        public override async Task updateRecord()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                var creditCardType = await db.CreditCardTypes.Where(w => w.ID == this.Id).Select(s => s).FirstOrDefaultAsync();

                creditCardType.NAME = this.Name;

                await db.SaveChangesAsync();
            }
        }
    }
}
