using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.business
{
    public abstract class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public abstract Task addPerson();
        public abstract Task updatePerson();
        //public abstract Task<Person> getPerson();
        //public abstract Task<List<Person>> getPeople();
    }
}
