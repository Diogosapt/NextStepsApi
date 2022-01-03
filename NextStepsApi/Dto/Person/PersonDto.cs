using System;
using System.Collections.Generic;

namespace NextSteps.Api.Dto
{
    public record PersonDto
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public string Surname { get; init; }

        public string Job { get; init; }

        public DateTime Birthday { get; init; }

        public string Email { get; init; }

        public IEnumerable<HobbiesDto> Hobbies { get; init; }
    }
}