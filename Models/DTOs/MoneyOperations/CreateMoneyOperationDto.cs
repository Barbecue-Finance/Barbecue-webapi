using System.ComponentModel.DataAnnotations;
using Models.Db;
using Models.Db.Account;

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

        [Required(AllowEmptyStrings = false)]
        [String(1, 128)]
        public string OperationCategoryTitle { get; set; }

        [Id(typeof(User))]
        public long UserId { get; set; }
    }
}