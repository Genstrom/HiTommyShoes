using hiTommy.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace HelloTommy.Views.Shared.Components
{
    public class NavBarViewComponent : ViewComponent
    {
        public BrandServices _brandServices;


        public NavBarViewComponent(BrandServices brandServices)
        {
            _brandServices = brandServices;
        }

        public IViewComponentResult Invoke()
        {
            var brand = _brandServices.GetAllBrands();
            return View(brand);
        }
    }
}