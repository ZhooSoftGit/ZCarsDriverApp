using ZCars.Model.DTOs.DriverApp;
using ZCarsDriver.Core.DBModel;
using ZhooCars.Model.DTOs;

namespace ZCarsDriver.UIModel
{
    public class CurrentRide
    {
        public BookingRequestModel BookingRequest { get; set; }

        public RideTripDto RideDetails { get; set; }
    }
}
