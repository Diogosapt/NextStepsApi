using System;
using System.Collections.Generic;

namespace NextSteps.Api.Dto
{
    public class PersonCreateDto
    {
        public string Name { get; init; }

        public string Surname { get; init; }

        public string Job { get; init; }

        public DateTime Birthday { get; init; }

        public string Email { get; init; }

        public IEnumerable<HobbiesCreateDto> Hobbies { get; init; }
    }
}