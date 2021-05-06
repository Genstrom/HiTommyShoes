using hiTommy.Data.ViewModels;
using hiTommy.Models;
using System.Collections.Generic;

namespace hiTommy.Data.Services
{
    public interface IBrandServices
    {
        void AddBrand(BrandVm brand);
        void DeleteBrandById(int brandId);
        List<Brand> GetAllBrands();
        BrandWithShoesListViewModel GetShoesByBrandId(int brandId);
        BrandWithShoesListViewModel GetShoesByBrandName(string brandName);
    }
}