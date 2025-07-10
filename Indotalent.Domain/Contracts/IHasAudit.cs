using System.ComponentModel.DataAnnotations.Schema;

namespace Indotalent.Domain.Contracts
{
    public interface IHasAudit
    {
        string? CreatedByUserId { get; set; }
        DateTime? CreatedAtUtc { get; set; }
        string? UpdatedByUserId { get; set; }
        DateTime? UpdatedAtUtc { get; set; }
        string? CreatedByUserName { get; set; }
        string? UpdatedByUserName { get; set; }


        //not mapped
        [NotMapped]
        string? CreatedAtString { get; set; }
        [NotMapped]
        string? UpdatedAtString { get; set; }
    }
}
