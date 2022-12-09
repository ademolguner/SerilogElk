namespace SerilogElasticKibana.Application.Models;

public class Car
{
    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public string Marka { get; set; }
    public  string Model { get; set; }
    public int ModelYil { get; set; }
}