using hiTommy.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace hiTommy.Interfaces
{
    public interface IUnitOfWork
    {
        BrandServices BrandService { get; }
        CustomerService CustomerService { get; }
        MailServices MailService { get; }
        OrderService OrderService { get; }
        QuantityService QuantityService { get; }
        ShoeServices ShoeService { get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}
