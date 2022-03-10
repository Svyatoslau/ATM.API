using System.ComponentModel.DataAnnotations;

namespace ATM.API.Model
{
    public class WithdrawMoneyOperation
    {

        [Required]
        [Range(1, 200, ErrorMessage = $"You couldn't withdraw more than at once.")]
        public int amount { get; set; }
    }
}
