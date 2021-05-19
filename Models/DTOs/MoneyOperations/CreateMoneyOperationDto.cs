using System.ComponentModel.DataAnnotations;
using Models.Db;

namespace Models.DTOs.MoneyOperations
{
    public class CreateMoneyOperationDto
    {
        [Required]
        [Range(0, double.MaxValue)]
        public float Amount { get; set; }

        [Required(AllowEmptyStrings = true)]
        [String(0, 128)]
        public string Comment { get; set; }

        [Id(typeof(Purse))]
        public long PurseId { get; set; }
    }
}