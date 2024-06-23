using System;

using Indotalent.Models.Contracts;
namespace Indotalent.Models.Entities

{
    public class ProductFileImages : _Base
    {
        public int ProductId { get; set; }
        public string FileImagesId { get; set; }
        public Guid Id { get; set; }
    }
}
