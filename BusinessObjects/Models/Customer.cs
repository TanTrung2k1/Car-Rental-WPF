using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Models
{
    public partial class Customer
    {
        public Customer()
        {
            CarRentals = new HashSet<CarRental>();
            Reviews = new HashSet<Review>();
        }

        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Mobile { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPassword { get; set; }
        public string IdentityCard { get; set; }
        public string LicenceNumber { get; set; }
        public DateTime? LicenceDate { get; set; }

        public virtual ICollection<CarRental> CarRentals { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
