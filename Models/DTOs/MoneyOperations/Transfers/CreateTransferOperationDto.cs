using Models.Db;

namespace Models.DTOs.MoneyOperations.Transfers
{
    public class CreateTransferOperationDto
    {
        public float Amount { get; set; }

        public string Comment { get; set; }

        [Id(typeof(Purse))]
        public long FromPurseId { get; set; }
        
        [Id(typeof(Purse))]
        public long ToPurseId { get; set; }
    }
}