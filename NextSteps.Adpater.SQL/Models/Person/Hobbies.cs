using System;

namespace NextSteps.Adpater.SQL.Models
{
    public class Hobbies
    {
        public Guid Id { get; set; }

        public string Hobby { get; set; }

        public Guid PersonId { get; set; }

        public Person Person { get; set; }
    }
}