﻿#nullable disable

using Indotalent.Domain.Contracts;

namespace Indotalent.Persistence.Images
{
    public class FileImage : _Base
    {
        public Guid Id { get; set; }
        public string OriginalFileName { get; set; }
        public int? ProductId { get; set; }
        public byte[] ImageData { get; set; }
    }
}
