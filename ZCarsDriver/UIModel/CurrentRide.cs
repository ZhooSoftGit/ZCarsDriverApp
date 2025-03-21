using ZCars.Model.DTOs.DriverApp;
using ZhooCars.Common;
using ZhooCars.Model.DTOs;

namespace ZCarsDriver.UIModel
{
    public class CurrentRide
    {
        public BookingRequestModel BookingRequest { get; set; }

        public RideTripDto RideDetails { get; set; }

        public RideStatus RideStatus { get; set; }
    }
}
