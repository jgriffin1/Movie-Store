using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Business
{
    public class PaymentType : Base<PaymentType>
    {
        /// <summary>
        /// Add Payment Type
        /// </summary>
        /// <returns></returns>
        public override async Task addRecord()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                MovieStore.data.PaymentType paymentType = new MovieStore.data.PaymentType();

                paymentType.NAME = this.Name;

                db.PaymentTypes.Add(paymentType);
                await db.SaveChangesAsync();
            }
        }

        public override async Task<PaymentType> getRecord()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                return await db.PaymentTypes.Where(w => w.ID == this.Id).Select(s => new PaymentType
                {
                    Name = s.NAME
                }).FirstOrDefaultAsync();
            }
        }

        /// <summary>
        /// Get Payment Types
        /// </summary>
        /// <returns></returns>
        public override async Task<List<PaymentType>> getRecords()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                return await db.PaymentTypes.Select(s => new PaymentType { Id = s.ID, Name = s.NAME }).ToListAsync();
            }
        }

        /// <summary>
        /// Update Payment Type
        /// </summary>
        /// <returns></returns>
        public override async Task updateRecord()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                var paymentType = await db.PaymentTypes.Where(w => w.ID == this.Id).Select(s => s).FirstOrDefaultAsync();

                paymentType.NAME = this.Name;

                await db.SaveChangesAsync();
            }
        }
    }
}
