using System.ComponentModel.DataAnnotations;

namespace Indotalent.DTOs
{
    public class CustomerCategoryDto
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        [Key] public Guid? RowGuid { get; set; }
        public DateTime? CreatedAtUtc { get; set; }
    }
}
