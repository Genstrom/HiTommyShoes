using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using hiTommy.Models;
using IdentityServer4.Models;

namespace hiTommy.Data.ViewModels
{
    public class ShoeViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "Price")] public decimal Price { get; set; }
        [Required] 
        public int BrandId { get; set; }
        [Required] 
        public string Description { get; set; }
        [Required]
        [DataType(DataType.ImageUrl)]
        public string PictureUrl { get; set; }
        [Required] 
        public int Size { get; set; }
        [Required] 
        public int Quantity { get; set; }
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