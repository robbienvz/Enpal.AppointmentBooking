using System.ComponentModel.DataAnnotations.Schema;

[Table("sales_managers")]
public class SalesManager
{
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("languages")]
    public List<string> Languages { get; set; }
    [Column("products")]
    public List<string> Products { get; set; }
    [Column("customer_ratings")]
    public List<string> CustomerRatings { get; set; }
}