using System.ComponentModel.DataAnnotations;

namespace ATM.API.Model
{
    public class AtmDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "No cash available at this moment.")]
        public int TotalMoney { get; set; } = 0;
        
    }
}
