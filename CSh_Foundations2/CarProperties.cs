namespace CarPropertiesDomain;

public class CarProperties
{
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public double Price { get; set; }
    public Guid CarId { get; set; }
    public List<double> Invoice { get; set; } = new List<double>();

    public string Display()
    {
        return $"ID: {CarId}, \nPrice: {Price:C}";
    }
}
