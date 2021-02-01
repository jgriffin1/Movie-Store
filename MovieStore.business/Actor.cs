using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.business
{
    public class Actor : Person
    {
        /// <summary>
        /// Add Person
        /// </summary>
        /// <returns></returns>
        public override async Task addPerson()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                MovieStore.data.Actor actor = new MovieStore.data.Actor();
                actor.First_Name = this.FirstName;
                actor.Middle_Name = this.MiddleName;
                actor.Last_Name = this.LastName;
                actor.Date_Created = DateTime.Now;

                db.Actors.Add(actor);
                await db.SaveChangesAsync();

            }
        }
        /// <summary>
        /// Get Actors
        /// </summary>
        /// <returns></returns>
        public async Task<List<Actor>> getActors()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                return await db.Actors.Select(s => new Actor 
                {
                    Id = s.Id,
                    FirstName = s.First_Name,
                    MiddleName = s.Middle_Name,
                    LastName = s.Last_Name
                }).ToListAsync();

            }
        }
        /// <summary>
        /// Search Actors
        /// </summary>
        /// <returns></returns>
        public async Task<List<Actor>> searchActors()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                var actors = db.Actors.Select(s => s);
                if (!string.IsNullOrEmpty(this.FirstName))
                {
                    actors = actors.Where(w => w.First_Name.StartsWith(this.FirstName));
                }
                if (!string.IsNullOrEmpty(this.MiddleName))
                {
                    actors = actors.Where(w => w.Middle_Name.StartsWith(this.MiddleName));
                }
                if (!string.IsNullOrEmpty(this.LastName))
                {
                    actors = actors.Where(w => w.Last_Name.StartsWith(this.LastName));
                }
                return await actors.Select(s => new Actor
                {
                    Id = s.Id,
                    FirstName = s.First_Name,
                    MiddleName = s.Middle_Name,
                    LastName = s.Last_Name
                }).ToListAsync();
            }
        }
        /// <summary>
        /// Get Actor
        /// </summary>
        /// <returns></returns>
        public  async Task<Actor> getActor()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                return await db.Actors.Where(w => w.Id == this.Id).Select(s => new Actor 
                {
                    Id=s.Id,
                    FirstName=s.First_Name,
                    MiddleName=s.Middle_Name,
                    LastName=s.Last_Name
                  

                }).FirstOrDefaultAsync();
            }
        }

        public override async Task updatePerson()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                var actor = await db.Actors.Where(w => w.Id == this.Id).Select(s => s).FirstOrDefaultAsync();
                actor.First_Name = this.FirstName;
                actor.Middle_Name = this.MiddleName;
                actor.Last_Name = this.LastName;
                actor.Date_Updated = DateTime.Now;

                await db.SaveChangesAsync();
            }
        }
    }
}
