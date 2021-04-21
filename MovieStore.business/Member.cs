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
    public Address Address { get; set; }
    public List<Phone> Phones { get; set; }
    public Phone phone { get; set; }
    public async Task<Member> getMember()
    {
      using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
      {
        return await db.People.Where(w => w.Id == this.Id).Select(s => new Member
        {
          Id = s.Id,
          FirstName = s.First_Name,
          MiddleName = s.Middle_Name,
          LastName = s.Last_name,
          ProfilePicture = s.Profile_Picture,
        }).FirstOrDefaultAsync();
      }
    }
    public async Task<List<Phone>> getPhones()
    {
      using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
      {
        return await db.Phones.Where(w => w.Person_Id == this.Id).Select(s => new Phone
        {
          Id = s.Id,
          PhoneNumber = s.Number,
          PhoneType = s.PhoneType.Name,
          PhoneTypeId = s.Phone_Type_ID
        }).ToListAsync();
      }
    }
    public async Task<Phone> getPhone()
    {
      using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
      {
        return await db.Phones.Where(w => w.Id == this.phone.Id).Select(s => new Phone { }).FirstOrDefaultAsync();
      }
    }
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
          Address_Line_1 = this.Address.AddressLine1,
          Address_Line_2 = this.Address.AddressLine2,
          City = this.Address.City,
          Zip_Code = this.Address.ZipCode,
          State = this.Address.State,
          Is_Primary = this.Address.IsPrimary,
          Date_Created = DateTime.Now

        });

        if (this.Phones != null && this.Phones.Count > 0)
        {
          foreach (var item in this.Phones)
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

    public async Task<List<Address>> getAddresses()
    {
      using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
      {
        return await db.Addresses.Where(w => w.Person_Id == this.Id).Select(s => new Address
        {
          AddressLine1 = s.Address_Line_1,
          AddressLine2 = s.Address_Line_2,
          City = s.City,
          State = s.State,
          ZipCode = s.Zip_Code,
          Id = s.Id,
          IsPrimary = s.Is_Primary
        }).ToListAsync();
      }
    }
    public async Task addAddress()
    {
      using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
      {
        MovieStore.data.Address address = new data.Address();
        address.Person_Id = this.Id;
        address.Address_Line_1 = this.Address.AddressLine1;
        address.Address_Line_2 = this.Address.AddressLine2;
        address.City = this.Address.City;
        address.State = this.Address.State;
        address.Zip_Code = this.Address.ZipCode;
        address.Date_Created = DateTime.Now;
        address.Is_Primary = this.Address.IsPrimary;

        db.Addresses.Add(address);
        await db.SaveChangesAsync();

      }
    }
    public async Task<Address> getAddress()
    {
      using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
      {
        return await db.Addresses.Where(w => w.Id == this.Address.Id).Select(s => new Address
        {
          AddressLine1 = s.Address_Line_1,
          AddressLine2 = s.Address_Line_2,
          City = s.City,
          State = s.State,
          ZipCode = s.Zip_Code,
          Id = s.Id,
          IsPrimary = s.Is_Primary
        }).FirstOrDefaultAsync();
      }
    }

    public async Task deleteAddress()
    {
      using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
      {
        var address = await db.Addresses.Where(w => w.Id == this.Address.Id && w.Person_Id == this.Id).FirstOrDefaultAsync();

        if (address != null)
        {
          db.Addresses.Remove(address);
          await db.SaveChangesAsync();
        }
      }
    }
    public async Task deletePhone()
    {
      using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
      {
        var phone = await db.Phones.Where(w => w.Id == this.phone.Id && w.Person_Id == this.Id).FirstOrDefaultAsync();

        if (phone != null)
        {
          db.Phones.Remove(phone);
          await db.SaveChangesAsync();
        }
      }
    }
    public async Task setAddressAsPrimary()
    {
      using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
      {
        using (var transaction = db.Database.BeginTransaction())
        {
          try
          {
            var addresses = await db.Addresses.Where(w => w.Person_Id == this.Id).ToListAsync();
            if (addresses != null && addresses.Count > 0)
            {
              foreach (var item in addresses)
              {
                item.Is_Primary = false;
                await db.SaveChangesAsync();
              }
            }
            var address = await db.Addresses.Where(w => w.Person_Id == this.Id && w.Id == this.Address.Id).FirstOrDefaultAsync();
            address.Is_Primary = true;
            await db.SaveChangesAsync();
            transaction.Commit();
          }
          catch (Exception e)
          {
            transaction.Rollback();
            throw e;
          }

        }

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
          if (this.ProfilePicture != null)
            member.Profile_Picture = this.ProfilePicture;
          member.Date_Updated = DateTime.Now;
        }
        db.SaveChanges();
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

    /// <summary>
    /// 
    /// Search Members
    /// </summary>
    /// <returns></returns>
    /// 

    public async Task<List<Member>> searchMembers()
    {
      using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
      {
        var members = db.People.Where(w => w.Role_Id == this.RoleId).Select(s => s);
        if (!string.IsNullOrEmpty(this.FirstName))
        {
          members = members.Where(w => w.First_Name.StartsWith(this.FirstName));
        }
        if (!string.IsNullOrEmpty(this.MiddleName))
        {
          members = members.Where(w => w.Middle_Name.StartsWith(this.MiddleName));
        }
        if (!string.IsNullOrEmpty(this.LastName))
        {
          members = members.Where(w => w.Last_name.StartsWith(this.LastName));
        }
        if (this.Address != null && !string.IsNullOrEmpty(this.Address.City))
        {
          members = members.Where(w => w.Addresses.Any(a => a.City.Contains(this.Address.City)));
        }
        if (this.Address != null && !string.IsNullOrEmpty(this.Address.State))
        {
          members = members.Where(w => w.Addresses.Any(a => a.State.Contains(this.Address.State)));
        }
        if (this.Address != null && !string.IsNullOrEmpty(this.Address.ZipCode))
        {
          members = members.Where(w => w.Addresses.Any(a => a.Zip_Code.Contains(this.Address.ZipCode)));
        }
        members = members.Where(w => w.Is_Active == this.IsActive);

        return await members.Select(s => new Member
        {
          Id = s.Id,
          FirstName = s.First_Name,
          MiddleName = s.Middle_Name,
          LastName = s.Last_name,
          IsActive = s.Is_Active,
          Address = new Address
          {
            AddressLine1 = s.Addresses.Where(w => w.Is_Primary == true).FirstOrDefault().Address_Line_1,
            AddressLine2 = s.Addresses.Where(w => w.Is_Primary == true).FirstOrDefault().Address_Line_2,
            City = s.Addresses.Where(w => w.Is_Primary == true).FirstOrDefault().City,
            State = s.Addresses.Where(w => w.Is_Primary == true).FirstOrDefault().State,
            ZipCode = s.Addresses.Where(w => w.Is_Primary == true).FirstOrDefault().Zip_Code,
          },
          Phones = s.Phones.Select(p => new Phone { PhoneNumber = p.Number }).ToList(),

        }).ToListAsync();
      }
    }
    public async Task<List<Member>> GetMembers()
    {
      using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
      {
        return await db.People.Where(w => w.Is_Active == true && w.Role_Id == this.RoleId).Select(s => new Member
        {
          Id = s.Id,
          FirstName = s.First_Name,
          MiddleName = s.Middle_Name,
          LastName = s.Last_name,
          IsActive = s.Is_Active,
          Address = new Address
          {
            AddressLine1 = s.Addresses.Where(w => w.Is_Primary == true).FirstOrDefault().Address_Line_1,
            AddressLine2 = s.Addresses.Where(w => w.Is_Primary == true).FirstOrDefault().Address_Line_2,
            City = s.Addresses.Where(w => w.Is_Primary == true).FirstOrDefault().City,
            State = s.Addresses.Where(w => w.Is_Primary == true).FirstOrDefault().State,
            ZipCode = s.Addresses.Where(w => w.Is_Primary == true).FirstOrDefault().Zip_Code,
          },
          Phones = s.Phones.Select(p => new Phone { PhoneNumber = p.Number }).ToList(),

        }).ToListAsync();
      }
    }
    public int Authenticate()
    {
      using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
      {
        return db.People.Where(w => w.UserName.Equals(this.UserName) && w.Password.Equals(this.Password)).Select(s => s.Id).FirstOrDefault();
      }
    }

    public async Task updateAddress()
    {
      using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
      {
        var address = await db.Addresses.Where(w => w.Id == this.Address.Id).FirstOrDefaultAsync();
        if (address != null)
        {
          address.Address_Line_1 = this.Address.AddressLine1;
          address.Address_Line_2 = this.Address.AddressLine2;
          address.City = this.Address.City;
          address.State = this.Address.State;
          address.Zip_Code = this.Address.ZipCode;
          address.Date_Updated = DateTime.Now;

          await db.SaveChangesAsync();
        }
      }
    }
    public async Task updatePhone()
    {
      using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
      {

        var phone = await db.Phones.Where(w => w.Id == this.phone.Id).FirstOrDefaultAsync();
        if (phone != null)
        {
          phone.Date_Updated = DateTime.Now;
          phone.Number = this.phone.PhoneNumber;
          phone.Phone_Type_ID = this.phone.PhoneTypeId;

          await db.SaveChangesAsync();
        }
      }
    }
    public async Task addPhone()
    {
      using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
      {
        MovieStore.data.Phone phone = new MovieStore.data.Phone
        {
          Date_Created = DateTime.Now,
          Number = this.phone.PhoneNumber,
          Phone_Type_ID = this.phone.PhoneTypeId,
          Person_Id = this.Id
        };
        db.Phones.Add(phone);
        await db.SaveChangesAsync();
      }
    }
  }
}
