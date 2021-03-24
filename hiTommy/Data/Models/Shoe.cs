using System.Collections.Generic;
using hiTommy.Data.Models;

#nullable disable

namespace hiTommy.Models
{
    public class Shoe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsOnSale { get; set; }
        public decimal? SalePrice { get; set; }
        public List<Quantity> Sizes { get; set; }
        public string Description { get; set; }

        public string PictureUrl { get; set; }

        //Navigation Properties
        public string Brand { get; set; }
        public int BrandId { get; set; }
    }
}