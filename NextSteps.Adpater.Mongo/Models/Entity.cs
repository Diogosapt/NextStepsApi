using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace NextSteps.Adpater.Mongo.Models
{
    [NotMapped]
    public abstract class Entity : IEntity
    {
        public string CreatedBy { get; set; }

        public DateTime CreateDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}