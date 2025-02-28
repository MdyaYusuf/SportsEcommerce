namespace Core.Abstractions;

public interface IUnitOfWork
{
  Task<int> SaveChangesAsync();
}
