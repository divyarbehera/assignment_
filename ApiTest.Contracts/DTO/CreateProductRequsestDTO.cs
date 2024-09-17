using ApiTest.Contracts.Database;
using System.ComponentModel.DataAnnotations;

namespace ApiTest.Contracts.DTO;
public record CreateProductRequsestDTO
{
    [Required] 
    public string Name { get; set; }
    public string? Description { get; set; }
}