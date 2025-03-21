using Indotalent.Domain.Contracts;

namespace Indotalent.Domain.Entities
{
    public class LogError : _Base
    {
        public LogError() { }

        public string? ExceptionMessage { get; set; }
        public string? StackTrace { get; set; }
        public string? AdditionalInfo { get; set; }

    }
}
