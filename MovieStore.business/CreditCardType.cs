using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.business
{
  public class CreditCardType : Base<CreditCardType>
  {
    /// <summary>
    /// Add credit card record
    /// </summary>
    /// <returns></returns>
    public override async Task addRecord()
    {
      using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
      {
        MovieStore.data.CreditCardType creditCardType = new MovieStore.data.CreditCardType();
        creditCardType.Name = this.Name;

        db.CreditCardTypes.Add(creditCardType);
        await db.SaveChangesAsync();
      }
    }
    /// <summary>
    /// get credit card type
    /// </summary>
    /// <returns></returns>
    public override async Task<List<CreditCardType>> getRecords()
    {
      using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
      {
        return await db.CreditCardTypes.Select(s => new CreditCardType { Id = s.Id, Name = s.Name }).ToListAsync();
      }
    }
    /// <summary>
    /// update credit card type
    /// </summary>
    /// <returns></returns>
    public override async Task updateRecord()
    {
      using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
      {
        var creditCardType = await db.Categories.Where(w => w.Id == this.Id).Select(s => s).FirstOrDefaultAsync();
        creditCardType.Name = this.Name;
        creditCardType.Date_Updated = DateTime.Now;
        await db.SaveChangesAsync();
      }
    }
  }
}
