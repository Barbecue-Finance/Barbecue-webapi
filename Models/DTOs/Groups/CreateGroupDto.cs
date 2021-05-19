namespace Models.DTOs.Groups
{
    public class CreateGroupDto
    {
        public string Title { get; set; }
        
        public long CreatorId { get; set; }
    }
}