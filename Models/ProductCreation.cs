namespace TodoApi.Models;

public class ProductCreation
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    public override string ToString()
    {
        return $"ProductCreation(Name={Name}, Price={Price})";
    }
}
