using Core.Entities.HR;
using Core.Entities.Lookups;
using Core.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUnitOfWork
    {
        IBaseRepository<Employee> EmployeeRepository { get; }
        IBaseRepository<Department> DepartmentRepository { get; }
        IBaseRepository<Log> LogRepository { get; }


        Task BeginTransactionAsync();
        Task CommitChangesAsync();
        Task RollbackTransactionAsync();
        Task DisposeTransactionAsync();
        
        Task<int> SaveChangesAsync();
    }
}
