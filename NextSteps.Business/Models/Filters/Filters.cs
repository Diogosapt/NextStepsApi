using NextSteps.Business.Core.Common;
using System;
using System.Collections.Generic;

namespace NextSteps.Business.Models
{
    public record Filters : Entity
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public string Surname { get; init; }

        public string Job { get; init; }

        public string Email { get; init; }

        public IEnumerable<Hobbies> Hobbies { get; init; }
    }
}