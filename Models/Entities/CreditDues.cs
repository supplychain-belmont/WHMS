using Indotalent.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using Indotalent.Models.Contracts;

namespace Indotalent.Models.Entities
{
    public class CreditDues : _Base
    {
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
