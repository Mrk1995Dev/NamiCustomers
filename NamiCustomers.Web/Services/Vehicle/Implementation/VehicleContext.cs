using NamiCustomers.Web.Services.Vehicle.Dto;

namespace NamiCustomers.Web.Services.Vehicle.Implementation
{
    public class VehicleContext
    {
        public VehicleModelDto? SelectedVin { get; private set; }

        public event Action? VinChanged;

        public void ChangeVin(VehicleModelDto vin)
        {
            SelectedVin = vin;
            VinChanged?.Invoke();
        }
    }
}
