using hiTommy.Data.Services;
using hiTommy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hiTommy.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly HiTommyApplicationDbContext _context;

        public UnitOfWork(HiTommyApplicationDbContext context)
        {
            _context = context;
        }
        public BrandServices BrandService => new BrandServices(_context);

        public CustomerService CustomerService => throw new NotImplementedException();

        public MailServices MailService => throw new NotImplementedException();

        public OrderService OrderService => throw new NotImplementedException();

        public QuantityService QuantityService => throw new NotImplementedException();

        public ShoeServices ShoeService => throw new NotImplementedException();

        public bool HasChanges()
        {
           return _context.ChangeTracker.HasChanges();
        }

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
