namespace ApiTest.Contracts.Database;
public interface IAuditableEntity
{
    DateTime Created { get; set; }
    DateTime LastUpdated { get; set; }
}
