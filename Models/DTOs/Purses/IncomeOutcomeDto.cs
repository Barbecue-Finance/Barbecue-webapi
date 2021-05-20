using System.Collections.Generic;
using Models.DTOs.MoneyOperations;

namespace Models.DTOs.Purses
{
    public class IncomeOutcomeDto
    {
        public virtual ICollection<OutComeMoneyOperationDto> OutComing { get; set; }
        public virtual ICollection<IncomeMoneyOperationDto> Incoming { get; set; }
    }
}