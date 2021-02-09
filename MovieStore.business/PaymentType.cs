using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.business
{
  public class PaymentType : Base<PaymentType>
  {
   /// <summary>
   /// add payment type
   /// </summary>
   /// <returns></returns>
    public override async Task addRecord()
    {
      using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
      {
        MovieStore.data.PaymentType paymentType = new MovieStore.data.PaymentType();
        paymentType.Name = this.Name;

        db.PaymentTypes.Add(paymentType);
        await db.SaveChangesAsync();
      }
    }
    /// <summary>
    /// update payment type
    /// </summary>
    /// <returns></returns>
    public override async Task<List<PaymentType>> getRecords()
    {
      using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
      {
        return await db.PaymentTypes.Select(s => new PaymentType { Id = s.Id, Name = s.Name }).ToListAsync();
      }
    }

    /// <summary>
    /// update payment type
    /// </summary>
    /// <returns></returns>
    public override async Task updateRecord()
    {
      using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
      {
        var paymentType = await db.Categories.Where(w => w.Id == this.Id).Select(s => s).FirstOrDefaultAsync();
        paymentType.Name = this.Name;
        paymentType.Date_Updated = DateTime.Now;
        await db.SaveChangesAsync();
      }
    }
  }
}
