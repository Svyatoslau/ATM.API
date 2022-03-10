using System.ComponentModel.DataAnnotations;

namespace ATM.API.Models
{
    public class AtmForWithdrawMoneyOperationDto
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "No cash available at this moment.")]
        public int TotalMoney { get; set; } = 0;
    }
}
