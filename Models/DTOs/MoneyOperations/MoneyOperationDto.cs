using System;
using System.ComponentModel.DataAnnotations;
using Models.Db;

namespace Models.DTOs.MoneyOperations
{
    public class MoneyOperationDto
    {
        [Required]
        public long Id { get; set; }
        
        [Required]
        [Range(0, double.MaxValue)]
        public float Amount { get; set; }

        [Required(AllowEmptyStrings = true)]
        [String(0, 128)]
        public string Comment { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Id(typeof(Purse))]
        public long PurseId { get; set; }
    }
}