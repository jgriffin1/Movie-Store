using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.business
{
  public class Rental
  {
    public int Id { get; set; }
    public int CustomerID { get; set; }
    public Member Customer { get; set; }
    public List<Movie> Movies { get; set; }
    public Payment PaymentInfo { get; set; }
    public DateTime RentalDate { get; set; }

    public async Task addRental()
    {
      using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
      {
        using (var transaction = db.Database.BeginTransaction())
        {
          try
          {
            //creates rental
            MovieStore.data.Rental rental = new MovieStore.data.Rental();

            rental.Customer_Id = this.CustomerID;
            rental.Rental_Date = DateTime.Now;

            db.Rentals.Add(rental);
            await db.SaveChangesAsync();

            //loops through all the movies costomer selected
            foreach (var item in this.Movies)
            {
              MovieStore.data.MovieRental movieRental = new MovieStore.data.MovieRental();

              movieRental.Movie_Id = item.Id;
              movieRental.Rental_Id = rental.Id;

              db.MovieRentals.Add(movieRental);
              await db.SaveChangesAsync();

              // decrements number of copies of each selected movie
              var movie = db.Movies.Where(w => w.Id == item.Id).FirstOrDefault();

              if (movie != null)
              {
                movie.Number_Of_Copies = movie.Number_Of_Copies - 1;

                if (movie.Number_Of_Copies < 0)
                {
                  movie.Number_Of_Copies = 0;
                }
                await db.SaveChangesAsync();
              }

              //create payment record
              MovieStore.data.Payment payment = new MovieStore.data.Payment();

              payment.Amount_Due = this.PaymentInfo.AmountDue;
              payment.Is_Paid = true;
              payment.Payment_Date = DateTime.Now;
              payment.Rental_Id = rental.Id;
              payment.Payment_Type_Id = this.PaymentInfo.PaymentTypeInformation.Id;

              db.Payments.Add(payment);
              await db.SaveChangesAsync();

              //create payment info record
              MovieStore.data.PaymentInfo paymentInfo = new MovieStore.data.PaymentInfo();
              paymentInfo.Payment_Id = payment.Id;
              if (this.PaymentInfo.PaymentInformation.CreditCardTypeInformation.Id > 0)
              {
                paymentInfo.Credit_Card_Type_Id = this.PaymentInfo.PaymentInformation.CreditCardTypeInformation.Id;
              }
              if (this.PaymentInfo.PaymentInformation.CheckNumber > 0)
              {
                paymentInfo.Check_Number = this.PaymentInfo.PaymentInformation.CheckNumber;
              }
              paymentInfo.Payment_Date = DateTime.Now;
              paymentInfo.Payment_Status = "S";
              paymentInfo.Is_Void = false;
              paymentInfo.Rep_Id = Guid.NewGuid().ToString();

              db.PaymentInfoes.Add(paymentInfo);
              await db.SaveChangesAsync();

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

    public async Task<Rental> getRental()
    {
      using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
      {
        return await db.Rentals.Where(w => w.Id == this.Id).Select(s => new Rental
        {
          Id = s.Id,
          RentalDate = s.Rental_Date,
          Customer = new Member
          {
            Id = s.Person.Id,
            FirstName = s.Person.First_Name,
            MiddleName = s.Person.Middle_Name,
            LastName = s.Person.Last_name,
            ProfilePicture = s.Person.Profile_Picture,
            Address = s.Person.Addresses.Where(a => a.Person_Id == s.Id).Select(x => new Address
            {
              AddressLine1 = x.Address_Line_1,
              AddressLine2 = x.Address_Line_2,
              City = x.City,
              IsPrimary = x.Is_Primary,
              State = x.State,
              ZipCode = x.Zip_Code

            }).FirstOrDefault()
          },
          Movies = s.MovieRentals.Where(m => m.Rental_Id == s.Id).Select(x => new Movie
          {
            Id = x.Movie_Id,
            Title = x.Movie.Title,
            NumberOfCopies = x.Movie.Number_Of_Copies
          }).ToList(),
          PaymentInfo = s.Payments.Where(p => p.Rental_Id == s.Id).Select(z => new Payment
          {
            AmountDue = z.Amount_Due,
            IsPaid = z.Is_Paid,
            PaymentTypeInformation = new PaymentType
            {
              Name = z.PaymentType.Name
            },
            PaymentInformation = z.PaymentInfoes.Where(i => i.Payment_Id == z.Id).Select(k => new PaymentInfo
            {
              CheckNumber = k.Check_Number,
              IsVoid = k.Is_Void,
              RepId = k.Rep_Id,
              PaymentStatus = k.Payment_Status,
              CreditCardTypeInformation = new CreditCardType
              {
                Name = k.CreditCardType.Name
              }
            }).FirstOrDefault()
          }).FirstOrDefault()
        }).FirstOrDefaultAsync();
      }
    }
    public async Task<List<Rental>> searchRentals()
    {
      using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
      {
        var rentals = db.Rentals.Select(s => s);

        if (this.Customer != null)
        {
          if (!string.IsNullOrEmpty(this.Customer.FirstName))
          {
            rentals = rentals.Where(w => w.Person.First_Name.StartsWith(this.Customer.FirstName));
          }
          if (!string.IsNullOrEmpty(this.Customer.LastName))
          {
            rentals = rentals.Where(w => w.Person.Last_name.StartsWith(this.Customer.LastName));
          }
        }
        if (this.RentalDate != null && this.RentalDate > DateTime.MinValue && this.RentalDate < DateTime.Now.AddMinutes(1))
        {
          rentals = rentals.Where(w => w.Rental_Date == this.RentalDate);
        }

        if (this.Movies != null && this.Movies.Count > 0)
        {
          rentals = rentals.Where(w => w.MovieRentals.Any(a => a.Movie_Id == this.Movies.FirstOrDefault().Id));
        }

        return await rentals.Select(s => new Rental
        {
          Id = s.Id,
          RentalDate = s.Rental_Date,
          Customer = new Member { Id = s.Person.Id, FirstName = s.Person.First_Name, MiddleName = s.Person.Middle_Name, LastName = s.Person.Last_name },
          Movies = s.MovieRentals.Where(m => m.Rental_Id == s.Id).Select(x => new Movie { Id = x.Movie_Id, Title = x.Movie.Title, }).ToList()
        }).ToListAsync();
      }
    }
  }
}
