//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MovieStore.data
{
    using System;
    using System.Collections.Generic;
    
    public partial class MovieRental
    {
        public int Rental_Id { get; set; }
        public int Movie_Id { get; set; }
        public Nullable<System.DateTime> Return_Date { get; set; }
    
        public virtual Movie Movie { get; set; }
        public virtual Rental Rental { get; set; }
    }
}