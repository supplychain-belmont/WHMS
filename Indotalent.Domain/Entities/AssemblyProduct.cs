using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Indotalent.Domain.Contracts;

namespace Indotalent.Domain.Entities;

public class AssemblyProduct : _Base
{
    public int AssemblyId { get; set; }
    public Product? Assembly { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public decimal Quantity { get; set; }
}
