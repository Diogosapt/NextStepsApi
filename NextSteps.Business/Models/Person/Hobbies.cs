using System;

namespace NextSteps.Business.Models
{
    public record Hobbies
    {
        public Guid Id { get; init; }
        public string Hobby { get; init; }
    }
}