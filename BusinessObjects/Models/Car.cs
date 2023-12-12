using System;
using System.Collections.Generic;

#nullable disable

namespace BusinessObjects.Models
{
    public partial class Car
    {
        public Car()
        {
            CarRentals = new HashSet<CarRental>();
            Reviews = new HashSet<Review>();
        }

        public string CarId { get; set; }
        public string CarName { get; set; }
        public int? CarModelYear { get; set; }
        public string Color { get; set; }
        public int? Capacity { get; set; }
        public string Description { get; set; }
        public DateTime? ImportDate { get; set; }
        public decimal? RentPrice { get; set; }
        public int? Status { get; set; }
        public string ProducerId { get; set; }

        public virtual CarProducer Producer { get; set; }
        public virtual ICollection<CarRental> CarRentals { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
    }
}
