using Core.Entities.HR;
using Core.Entities.Lookups;
using Core.Interfaces;
using Core.Interfaces.IRepositories;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private Elite_DbContext _context;
        private IDbContextTransaction _transaction;

        public IBaseRepository<Employee> EmployeeRepository { get; private set; }
        public IBaseRepository<Department> DepartmentRepository { get; private set; }
        public IBaseRepository<Log> LogRepository { get; private set; }

        public UnitOfWork(Elite_DbContext context)
        {
            _context = context;
            EmployeeRepository = new BaseRepository<Employee>(_context);
            DepartmentRepository = new BaseRepository<Department>(_context);
            LogRepository = new BaseRepository<Log>(_context);
        }


        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }
        public async Task CommitChangesAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                if (_transaction != null)
                {
                    await DisposeTransactionAsync();
                }
            }
        }
        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }
        }
        public async Task DisposeTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }


    }
}
