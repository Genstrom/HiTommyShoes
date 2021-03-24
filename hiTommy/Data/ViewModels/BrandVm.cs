using System.Collections.Generic;
using hiTommy.Models;

namespace hiTommy.Data.ViewModels
{
    public class BrandVm
    {
        public string Name { get; set; }
        public int Id { get; }
    }

    public class BrandWithShoesListViewModel
    {
        public int Id { get; set; }
        public Brand Brand { get; set; }
        public List<Shoe> Shoes { get; set; }
    }
}