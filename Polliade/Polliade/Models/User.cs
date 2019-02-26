namespace Polliade.Models
{
    public class User : BaseModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
    }
}