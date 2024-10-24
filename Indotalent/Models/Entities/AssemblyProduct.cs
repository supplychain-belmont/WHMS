using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Indotalent.Models.Contracts;

namespace Indotalent.Models.Entities;

public class AssemblyProduct : _Base
{
    public int AssemblyId { get; set; }
    public Product? Assembly { get; set; }
    public int ProductId { get; set; }
    public Product? Product { get; set; }
}
