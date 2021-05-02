using System.Collections.Generic;
using System.Linq;
using hiTommy.Data.ViewModels;
using hiTommy.Models;

namespace hiTommy.Data.Services
{
    public class BrandServices
    {
        private readonly HiTommyApplicationDbContext _context;

        public BrandServices(HiTommyApplicationDbContext context)
        {
            _context = context;
        }

        public List<Brand> GetAllBrands()
        {
            return _context.Brands.ToList();
        }

        public void AddBrand(BrandVm brand)
        {
            if (string.IsNullOrWhiteSpace(brand.Name)) return;
            var _brand = new Brand
            {
                Name = brand.Name
            };
            _context.Brands.Add(_brand);
            _context.SaveChanges();
        }

        public BrandWithShoesListViewModel GetShoesByBrandId(int brandId)
        {
            var _brand = _context.Brands.Where(n => n.Id == brandId).Select(n => new BrandWithShoesListViewModel
            {
                Shoes = n.Shoes
            }).FirstOrDefault();

            return _brand;
        }

        public BrandWithShoesListViewModel GetShoesByBrandName(string brandName)
        {
            var _brand = _context.Brands.Where(n => n.Name == brandName).Select(n => new BrandWithShoesListViewModel
            {
                Shoes = n.Shoes
            }).FirstOrDefault();

            return _brand;
        }

        public void DeleteBrandById(int brandId)
        {
            var _brand = _context.Brands.FirstOrDefault(n => n.Id == brandId);
            if (_brand is not null)
            {
                _context.Brands.Remove(_brand);
                _context.SaveChanges();
            }
        }
    }
}