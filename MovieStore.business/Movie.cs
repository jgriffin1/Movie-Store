using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Business
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public bool ActiveOnly { get; set; }

        public string Description { get; set; }

        public int ReleaseYear { get; set; }

        public string Rating { get; set; }

        public int NumberOfCopies { get; set; }

        public bool IsRemoved { get; set; }

        public byte[] Cover { get; set; }

        public List<Actor> Actors { get; set; }

        /// <summary>
        /// Add Movie
        /// </summary>
        /// <returns></returns>
        public async Task addMovie()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        MovieStore.data.Movie movie = new MovieStore.data.Movie();

                        movie.COVER = this.Cover;
                        movie.DATE_CREATED = DateTime.Now;
                        movie.DESCRIPTION = this.Description;
                        movie.TITLE = this.Title;
                        movie.NUMBER_OF_COPIES = this.NumberOfCopies;
                        movie.RATING = this.Rating;
                        movie.RELEASE_YEAR = this.ReleaseYear;
                        movie.IS_REMOVED = false;

                        db.Movies.Add(movie);

                        await db.SaveChangesAsync();

                        if (this.Actors != null && this.Actors.Count > 0)
                        {
                            foreach (var item in this.Actors)
                            {
                                MovieStore.data.MovieActor movieActor = new data.MovieActor();

                                movieActor.ACTOR_ID = item.Id;
                                movieActor.MOVIE_ID = movie.ID;
                                movieActor.DATE_CREATED = DateTime.Now;

                                db.MovieActors.Add(movieActor);

                                await db.SaveChangesAsync();
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
              
            }
        }

        /// <summary>
        /// Update Movie
        /// </summary>
        /// <returns></returns>
        public async Task updateMovie()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var movie = await db.Movies.Where(w => w.ID == this.Id).FirstOrDefaultAsync();

                        if (movie != null)
                        {
                            if (this.Cover != null)
                            {
                                movie.COVER = this.Cover;
                            }
                            movie.DATE_UPDATED = DateTime.Now;
                            movie.DESCRIPTION = this.Description;
                            movie.TITLE = this.Title;
                            movie.NUMBER_OF_COPIES = this.NumberOfCopies;
                            movie.RATING = this.Rating;
                            movie.RELEASE_YEAR = this.ReleaseYear;

                            await db.SaveChangesAsync();

                            var movieActors = await db.MovieActors.Where(w => w.MOVIE_ID == this.Id).ToListAsync();

                            if (movieActors != null && movieActors.Count > 0)
                            {
                                db.MovieActors.RemoveRange(movieActors);

                                await db.SaveChangesAsync();
                            }

                            if (this.Actors != null && this.Actors.Count > 0)
                            {
                                foreach (var item in this.Actors)
                                {
                                    MovieStore.data.MovieActor movieActor = new data.MovieActor();

                                    movieActor.ACTOR_ID = item.Id;
                                    movieActor.MOVIE_ID = movie.ID;
                                    movieActor.DATE_CREATED = DateTime.Now;

                                    db.MovieActors.Add(movieActor);

                                    await db.SaveChangesAsync();
                                }
                            }

                            transaction.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

        /// <summary>
        /// Remove Movie
        /// </summary>
        /// <returns></returns>
        public void removeMovie()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                var movie = db.Movies.Where(w => w.ID == this.Id).FirstOrDefault();

                if (movie != null)
                {
                    movie.IS_REMOVED = true;
                    movie.DATE_UPDATED = DateTime.Now;

                    db.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Readd Movie
        /// </summary>
        /// <returns></returns>
        public void readdMovie()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                var movie = db.Movies.Where(w => w.ID == this.Id).FirstOrDefault();

                if (movie != null)
                {
                    movie.IS_REMOVED = false;
                    movie.DATE_UPDATED = DateTime.Now;

                    db.SaveChanges();
                }
            }
        }

        /// <summary>
        /// Get Movie
        /// </summary>
        /// <returns></returns>
        public async Task<Movie> getMovie()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                return await db.Movies.Where(w => w.ID == this.Id).Select(s => new Movie 
                { 
                    Cover = s.COVER,
                    Description = s.DESCRIPTION,
                    Id = s.ID,
                    NumberOfCopies = s.NUMBER_OF_COPIES,
                    Rating = s.RATING,
                    ReleaseYear = s.RELEASE_YEAR,
                    Title = s.TITLE,
                    Actors = s.MovieActors.Where(w => w.MOVIE_ID == this.Id).Select(x => new Actor 
                    { 
                        Id = x.ACTOR_ID, 
                        FirstName = x.Actor.FIRST_NAME, 
                        MiddleName = x.Actor.MIDDLE_NAME,
                        LastName = x.Actor.LAST_NAME
                    }).ToList()
                }).FirstOrDefaultAsync();
            }
        }

        /// <summary>
        /// Get Movies
        /// </summary>
        /// <returns></returns>
        public async Task<List<Movie>> getMovies()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                return await db.Movies.Select(s => new Movie
                {
                    Cover = s.COVER,
                    Description = s.DESCRIPTION,
                    Id = s.ID,
                    NumberOfCopies = s.NUMBER_OF_COPIES,
                    Rating = s.RATING,
                    ReleaseYear = s.RELEASE_YEAR,
                    Title = s.TITLE,
                    IsRemoved = s.IS_REMOVED.Value,
                    Actors = s.MovieActors.Where(w => w.MOVIE_ID == s.ID).Select(x => new Actor
                    {
                        Id = x.ACTOR_ID,
                        FirstName = x.Actor.FIRST_NAME,
                        MiddleName = x.Actor.MIDDLE_NAME,
                        LastName = x.Actor.LAST_NAME
                    }).ToList()
                }).ToListAsync();
            }
        }

        /// <summary>
        /// Browse Movies
        /// </summary>
        /// <returns></returns>
        public async Task<List<Movie>> browseMovies()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                return await db.Movies.Where(w => w.IS_REMOVED == false).Select(s => new Movie
                {
                    Cover = s.COVER,
                    Description = s.DESCRIPTION,
                    Id = s.ID,
                    NumberOfCopies = s.NUMBER_OF_COPIES,
                    Rating = s.RATING,
                    ReleaseYear = s.RELEASE_YEAR,
                    Title = s.TITLE,
                    IsRemoved = s.IS_REMOVED.Value,
                    Actors = s.MovieActors.Where(w => w.MOVIE_ID == s.ID).Select(x => new Actor
                    {
                        Id = x.ACTOR_ID,
                        FirstName = x.Actor.FIRST_NAME,
                        MiddleName = x.Actor.MIDDLE_NAME,
                        LastName = x.Actor.LAST_NAME
                    }).ToList()
                }).ToListAsync();
            }
        }

        /// <summary>
        /// Search Movies
        /// </summary>
        /// <returns></returns>
        public async Task<List<Movie>> searchMovies()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                var movies = db.Movies.Select(s => s);

                if (this.ActiveOnly == true)
                {
                    movies = movies.Where(w => w.IS_REMOVED == false);
                }

                if (!string.IsNullOrEmpty(this.Title))
                {
                    movies = movies.Where(w => w.TITLE.Contains(this.Title));
                }

                if (!string.IsNullOrEmpty(this.Rating))
                {
                    movies = movies.Where(w => w.RATING.Equals(this.Rating));
                }

                if (this.ReleaseYear >= 1888)
                {
                    movies = movies.Where(w => w.RELEASE_YEAR == this.ReleaseYear);
                }

                if (this.Actors != null && this.Actors.Count > 0)
                {
                    foreach (var item in this.Actors)
                    {
                        movies = movies.Where(w => w.MovieActors.Any(a => a.ACTOR_ID == item.Id));
                    }
                }

                return await movies.Select(s => new Movie 
                {
                    Cover = s.COVER,
                    Description = s.DESCRIPTION,
                    Id = s.ID,
                    NumberOfCopies = s.NUMBER_OF_COPIES,
                    Rating = s.RATING,
                    IsRemoved = s.IS_REMOVED.Value,
                    ReleaseYear = s.RELEASE_YEAR,
                    Title = s.TITLE,
                    Actors = s.MovieActors.Where(w => w.MOVIE_ID == s.ID).Select(x => new Actor
                    {
                        Id = x.ACTOR_ID,
                        FirstName = x.Actor.FIRST_NAME,
                        MiddleName = x.Actor.MIDDLE_NAME,
                        LastName = x.Actor.LAST_NAME
                    }).ToList()
                }).ToListAsync();
            }
        }
    }
}
