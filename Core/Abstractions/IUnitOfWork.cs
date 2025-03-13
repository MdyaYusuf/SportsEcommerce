using Microsoft.EntityFrameworkCore.Storage;

namespace Core.Abstractions;

public interface IUnitOfWork
{
  Task<int> SaveChangesAsync();
  Task<IDbContextTransaction> BeginTransactionAsync();
}
