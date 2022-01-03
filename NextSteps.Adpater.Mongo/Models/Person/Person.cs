using System;
using System.Collections.Generic;

namespace NextSteps.Adpater.Mongo.Models
{
    public class Person : Entity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Job { get; set; }

        public DateTime Birthday { get; set; }

        public string Email { get; set; }

        public IEnumerable<Hobbies> Hobbies { get; set; }
    }
}