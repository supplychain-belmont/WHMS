using System;

using Indotalent.Models.Enums;

namespace Indotalent.DTOs
{
    public class CreditDuesDto
    {
        public int Id { get; set; }
        public int CreditId { get; set; }
        public string? FileImage { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
        public int CustomerId { get; set; }
        public int DueValue { get; set; }
        public int DueNumber { get; set; }
        public DateTime DueLapse { get; set; }
        public int InitialPaymentPercentage { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
    }
}
