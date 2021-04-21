using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Business
{
    public class Role : Base<Role>
    {
        /// <summary>
        /// Add Role
        /// </summary>
        /// <returns></returns>
        public override async Task addRecord()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                MovieStore.data.Role roles = new MovieStore.data.Role();

                roles.NAME = this.Name;

                db.Roles.Add(roles);
                await db.SaveChangesAsync();
            }
        }

        public override Task<Role> getRecord()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get Roles
        /// </summary>
        /// <returns></returns>
        public override async Task<List<Role>> getRecords()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                return await db.Roles.OrderByDescending(o => o.NAME).Select(s => new Role { Id = s.ID, Name = s.NAME }).ToListAsync();
            }
        }

        /// <summary>
        /// Update Role
        /// </summary>
        /// <returns></returns>
        public override async Task updateRecord()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                var role = await db.Roles.Where(w => w.ID == this.Id).Select(s => s).FirstOrDefaultAsync();

                role.NAME = this.Name;

                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Get User Role Id
        /// </summary>
        /// <returns></returns>
        public async Task<int> getUserRoleId()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                return await db.Roles.Where(w => w.NAME.Equals("User")).Select(s => s.ID).FirstOrDefaultAsync();
            }
        }
    }
}
