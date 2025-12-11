namespace WebApiRestaurant.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public required string Role { get; set; }
        public string? Mail { get; set; }
    }
}
