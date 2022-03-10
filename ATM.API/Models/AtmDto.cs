using System.ComponentModel.DataAnnotations;

namespace ATM.API.Models
{
    public class AtmDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public int TotalMoney { get; set; } = 0;
        
    }
}
