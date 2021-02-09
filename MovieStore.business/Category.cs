using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.business
{
    class Category : Base<Category>
    {


        #region Method

        public override async Task<List<Category>> getRecords()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                return await db.Categories.Select(s => new Category { Id = s.Id, Name = s.Name }).ToListAsync();
            }
        }
        public override async Task addRecord()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                MovieStore.data.Category category = new MovieStore.data.Category();
                category.Name = this.Name;
                category.Date_Created = DateTime.Now;

                db.Categories.Add(category);
                await db.SaveChangesAsync();
            }
        }
        public override async Task updateRecord()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                var category = await db.Categories.Where(w=>w.Id==this.Id).Select(s => s).FirstOrDefaultAsync();
                category.Name = this.Name;
                category.Date_Updated = DateTime.Now;
                await db.SaveChangesAsync();
            }
        }
        #endregion Method
    }
}
