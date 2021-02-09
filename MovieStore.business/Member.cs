using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.business
{
    public class Member : Person
    {
        public bool IsActive { get; set; }
        public string UserName { get; set; }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        public byte[] ProfilePicture { get; set; }
        public int RoleId { get; set; }
        public Address address { get; set; }
        public List<Phone> phones { get; set; }
        
        public override async Task addPerson()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                MovieStore.data.Person member = new MovieStore.data.Person();
                member.First_Name = this.FirstName;
                member.Middle_Name = this.MiddleName;
                member.Last_name = this.LastName;
                member.Is_Active = this.IsActive;
                member.UserName = this.UserName;
                member.Password = this.Password;
                member.Profile_Picture = this.ProfilePicture;
                member.Role_Id = this.RoleId;
                member.Date_Created = DateTime.Now;

                member.Addresses.Add(new MovieStore.data.Address
                {
                    Address_Line_1 = this.address.AddressLine1,
                    Address_Line_2 = this.address.AddressLine2,
                    City = this.address.City,
                    Zip_Code = this.address.ZipCode,
                    State = this.address.State,
                    Is_Primary = this.address.IsPrimary,
                    Date_Created = DateTime.Now

                });

                if(this.phones != null && this.phones.Count > 0)
                {
                    foreach (var item in this.phones)
                    {
                        member.Phones.Add(new MovieStore.data.Phone
                        {
                            Number = item.PhoneNumber,
                            Phone_Type_ID = item.PhoneTypeId,
                            Date_Created = DateTime.Now

                        });
                    }
                }
                
                db.People.Add(member);
                await db.SaveChangesAsync();

            }
        }

        public override async Task updatePerson()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                var member = await db.People.Where(w => Id == this.Id).FirstOrDefaultAsync();
                if (member != null)
                {
                    member.First_Name = this.FirstName;
                    member.Middle_Name = this.MiddleName;
                    member.Last_name = this.LastName;
                    member.Profile_Picture = this.ProfilePicture;
                    member.Date_Updated = DateTime.Now;
                }
            }
        }
        public async Task updateSecurityInfo()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                var member = await db.People.Where(w => w.Id == this.Id).FirstOrDefaultAsync();
                if (member != null)
                {
                    member.UserName = this.UserName;
                    member.Password = this.password;
                    member.Date_Updated = DateTime.Now;
                }
            }

        }
        public async Task updatePhoneInfo()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                using(var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var phones = await db.Phones.Where(w => w.Person_Id == this.Id).ToListAsync();
                        if (phones != null)
                        {
                            db.Phones.RemoveRange(phones);
                            await db.SaveChangesAsync();
                        }
                        if(this.phones!=null && this.phones.Count > 0)
                        {
                            foreach(Phone item in this.phones)
                            {
                                MovieStore.data.Phone phone = new MovieStore.data.Phone();
                                phone.Number = item.PhoneNumber;
                                phone.Phone_Type_ID = item.PhoneTypeId;
                                phone.Person_Id = this.Id;
                                db.Phones.Add(phone);
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
    }
}
