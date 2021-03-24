using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using hiTommy.Models;

namespace hiTommy.Data.ViewModels
{
    public class ShoeViewModel
    {
        public string Name { get; set; }

        [Column(TypeName = "Price")] public decimal Price { get; set; }

        public int BrandId { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
    }

    public class ShoeSaleViewModel
    {
        public bool IsOnSale = true;

        [Column(TypeName = "SalePrice")] public decimal? SalePrice { get; set; }
    }

    public class ShoeListViewModel
    {
        public List<Shoe> Shoes { get; set; }
    }
}