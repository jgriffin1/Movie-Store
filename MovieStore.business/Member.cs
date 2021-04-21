﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Business
{
    public class Member : Person
    {
        public bool IsActive { get; set; }
        public string UserName { get; set; }

        private string password;
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
            }
        }

        public byte[] ProfilePicture { get; set; }

        public int RoleId { get; set; }

        public string RoleName { get; set; }

        public Address Address { get; set; }

        public Phone Phone { get; set; }

        public List<Phone> Phones { get; set; }

        /// <summary>
        /// Authenticate
        /// </summary>
        /// <returns></returns>
        public Member authenticate()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                return db.People.Where(w => w.USERNAME.Equals(this.UserName) && w.PASSWORD.Equals(this.Password))
                    .Select(s => new Member { Id = s.ID, RoleId = s.ROLE_ID, RoleName = s.Role.NAME }).FirstOrDefault();
            }
        }

        /// <summary>
        /// Add Person
        /// </summary>
        /// <returns></returns>
        public override async Task addPerson()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                MovieStore.data.Person member = new MovieStore.data.Person();

                member.FIRST_NAME = this.FirstName;
                member.MIDDLE_NAME = this.MiddleName;
                member.LAST_NAME = this.LastName;
                member.IS_ACTIVE = this.IsActive;
                member.USERNAME = this.UserName;
                member.PASSWORD = this.Password;
                member.PROFILE_PICTURE = this.ProfilePicture;
                member.ROLE_ID = this.RoleId;
                member.DATE_CREATED = DateTime.Now;

                member.Addresses.Add(new MovieStore.data.Address
                {
                    ADDRESS_LINE_1 = this.Address.AddressLine1,
                    ADDRESS_LINE_2 = this.Address.AddressLine2,
                    CITY = this.Address.City,
                    ZIP_CODE = this.Address.ZipCode,
                    STATE = this.Address.State,
                    IS_PRIMARY = this.Address.IsPrimary,
                    DATE_CREATED = DateTime.Now
                });

                if (this.Phones != null && this.Phones.Count > 0)
                {
                    foreach (Phone item in this.Phones)
                    {
                        member.Phones.Add(new MovieStore.data.Phone
                        {
                            NUMBER = item.PhoneNumber,
                            PHONE_TYPE_ID = item.PhoneTypeId,
                            DATE_CREATED = DateTime.Now
                        });
                    }
                }

                db.People.Add(member);
                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Update Person
        /// </summary>
        /// <returns></returns>
        public override async Task updatePerson()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                var member = await db.People.Where(w => w.ID == this.Id).FirstOrDefaultAsync();

                if (member != null)
                {
                    member.FIRST_NAME = this.FirstName;
                    member.MIDDLE_NAME = this.MiddleName;
                    member.LAST_NAME = this.LastName;
                    if (this.ProfilePicture != null)
                    {
                        member.PROFILE_PICTURE = this.ProfilePicture;
                    }
                    member.DATE_UPDATED = DateTime.Now;

                    await db.SaveChangesAsync();
                }
            }
        }

        /// <summary>
        /// Delete Address
        /// </summary>
        /// <returns></returns>
        public async Task deleteAddress()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                var address = await db.Addresses.Where(w => w.ID == this.Address.Id && w.PERSON_ID == this.Id).FirstOrDefaultAsync();

                if (address != null)
                {
                    db.Addresses.Remove(address);
                    await db.SaveChangesAsync();
                }
            }
        }

        /// <summary>
        /// Delete Phone
        /// </summary>
        /// <returns></returns>
        public async Task deletePhone()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                var phone = await db.Phones.Where(w => w.ID == this.Phone.Id && w.PERSON_ID == this.Id).FirstOrDefaultAsync();

                if (phone != null)
                {
                    db.Phones.Remove(phone);
                    await db.SaveChangesAsync();
                }
            }
        }

        /// <summary>
        /// Set Address As Primary
        /// </summary>
        /// <returns></returns>
        public async Task setAddressAsPrimary()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var addresses = await db.Addresses.Where(w => w.PERSON_ID == this.Id).ToListAsync();

                        if (addresses != null && addresses.Count > 0)
                        {
                            foreach (var item in addresses)
                            {
                                item.IS_PRIMARY = false;

                                await db.SaveChangesAsync();
                            }
                        }

                        var address = await db.Addresses.Where(w => w.ID == this.Address.Id && w.PERSON_ID == this.Id).FirstOrDefaultAsync();

                        if (address != null)
                        {
                            address.IS_PRIMARY = true;
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

        /// <summary>
        /// Search Members
        /// </summary>
        /// <returns></returns>
        public async Task<List<Member>> searchMembers()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                var members = db.People.Where(w => w.ROLE_ID == this.RoleId).Select(s => s);

                if (!string.IsNullOrEmpty(this.FirstName))
                {
                    members = members.Where(w => w.FIRST_NAME.StartsWith(this.FirstName));
                }

                if (!string.IsNullOrEmpty(this.MiddleName))
                {
                    members = members.Where(w => w.MIDDLE_NAME.StartsWith(this.MiddleName));
                }

                if (!string.IsNullOrEmpty(this.LastName))
                {
                    members = members.Where(w => w.LAST_NAME.StartsWith(this.LastName));
                }

                if (this.Address != null && !string.IsNullOrEmpty(this.Address.City))
                {
                    members = members.Where(w => w.Addresses.Any(a => a.CITY.Contains(this.Address.City)));
                }

                if (this.Address != null && !string.IsNullOrEmpty(this.Address.State))
                {
                    members = members.Where(w => w.Addresses.Any(a => a.STATE.Equals(this.Address.State)));
                }

                if (this.Address != null && !string.IsNullOrEmpty(this.Address.ZipCode))
                {
                    members = members.Where(w => w.Addresses.Any(a => a.ZIP_CODE.Equals(this.Address.ZipCode)));
                }

                members = members.Where(w => w.IS_ACTIVE == this.IsActive);

                return await members.Select(s => new Member
                {
                    Id = s.ID,
                    FirstName = s.FIRST_NAME,
                    MiddleName = s.MIDDLE_NAME,
                    LastName = s.LAST_NAME,
                    IsActive = s.IS_ACTIVE,
                    ProfilePicture = s.PROFILE_PICTURE,
                    Address = new Address
                    {
                        AddressLine1 = s.Addresses.Where(w => w.IS_PRIMARY == true).FirstOrDefault().ADDRESS_LINE_1,
                        AddressLine2 = s.Addresses.Where(w => w.IS_PRIMARY == true).FirstOrDefault().ADDRESS_LINE_2,
                        City = s.Addresses.Where(w => w.IS_PRIMARY == true).FirstOrDefault().CITY,
                        State = s.Addresses.Where(w => w.IS_PRIMARY == true).FirstOrDefault().STATE,
                        ZipCode = s.Addresses.Where(w => w.IS_PRIMARY == true).FirstOrDefault().ZIP_CODE
                    },
                    Phones = s.Phones.Select(p => new Phone { PhoneNumber = p.NUMBER }).ToList()
                }).ToListAsync();
            }
        }

        /// <summary>
        /// Get Members
        /// </summary>
        /// <returns></returns>
        public async Task<List<Member>> getMembers()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                return await db.People.Where(w => w.IS_ACTIVE == true).Select(s => new Member
                {
                    Id = s.ID,
                    FirstName = s.FIRST_NAME,
                    MiddleName = s.MIDDLE_NAME,
                    LastName = s.LAST_NAME,
                    IsActive = s.IS_ACTIVE,
                    ProfilePicture = s.PROFILE_PICTURE,
                    Address = new Address
                    {
                        AddressLine1 = s.Addresses.Where(w => w.IS_PRIMARY == true).FirstOrDefault().ADDRESS_LINE_1,
                        AddressLine2 = s.Addresses.Where(w => w.IS_PRIMARY == true).FirstOrDefault().ADDRESS_LINE_2,
                        City = s.Addresses.Where(w => w.IS_PRIMARY == true).FirstOrDefault().CITY,
                        State = s.Addresses.Where(w => w.IS_PRIMARY == true).FirstOrDefault().STATE,
                        ZipCode = s.Addresses.Where(w => w.IS_PRIMARY == true).FirstOrDefault().ZIP_CODE
                    },
                    Phones = s.Phones.Select(p => new Phone { PhoneNumber = p.NUMBER }).ToList()
                }).ToListAsync();
            }
        }

        /// <summary>
        /// Get Member
        /// </summary>
        /// <returns></returns>
        public async Task<Member> getMember()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                return await db.People.Where(w => w.ID == this.Id).Select(s => new Member
                {
                    Id = s.ID,
                    FirstName = s.FIRST_NAME,
                    MiddleName = s.MIDDLE_NAME,
                    LastName = s.LAST_NAME,
                    ProfilePicture = s.PROFILE_PICTURE,
                    UserName = s.USERNAME,
                    password = s.PASSWORD
                }).FirstOrDefaultAsync();
            }
        }

        /// <summary>
        /// Get Address
        /// </summary>
        /// <returns></returns>
        public async Task<List<Address>> getAddresses()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                return await db.Addresses.Where(w => w.PERSON_ID == this.Id).Select(s => new Address
                {
                    AddressLine1 = s.ADDRESS_LINE_1,
                    AddressLine2 = s.ADDRESS_LINE_2,
                    City = s.CITY,
                    Id = s.ID,
                    IsPrimary = s.IS_PRIMARY,
                    State = s.STATE,
                    ZipCode = s.ZIP_CODE
                }).ToListAsync();
            }
        }

        /// <summary>
        /// Get Phones
        /// </summary>
        /// <returns></returns>
        public async Task<List<Phone>> getPhones()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                return await db.Phones.Where(w => w.PERSON_ID == this.Id).Select(s => new Phone 
                { 
                    Id = s.ID,
                    PhoneNumber = s.NUMBER,
                    PhoneType = s.PhoneType.NAME,
                    PhoneTypeId = s.PHONE_TYPE_ID
                }).ToListAsync();
            }
        }

        /// <summary>
        /// Get Phone
        /// </summary>
        /// <returns></returns>
        public async Task<Phone> getPhone()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                return await db.Phones.Where(w => w.ID == this.Phone.Id).Select(s => new Phone 
                {
                    Id = s.ID,
                    PhoneNumber = s.NUMBER,
                    PhoneType = s.PhoneType.NAME,
                    PhoneTypeId = s.PHONE_TYPE_ID
                }).FirstOrDefaultAsync();
            }
        }

        /// <summary>
        /// Get Address
        /// </summary>
        /// <returns></returns>
        public async Task<Address> getAddress()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                return await db.Addresses.Where(w => w.ID == this.Address.Id).Select(s => new Address
                {
                    AddressLine1 = s.ADDRESS_LINE_1,
                    AddressLine2 = s.ADDRESS_LINE_2,
                    City = s.CITY,
                    Id = s.ID,
                    IsPrimary = s.IS_PRIMARY,
                    State = s.STATE,
                    ZipCode = s.ZIP_CODE
                }).FirstOrDefaultAsync();
            }
        }

        /// <summary>
        /// Add Address
        /// </summary>
        /// <returns></returns>
        public async Task addAddress()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                data.Address address = new data.Address();

                address.PERSON_ID = this.Id;
                address.ADDRESS_LINE_1 = this.Address.AddressLine1;
                address.ADDRESS_LINE_2 = this.Address.AddressLine2;
                address.CITY = this.Address.City;
                address.STATE = this.Address.State;
                address.ZIP_CODE = this.Address.ZipCode;
                address.DATE_CREATED = DateTime.Now;

                db.Addresses.Add(address);

                await db.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Update Address
        /// </summary>
        /// <returns></returns>
        public async Task updateAddress()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                var address = await db.Addresses.Where(w => w.ID == this.Address.Id).FirstOrDefaultAsync();

                if (address != null)
                {
                    address.ADDRESS_LINE_1 = this.Address.AddressLine1;
                    address.ADDRESS_LINE_2 = this.Address.AddressLine2;
                    address.CITY = this.Address.City;
                    address.STATE = this.Address.State;
                    address.ZIP_CODE = this.Address.ZipCode;
                    address.DATE_UPDATED = DateTime.Now;

                    await db.SaveChangesAsync();
                }
            }
        }

        /// <summary>
        /// Update Security Info
        /// </summary>
        /// <returns></returns>
        public async Task updateSecurityInfo()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                var member = await db.People.Where(w => w.ID == this.Id).FirstOrDefaultAsync();

                if (member != null)
                {
                    member.USERNAME = this.UserName;
                    member.PASSWORD = this.password;
                    member.DATE_UPDATED = DateTime.Now;

                    await db.SaveChangesAsync();
                }
            }
        }

        /// <summary>
        /// Update Phone
        /// </summary>
        /// <returns></returns>
        public async Task updatePhone()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                var phone = await db.Phones.Where(w => w.ID == this.Phone.Id).FirstOrDefaultAsync();
                
                if (phone != null)
                {
                    phone.DATE_UPDATED = DateTime.Now;
                    phone.NUMBER = this.Phone.PhoneNumber;
                    phone.PHONE_TYPE_ID = this.Phone.PhoneTypeId;

                    await db.SaveChangesAsync();
                }
            }
        }

        /// <summary>
        /// Add Phone
        /// </summary>
        /// <returns></returns>
        public async Task addPhone()
        {
            using (MovieStore.data.MovieStoreEntities db = new MovieStore.data.MovieStoreEntities())
            {
                MovieStore.data.Phone phone = new MovieStore.data.Phone
                {
                    DATE_CREATED = DateTime.Now,
                    NUMBER = this.Phone.PhoneNumber,
                    PHONE_TYPE_ID = this.Phone.PhoneTypeId,
                    PERSON_ID = this.Id
                };

                db.Phones.Add(phone);

                await db.SaveChangesAsync();
            }
        }
    }
}
