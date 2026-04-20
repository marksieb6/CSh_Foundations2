using System;
using CarDomain;

namespace Services;

public class CarService
{
    public static Car GetCarByID(List<Car> cars)
    {
        Console.Clear();
        Console.WriteLine("Enter Car ID:");
        Guid idFind = Guid.Parse(Console.ReadLine()!);
        var findCar = cars.Find(Car => (Car.CarId == idFind));
        Console.WriteLine(findCar.Display());
        return findCar;
    }

    public static Car GetCarByPrice(List<Car> cars)
    {
        Console.Clear();
        Console.WriteLine("Enter maximum Car price:");
        double priceFind = double.Parse(Console.ReadLine()!);
        var findCar = cars.Find(Car => (Car.Price <= priceFind));
        Console.WriteLine(findCar.Display());
        return findCar;
    }
}
