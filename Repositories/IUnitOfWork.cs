namespace basic_delivery_api.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}