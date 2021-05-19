namespace Models.DTOs.Users
{
    public class UpdateUserDto
    {
        public long Id { get; set; }
        
        public string Username { get; set; }

        public string Login { get; set; }
        
        public string Password { get; set; }
    }
}