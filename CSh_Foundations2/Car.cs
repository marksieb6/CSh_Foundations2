namespace CarDomain;
public class Car
{
    public string Make;
    public string Model;
    public int Year;
    public double Price;
    public Guid CarId;
    public List<double> Invoice;
    
    public Car()
    {
        Make = "Mitsubishi";
        Model = "Lancer";
        Year = 2026;
        Price = 40000;
        CarId = Guid.NewGuid();
        List<double> Invoice;
    }
    public Car(string make, string model, int year, double price, Guid id)
    {
        Make = make;
        Model = model;
        Year = year;
        Price = price;
        CarId = id;
        List<double> Invoice;
    }
    public string Display()
    {
        return $"ID: {CarId} \n{Make} {Model} \nYear: {Year} \nPrice: {Price:C}";
    }
}