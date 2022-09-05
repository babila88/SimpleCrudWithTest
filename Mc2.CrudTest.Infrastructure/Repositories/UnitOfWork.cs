using Mc2.CrudTest.Application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork

    {
        private readonly DataContext _dataContext;
        private ICustomerRepository _customerRepository;

        public ICustomerRepository CustomerRepository =>
           _customerRepository ??= new CustomerRepository(_dataContext);

        public UnitOfWork(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public void Dispose()
        {
            _dataContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _dataContext.SaveChangesAsync();
        }
    }
}
