using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextSteps.Adpater.Mongo.Models
{
    public class Hobbies
    {
        public Guid Id { get; set; }

        public string Hobby { get; set; }

        public Guid PersonId { get; set; }

        public Person Person { get; set; }
    }
}