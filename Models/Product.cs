using SqlSugar;

namespace TodoApi.Models;

public class Product
{
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    public override string ToString()
    {
        return $"Product(Id={Id}, Name={Name}, Price={Price})";
    }
}
