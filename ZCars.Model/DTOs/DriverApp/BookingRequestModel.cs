﻿using ZhooCars.Common;

namespace ZCars.Model.DTOs.DriverApp
{
    public class BookingRequestModel
    {
        public RideTypeEnum BookingType { get; set; }
        public string Fare { get; set; }
        public string DistanceAndPayment { get; set; }
        public string PickupLocation { get; set; }
        public double PickupLatitude { get; set; }
        public double PickupLongitude { get; set; }
        public string PickupAddress { get; set; }
        public string PickupTime { get; set; }
        public string DropoffLocation { get; set; }
        public double DropLatitude { get; set; }
        public double DropLongitude { get; set; }
        public int RemainingBids { get; set; }
        public int BoookingRequestId { get; set; }

        public string? UserName { get; set; }
    }
}
