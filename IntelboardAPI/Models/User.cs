namespace IntelboardAPI.Models
{
    public class User
    {
        // Create DTO for lists and other properties that should not be exposed directly
        public int Id { get; set; }
        public string Name { get; set; }
        public  string Role { get; set; }
    }
}
