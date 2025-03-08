using ZhooCars.Common;

namespace ZhooCars.Model.DTOs
{
    public class InsuranceUpdateDto
    {
        #region Properties

        public DateTime InsuranceExpiryDate { get; set; }

        public string InsuranceNumber { get; set; } = string.Empty;

        #endregion
    }

    public class RegisterVehicleDto
    {
        #region Properties

        public List<DocumentDto> Documents { get; set; }

        public required VehicleDto VehicleDetails { get; set; }

        #endregion
    }

    public class VehicleDto
    {
        #region Properties

        public ApprovalStatus ApprovalStatus { get; set; }

        public required string Color { get; set; }

        public required string FuelType { get; set; }

        public required DateTime InsuranceExpiryDate { get; set; }

        public required string InsuranceNumber { get; set; }

        public required string RCNumber { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public required int SeatingCapacity { get; set; }

        public VehicleStatus Status { get; set; }

        public int VehicleId { get; set; }

        public required string VehicleMake { get; set; }

        public required string VehicleModel { get; set; }

        public required string VehicleRegistrationNumber { get; set; }

        public int VehicleTypeId { get; set; }

        public required int VehicleYear { get; set; }

        #endregion
    }
}
