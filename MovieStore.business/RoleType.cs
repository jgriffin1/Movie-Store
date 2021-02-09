using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.business
{
  public class Role : Base<Role>
  {
    /// <summary>
    /// add role 
    /// </summary>
    /// <returns></returns>
    public override async Task addRecord()
    {
      using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
      {
        MovieStore.data.Role role = new MovieStore.data.Role();
        role.Name = this.Name;

        db.Roles.Add(role);
        await db.SaveChangesAsync();
      }
    }
    /// <summary>
    /// update role 
    /// </summary>
    /// <returns></returns>
    public override async Task<List<Role>> getRecords()
    {
      using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
      {
        return await db.Roles.Select(s => new Role { Id = s.Id, Name = s.Name }).ToListAsync();
      }
    }

    /// <summary>
    /// update role 
    /// </summary>
    /// <returns></returns>
    public override async Task updateRecord()
    {
      using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
      {
        var role = await db.Categories.Where(w => w.Id == this.Id).Select(s => s).FirstOrDefaultAsync();
        role.Name = this.Name;
        role.Date_Updated = DateTime.Now;
        await db.SaveChangesAsync();
      }
    }
  }
}
