namespace TodoApi.Models;

public class ProductCreation
{
    public required string Name { get; set; }
    public decimal Price { get; set; }

    public override string ToString()
    {
        return $"ProductCreation(Name={Name}, Price={Price})";
    }
}

public class ProductUpdate
{
    public string? Name { get; set; }
    public decimal? Price { get; set; }

    public override string ToString()
    {
        return $"ProductCreation(Name={Name}, Price={Price})";
    }
}
