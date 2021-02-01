using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.business
{
    class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }


        /// <summary>
        /// Default Constructor
        /// </summary>
        public Category()
        {

        }
        /// <summary>
        /// Constructor with parameter of type string
        /// </summary>
        /// <param name="name"></param>
        public Category(string name)
        {
            this.Name = name;
        }
        public void AddCategory()
        {

        }
        public async Task<List<Category>> GetCategories()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                return await db.Categories.Select(s => new Category { Id = s.Id, Name = s.Name }).ToListAsync();
            }
        }
        public async Task  addCategory()
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
        public async Task updateCategory()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                var category = await db.Categories.Where(w=>w.Id==this.Id).Select(s => s).FirstOrDefaultAsync();
                category.Name = this.Name;
                category.Date_Updated = DateTime.Now;
                await db.SaveChangesAsync();
            }
        }
    }
}
