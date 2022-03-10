using System.ComponentModel.DataAnnotations;

namespace ATM.API.Model
{
    public class WithdrawMoneyOperationDto
    {
        
        [Required]
        [Range(1, 200, ErrorMessage = $"You couldn't withdraw more than 200 at once.")]
        public int amount { get; set; }
    }
}
