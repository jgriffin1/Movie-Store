using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Business
{
    public class Rental
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

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
                        if (this.Movies != null && this.Movies.Count > 0)
                        {
                            // Creates Rental
                            MovieStore.data.Rental rental = new MovieStore.data.Rental();

                            rental.CUSTOMER_ID = this.CustomerId;
                            rental.RENTAL_DATE = DateTime.Now;

                            db.Rentals.Add(rental);
                            await db.SaveChangesAsync();

                            // Loops through all the movies customer selected
                            foreach (var item in this.Movies)
                            {
                                // Creates Movie Rental Record
                                MovieStore.data.MovieRental movieRental = new MovieStore.data.MovieRental();

                                movieRental.MOVIE_ID = item.Id;
                                movieRental.RENTAL_ID = rental.ID;

                                db.MovieRentals.Add(movieRental);
                                await db.SaveChangesAsync();

                                // Decrement number of copies of each selected movie
                                var movie = db.Movies.Where(w => w.ID == item.Id).FirstOrDefault();

                                if (movie != null)
                                {
                                    movie.NUMBER_OF_COPIES = movie.NUMBER_OF_COPIES - 1;

                                    if (movie.NUMBER_OF_COPIES < 0)
                                    {
                                        movie.NUMBER_OF_COPIES = 0;
                                    }

                                    await db.SaveChangesAsync();
                                }

                                // Create Payment Record
                                MovieStore.data.Payment payment = new MovieStore.data.Payment();

                                payment.AMOUNT_DUE = this.PaymentInfo.AmountDue;
                                payment.IS_PAID = true;
                                payment.PAYMENT_DATE = DateTime.Now;
                                payment.RENTAL_ID = rental.ID;
                                payment.PAYMENT_TYPE_ID = this.PaymentInfo.PaymentTypeInformation.Id;

                                db.Payments.Add(payment);
                                await db.SaveChangesAsync();

                                // Create Payment Info Record
                                MovieStore.data.PaymentInfo paymentInfo = new MovieStore.data.PaymentInfo();

                                paymentInfo.PAYMENT_ID = payment.ID;
                                if (this.PaymentInfo.PaymentInformation.CreditCardTypeInformation.Id > 0)
                                {
                                    paymentInfo.CREDIT_CARD_TYPE_ID = this.PaymentInfo.PaymentInformation.CreditCardTypeInformation.Id;
                                }
                                if (this.PaymentInfo.PaymentInformation.CheckNumber > 0)
                                {
                                    paymentInfo.CHECK_NUMBER = this.PaymentInfo.PaymentInformation.CheckNumber;
                                }
                                paymentInfo.PAYMENT_DATE = DateTime.Now;
                                paymentInfo.PAYMENT_STATUS = "S";
                                paymentInfo.IS_VOID = false;
                                paymentInfo.REF_ID = Guid.NewGuid().ToString();

                                db.PaymentInfoes.Add(paymentInfo);
                                await db.SaveChangesAsync();
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

        public async Task<Rental> getRental()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                return await db.Rentals.Where(w => w.ID == this.Id).Select(s => new Rental 
                { 
                    Id = s.ID,
                    RentalDate = s.RENTAL_DATE,
                    Customer = new Member 
                    { 
                        Id = s.Person.ID, 
                        FirstName = s.Person.FIRST_NAME, 
                        MiddleName = s.Person.MIDDLE_NAME, 
                        LastName = s.Person.LAST_NAME, 
                        ProfilePicture = s.Person.PROFILE_PICTURE,
                        Address = s.Person.Addresses.Where(a => a.PERSON_ID == s.ID && a.IS_PRIMARY == true).Select(x => new Address 
                        { 
                            AddressLine1 = x.ADDRESS_LINE_1,
                            AddressLine2 = x.ADDRESS_LINE_2,
                            City = x.CITY,
                            IsPrimary = x.IS_PRIMARY,
                            State = x.STATE,
                            ZipCode = x.ZIP_CODE
                        }).FirstOrDefault()
                    },
                    Movies = s.MovieRentals.Where(m => m.RENTAL_ID == s.ID).Select(x => new Movie 
                    { 
                        Id = x.MOVIE_ID, 
                        Title = x.Movie.TITLE, 
                        NumberOfCopies = x.Movie.NUMBER_OF_COPIES 
                    }).ToList(),
                    PaymentInfo = s.Payments.Where(p => p.RENTAL_ID == s.ID).Select(z => new Payment 
                    { 
                        AmountDue = z.AMOUNT_DUE,
                        IsPaid = z.IS_PAID,
                        PaymentTypeInformation = new PaymentType 
                        { 
                            Name = z.PaymentType.NAME 
                        },
                        PaymentInformation = z.PaymentInfoes.Where(i => i.PAYMENT_ID == z.ID).Select(k => new PaymentInfo 
                        { 
                            CheckNumber = k.CHECK_NUMBER,
                            IsVoid = k.IS_VOID,
                            RefId = k.REF_ID,
                            PaymentStatus = k.PAYMENT_STATUS,
                            CreditCardTypeInformation = new CreditCardType { Name = k.CreditCardType.NAME } 
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
                        rentals = rentals.Where(w => w.Person.FIRST_NAME.StartsWith(this.Customer.FirstName));
                    }

                    if (!string.IsNullOrEmpty(this.Customer.LastName))
                    {
                        rentals = rentals.Where(w => w.Person.LAST_NAME.StartsWith(this.Customer.LastName));
                    }
                }

                if (this.RentalDate != null && this.RentalDate > DateTime.MinValue && this.RentalDate < DateTime.Now.AddMinutes(1))
                {
                    rentals = rentals.Where(w => w.RENTAL_DATE == this.RentalDate);
                }

                if (this.Movies != null && this.Movies.Count > 0)
                {
                    rentals = rentals.Where(w => w.MovieRentals.Any(a => a.MOVIE_ID == this.Movies.FirstOrDefault().Id));
                }

                return await rentals.Select(s => new Rental 
                { 
                    Id = s.ID,
                    RentalDate = s.RENTAL_DATE,
                    Customer = new Member { Id = s.Person.ID, FirstName = s.Person.FIRST_NAME,  MiddleName = s.Person.MIDDLE_NAME, LastName = s.Person.LAST_NAME },
                    Movies = s.MovieRentals.Where(m => m.RENTAL_ID == s.ID).Select(x => new Movie { Id = x.MOVIE_ID, Title = x.Movie.TITLE }).ToList()              
                }).ToListAsync();
            }
        }
    }
}
