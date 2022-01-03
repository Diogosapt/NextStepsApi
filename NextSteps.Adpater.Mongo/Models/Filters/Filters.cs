using System;
using System.Collections.Generic;

namespace NextSteps.Adpater.Mongo.Models
{
    public class Filters : Entity
    {
        public Guid Id { get; init; }

        public string Name { get; init; }

        public string Surname { get; init; }

        public string Job { get; init; }

        public string Email { get; init; }

        public IEnumerable<Hobbies> Hobbies { get; set; }
    }
}