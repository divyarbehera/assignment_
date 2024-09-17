using ApiTest.Contracts.Repository;
using System.ComponentModel.DataAnnotations;

namespace ApiTest.Contracts.Database;


public record Product : IAuditableEntity
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastUpdated { get; set; }
}
