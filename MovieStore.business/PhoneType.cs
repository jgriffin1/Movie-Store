using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.business
{
  public class PhoneType : Base<PhoneType>
  {
    /// <summary>
    /// Add phone type
    /// </summary>
    /// <returns></returns>
    public override async Task addRecord()
    {
      using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
      {
        MovieStore.data.PhoneType phoneType = new MovieStore.data.PhoneType();
        phoneType.Name = this.Name;

        db.PhoneTypes.Add(phoneType);
        await db.SaveChangesAsync();
      }
    }
    /// <summary>
    /// update phone type
    /// </summary>
    /// <returns></returns>
    public override async Task<List<PhoneType>> getRecords()
    {
      using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
      {
        return await db.PhoneTypes.Select(s => new PhoneType { Id = s.Id, Name = s.Name }).ToListAsync();
      }
    }

    /// <summary>
    /// update phone type
    /// </summary>
    /// <returns></returns>
    public override async Task updateRecord()
    {
      using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
      {
        var phoneType = await db.Categories.Where(w => w.Id == this.Id).Select(s => s).FirstOrDefaultAsync();
        phoneType.Name = this.Name;
        phoneType.Date_Updated = DateTime.Now;
        await db.SaveChangesAsync();
      }
    }
  }
}
