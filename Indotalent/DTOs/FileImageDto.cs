using System.ComponentModel.DataAnnotations;

namespace Indotalent.DTOs
{
    public class FileImageDto
    {
        [Key] public Guid? Id { get; set; }

        public string? OriginalFileName { get; set; }

        public byte[]? ImageData { get; set; }

        public DateTime? CreatedAtUtc { get; set; }

        public int? ProductId { get; set; }
    }
}
