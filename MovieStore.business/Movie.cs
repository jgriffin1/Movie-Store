using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.business
{
  public class Movie
  {
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int ReleaseYear { get; set; }
    public string Rating { get; set; }
    public int NumberOfCopies { get; set; }
    public byte[] Cover { get; set; }

    public List<Actor> Actors { get; set; }

    /// <summary>
    /// Add movie
    /// </summary>
    /// <returns></returns>
    public async Task addMovie()
    {
      using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
      {
        MovieStore.data.Movie movie = new MovieStore.data.Movie();
        movie.Cover = this.Cover;
        movie.Date_Created = DateTime.Now;
        movie.Description = this.Description;
        movie.Title = this.Title;
        movie.Number_Of_Copies = this.NumberOfCopies;
        movie.Rating = this.Rating;
        movie.Release_Year = this.ReleaseYear;

        db.Movies.Add(movie);
        await db.SaveChangesAsync();

      }
    }
    /// <summary>
    /// Update movie cover
    /// </summary>
    /// <returns></returns>
    public async Task updateMovieCover()
    {
      using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
      {
        var movie = await db.Movies.Where(w => w.Id == this.Id).FirstOrDefaultAsync();

        if (movie != null)
        {
          movie.Cover = this.Cover;
          movie.Date_Updated = DateTime.Now;

          await db.SaveChangesAsync();
        }
      }
    }
    /// <summary>
    /// update number of copies
    /// </summary>
    /// <returns></returns>
    public async Task updateNumberOfCopies()
    {
      using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
      {
        var movie = await db.Movies.Where(w => w.Id == this.Id).FirstOrDefaultAsync();

        if (movie != null)
        {
          movie.Number_Of_Copies = this.NumberOfCopies;
          movie.Date_Updated = DateTime.Now;

          await db.SaveChangesAsync();
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
        var movie = await db.Movies.Where(w => w.Id == this.Id).FirstOrDefaultAsync();

        if (movie != null)
        {
          movie.Title = this.Title;
          movie.Description = this.Description;
          movie.Rating = this.Rating;
          movie.Release_Year = this.ReleaseYear;
          movie.Date_Updated = DateTime.Now;
          await db.SaveChangesAsync();
        }
      }
    }
    /// <summary>
    /// add actors
    /// </summary>
    /// <returns></returns>
    public async Task addActors()
    {
      using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
      {

        if(this.Actors!=null && this.Actors.Count > 0)
        {
          using (var transaction = db.Database.BeginTransaction())
          {
            try
            {
              foreach(var item in this.Actors)
              {
                MovieStore.data.MovieActor movieActor = new MovieStore.data.MovieActor();

                movieActor.Actor_Id = item.Id;
                movieActor.Movie_Id = this.Id;
                movieActor.Date_Created = DateTime.Now;

                await db.SaveChangesAsync();
              }
              transaction.Commit();
            }
            catch(Exception ex)
            {
              transaction.Rollback();
              throw ex;
            }
          }
        }
      }
    }

  }
}
