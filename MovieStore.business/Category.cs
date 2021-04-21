using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.Business
{
    public class Category : Base<Category>
    {
        #region Methods
        /// <summary>
        /// Get Categories
        /// </summary>
        /// <returns></returns>
        public override async Task<List<Category>> getRecords()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                return await db.Categories.Select(s => new Category { Id = s.ID, Name = s.NAME, DateCreated = s.DATE_CREATED, DateUpdated = s.DATE_UPDATED }).ToListAsync();
            }
        }

        /// <summary>
        /// Add Category
        /// </summary>
        /// <returns></returns>
        public override async Task addRecord()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                MovieStore.data.Category category = new MovieStore.data.Category();

                category.NAME = this.Name;
                category.DATE_CREATED = DateTime.Now;

                db.Categories.Add(category);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Update Category
        /// </summary>
        /// <returns></returns>
        public override async Task updateRecord()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                var category = await db.Categories.Where(w => w.ID == this.Id).Select(s => s).FirstOrDefaultAsync();

                category.NAME = this.Name;
                category.DATE_UPDATED = DateTime.Now;

                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Get Category
        /// </summary>
        /// <returns></returns>
        public override async Task<Category> getRecord()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                return await db.Categories.Where(w => w.ID == this.Id).Select(s => new Category 
                { 
                    Name = s.NAME
                }).FirstOrDefaultAsync();
            }
        }
        #endregion
    }
}
