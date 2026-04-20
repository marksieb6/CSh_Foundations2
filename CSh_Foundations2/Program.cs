using CarDomain;
using CarPropertiesDomain;
using Services;
using ConsoleTables;
using System.Text.Json;
using System.ComponentModel;

namespace CSh_Foundations2;

class Program {

    static List<Car> carList = new List<Car>();
    public static async Task Main(string[] args)
    {
        // C# Foundations Challenge 2
        // constructor class instances
        Car getCar = new Car();
        Car car2 = new Car("Toyota", "Camry", 2020, 24000, Guid.NewGuid());

        // properties class instances
        CarProperties car3 = new CarProperties()
        {
            Make = "Honda",
            Model = "Civic",
            Year = 2022,
            Price = 22000,
            CarId = Guid.NewGuid()
        };
        CarProperties car4 = new CarProperties()
        {
            Make = "Ford",
            Model = "Mustang",
            Year = 2021,
            Price = 35000,
            CarId = Guid.NewGuid()
        };

        // display car information in console
        // Console.WriteLine("Car 1:\n" + car1.Display());
        // Console.WriteLine("Car 2:\n" + car2.Display());
        // Console.WriteLine("Car 3:\n" + car3.Display());
        // Console.WriteLine("Car 4:\n" + car4.Display());

        // C# Foundations Challenge 3, 4 and 5
        // Console interactive table menu
        Dictionary<Guid, string> carInvoices = new Dictionary<Guid, string>();

        bool runCheck = true;
        while (runCheck == true)
        {
            var table = new ConsoleTable("Press 1 to Enter New Car", "Press 2 to List Cars", "Press 3 to Finds Car by price");
            table.AddRow("Press 4 to Edit Car", "Press 5 to Delete Car", "Press 6 to Add an Invoice");
            table.AddRow("Press 7 to Search Cars by Year", "", "Press 9 to Exit");
            table.Write();
            switch (Console.ReadLine())
            {
                case "1": // Enter New Car
                    Console.Clear();
                    Console.WriteLine("Enter Car Make:");
                    string make = Console.ReadLine()!;
                    Console.WriteLine("Enter Car Model:");
                    string model = Console.ReadLine()!;
                    Console.WriteLine("Enter Car Year:");
                    int year = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("Enter Car Price:");
                    double price = double.Parse(Console.ReadLine()!);
                    carList.Add(new Car(make, model, year, price, Guid.NewGuid()));
                    break;

                case "2": // List Cars
                    Console.Clear();
                    var displayTable = new ConsoleTable("Make", "Model", "Year", "Price", "ID");
                    foreach (var car in carList)
                    {
                        displayTable.AddRow(car.Make, car.Model, car.Year, car.Price, car.CarId);
                    }
                    displayTable.Write();
                    break;

                case "3": // Find Car by Price service
                    Console.Clear();
                    getCar = CarService.GetCarByPrice(carList);
                    break;

                case "4": // Edit Car info
                    Console.Clear();
                    getCar = CarService.GetCarByID(carList);

                    Console.WriteLine("Enter car Make:");
                    getCar.Make = Console.ReadLine();
                    Console.WriteLine("Enter car Model:");
                    getCar.Model = Console.ReadLine();
                    Console.WriteLine("Enter car Year:");
                    getCar.Year = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter car Price:");
                    getCar.Price = double.Parse(Console.ReadLine());
                    Console.WriteLine(getCar.Display());
                    break;
                    
                case "5": // Delete Car by ID
                    Console.Clear();
                    getCar = CarService.GetCarByID(carList);
                    carList.Remove(getCar);
                    break;
                
                case "6": // Add Invoice to existing Car
                    Console.Clear();
                    getCar = CarService.GetCarByID(carList);
                    getCar.Invoice = new List<double>();

                    Console.WriteLine("Enter Invoice Amount:");
                    double invoiceAdd = double.Parse(Console.ReadLine());
                    getCar.Invoice.Add(invoiceAdd);
                    Console.WriteLine($"Invoice for {getCar.Make} {getCar.Model}: ${getCar.Invoice:C}");
                    getCar.Display();
                    break;

                case "7": // Search for car by Year using LINQ
                    Console.WriteLine("Please enter car year: ");
                    var query = int.Parse(Console.ReadLine());
                    IEnumerable<Car> carsWhereYear =
                        from car in carList
                        where car.Year == query
                        select car;
                    
                    var queryTable = new ConsoleTable("ID", "Make", "Model", "Year", "Price");
                    foreach (Car car in carsWhereYear)
                    {
                        queryTable.AddRow(car.CarId, car.Make, car.Model, car.Year, $"{car.Price:C}");
                    }
                    queryTable.Write();
                    break;

                case "9": // exit statement + outputs list of cars to text file
                    string path = (Directory.GetCurrentDirectory() + "Cars.json");
                
                    if (!File.Exists(path)) {
                        using (StreamWriter sw = File.CreateText(path))
                        {
                            await sw.WriteLineAsync("This is the first time running...");
                            string jsonString = JsonSerializer.Serialize(carList);
                            await sw.WriteLineAsync(jsonString);
                        }
                    } 
                    else if (File.Exists(path)) {
                        using (StreamWriter sw = File.AppendText(path))
                        {
                            string jsonString = JsonSerializer.Serialize(carList);
                            await sw.WriteLineAsync(jsonString);
                        }
                    }
                    // Console.Clear();
                    Console.WriteLine("Exiting program. Goodbye!");
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("Invalid option. Please try again.");
                    continue;
            }
            
            Console.WriteLine("Do you want to continue? (y/n)");
            string exit = Console.ReadLine().ToLower();
            if (exit == "y")
            {
                runCheck = true;
            }
            else if (exit == "n")
            {
                runCheck = false;
            }
            else
            {
                Console.WriteLine("Invalid option. Please try again.");
            }
        }
    }
}