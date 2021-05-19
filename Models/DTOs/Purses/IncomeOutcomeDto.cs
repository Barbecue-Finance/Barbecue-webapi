using System.Collections.Generic;
using Models.DTOs.MoneyOperations;

namespace Models.DTOs.Purses
{
    public class IncomeOutcomeDto
    {
        public virtual ICollection<MoneyOperationDto> OutComing { get; set; }
        public virtual ICollection<MoneyOperationDto> Incoming { get; set; }
    }
}