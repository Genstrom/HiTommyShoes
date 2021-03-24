using hiTommy.Models;

namespace hiTommy.Data.Models
{
    public class Quantity
    {
        public int Id { get; set; }
        public double Size { get; set; }
        public int Quantities { get; set; }

        //Navigation Properties
        public int ShoeId { get; set; }
        public Shoe Shoe { get; set; }
    }
}