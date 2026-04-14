namespace VehicleManagementSystem;

public abstract class Vehicle
{
    public abstract double CalculateFuelConsumption();
}

public class Car : Vehicle
{
    public override double CalculateFuelConsumption()
    {
        Console.WriteLine("Car fuel consumption.");
        return 20;
    }
}

public class Bike : Vehicle
{
    public override double CalculateFuelConsumption()
    {
        Console.WriteLine("Bike fuel consumption.");
        return 0;
    }
}