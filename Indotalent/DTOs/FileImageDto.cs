using System.ComponentModel.DataAnnotations;

namespace Indotalent.DTOs
{
    public class FileImageDto
    {
        [Key]
        public Guid? Id { get; set; }

        public string? OriginalFileName { get; set; }

        public byte[]? ImageData { get; set; }

        public DateTime? CreatedAtUtc { get; set; }

        public string? CreatedByUserId { get; set; }

        public DateTime? UpdatedAtUtc { get; set; }

        public string? UpdatedByUserId { get; set; }
    }
}
