using System;
using System.Collections.Generic;

// Вывод сообщений
public interface IOutput
{
    void Write(string message);
}

// Вывод в консоль
public class ConsoleOutput : IOutput
{
    public void Write(string message)
    {
        Console.WriteLine(message);
    }
}

// Реализация заглушки для отключения вывода в консоль
public class NullOutput : IOutput
{
    public void Write(string message)
    {
    
    }
}

class Vehicle
{
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }

    protected IOutput _output;

    public Vehicle(string brand, string model, int year, IOutput output)
    {
        Brand = brand;
        Model = model;
        Year = year;
        _output = output;
    }

    public virtual void StartEngine()
    {
        _output.Write($"{Brand} {Model} двигатель запущен");
    }

    public virtual void StopEngine()
    {
        _output.Write($"{Brand} {Model} двигатель остановлен");
    }
}

class Car : Vehicle
{
    public int Doors { get; set; }
    public string Transmission { get; set; }

    public Car(string brand, string model, int year, int doors, string transmission, IOutput output)
        : base(brand, model, year, output)
    {
        Doors = doors;
        Transmission = transmission;
    }

    public override void StartEngine()
    {
        _output.Write($"{Brand} {Model} двигатель запущен");
    }
}

class Motorcycle : Vehicle
{
    public string Body { get; set; }
    public bool Box { get; set; }

    public Motorcycle(string brand, string model, int year, string body, bool box, IOutput output)
        : base(brand, model, year, output)
    {
        Body = body;
        Box = box;
    }

    public override void StartEngine()
    {
        _output.Write($"{Brand} {Model} двигатель запущен");
    }
}

class Garage
{
    public List<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    private IOutput _output;

    public Garage(IOutput output)
    {
        _output = output;
    }

    public void AddVehicle(Vehicle vehicle)
    {
        Vehicles.Add(vehicle);
        _output.Write($"{vehicle.Brand} {vehicle.Model} добавлен в гараж");
    }

    public void RemoveVehicle(Vehicle vehicle)
    {
        Vehicles.Remove(vehicle);
        _output.Write($"{vehicle.Brand} {vehicle.Model} удален из гаража");
    }

    public void ListVehicles()
    {
        foreach (var vehicle in Vehicles)
        {
            _output.Write($"{vehicle.Brand} {vehicle.Model}, Год: {vehicle.Year}");
        }
    }
}

class Fleet
{
    public List<Garage> Garages { get; set; } = new List<Garage>();
    private IOutput _output;

    public Fleet(IOutput output)
    {
        _output = output;
    }

    public void AddGarage(Garage garage)
    {
        Garages.Add(garage);
        _output.Write("Гараж добавлен");
    }

    public void RemoveGarage(Garage garage)
    {
        Garages.Remove(garage);
        _output.Write("Гараж удален");
    }

    public void ListGarages()
    {
        foreach (var garage in Garages)
        {
            _output.Write("Гараж: ");
            garage.ListVehicles();
        }
    }
}

class Application
{
    static bool isConsoleOutputEnabled = true;

    static void Main(string[] args)
    {
        IOutput output;

        if (isConsoleOutputEnabled)
        {
            output = new ConsoleOutput(); // Включено - значит консоль
        }
        else
        {
            output = new NullOutput(); // Выключено - вывод в потенциальное приложение
        }

        Car car1 = new Car("Toyota", "1", 2024, 4, "А", output);
        Car car2 = new Car("Ford", "2", 2023, 2, "М", output);
        Motorcycle bike1 = new Motorcycle("Yamaha", "3", 2022, "Спортивный", false, output);

        Garage garage1 = new Garage(output);
        Garage garage2 = new Garage(output);

        Fleet fleet = new Fleet(output);

        garage1.AddVehicle(car1);
        garage1.AddVehicle(bike1);

        garage2.AddVehicle(car2);

        fleet.AddGarage(garage1);
        fleet.AddGarage(garage2);

        fleet.ListGarages();

        garage1.RemoveVehicle(bike1);
        fleet.RemoveGarage(garage2);

        fleet.ListGarages();
    }
}