using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStore.business
{
    class Category
    {
        #region Properties
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Date Created
        /// </summary>
        public DateTime DateCreated { get; set; }
        /// <summary>
        /// date updated
        /// </summary>
        public DateTime? DateUpdated { get; set; }
        #endregion

        #region Constructors
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
        #endregion

        #region Method
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
        #endregion Method
    }
}
