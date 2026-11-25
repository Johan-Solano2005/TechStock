using System.ComponentModel.DataAnnotations;

namespace TechStock.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; }
        public string? Phone { get; set; }
    }
}
