using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZCars.Model.DTOs.DriverApp
{
    public class BookingRequestModel
    {
        public string BookingType { get; set; }
        public string Fare { get; set; }
        public string DistanceAndPayment { get; set; }
        public string PickupLocation { get; set; }
        public string PickupAddress { get; set; }
        public string PickupTime { get; set; }
        public string DropoffLocation { get; set; }
        public int RemainingBids { get; set; }
    }
}
