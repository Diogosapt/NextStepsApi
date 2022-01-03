using System;

namespace NextSteps.Api.Dto
{
    public record HobbiesDto
    {
        public Guid Id { get; init; }

        public string Hobby { get; init; }
    }
}