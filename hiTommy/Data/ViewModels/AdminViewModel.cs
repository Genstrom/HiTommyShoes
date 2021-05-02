using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using hiTommy.Models;

namespace hiTommy.Data.ViewModels
{
    public class AdminViewModel
    {
        public List<Brand> Brands { get; set; }
        public List<Shoe> Shoes { get; set; }
        [Required]
        public ShoeViewModel Shoe { get; set; }
        [Required]
        public BrandVm Brand { get; set; }
        
    }
}