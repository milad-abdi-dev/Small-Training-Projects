// See https://aka.ms/new-console-template for more information

using VehicleManagementSystem;

List<Vehicle> vehicles = new List<Vehicle>();
vehicles.Add(new Car());
vehicles.Add(new Bike());
vehicles.Add(new Bike());
vehicles.Add(new Car());

foreach (Vehicle vehicle in vehicles)
{
    vehicle.CalculateFuelConsumption();
}