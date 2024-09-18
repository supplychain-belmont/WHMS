#nullable disable

using Indotalent.Models.Contracts;

namespace Indotalent.Infrastructures.Images
{
    public class FileImage : _Base
    {
        public Guid Id { get; set; }
        public string OriginalFileName { get; set; }
        public byte[] ImageData { get; set; }
        public string? CreatedByUserId { get; set; }
        public DateTime? CreatedAtUtc { get; set; }
        public string? UpdatedByUserId { get; set; }
        public DateTime? UpdatedAtUtc { get; set; }

        public bool IsNotDeleted { get; set; } = true;
    }
}
