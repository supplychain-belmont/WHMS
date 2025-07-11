﻿using Indotalent.Domain.Contracts;

namespace Indotalent.Domain.Entities
{
    public class LogSession : _Base
    {
        public LogSession() { }

        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? IPAddress { get; set; }
        public string? Action { get; set; }

    }
}
