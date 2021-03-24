using System.Collections.Generic;
using System.Linq;
using hiTommy.Data.Models;
using hiTommy.Data.ViewModels;

namespace hiTommy.Data.Services
{
    public class QuantityService
    {
        private readonly HiTommyApplicationDbContext _context;

        public QuantityService(HiTommyApplicationDbContext context)
        {
            _context = context;
        }

        public List<string> GetAllShoesBySize(double size)
        {
            return _context.Quantities.Where(n => n.Size == size).Select(n => n.Shoe.Name).ToList();
        }

        public List<Quantity> GetAllSizesById(int id)
        {
            return _context.Quantities.Where(n => n.ShoeId == id).OrderBy(n => n.Size).ToList();
        }

        public Quantity AddQuantityToShoeBySizeAndShoeId(double size, int id, QuantityVm quantity)
        {
            var _quantity = _context.Quantities.FirstOrDefault(n => n.Id == id && n.Size == size);

            if (_quantity is not null) _quantity.Quantities = quantity.Quantities;
            _context.SaveChanges();

            return _quantity;
        }
    }
}