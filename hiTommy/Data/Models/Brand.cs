using System.Collections.Generic;

#nullable disable

namespace hiTommy.Models
{
    public class Brand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Shoe> Shoes { get; set; } = new();
    }
}