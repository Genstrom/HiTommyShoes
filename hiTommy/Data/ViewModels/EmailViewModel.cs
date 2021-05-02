using System.ComponentModel.DataAnnotations;

namespace hiTommy.Data.ViewModels
{
    public class EmailViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public string Subject { get; set; }
    }
}