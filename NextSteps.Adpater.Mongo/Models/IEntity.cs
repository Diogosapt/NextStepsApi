using System;

namespace NextSteps.Adpater.Mongo.Models
{
    public interface IEntity
    {
        public string CreatedBy { get; set; }

        public DateTime CreateDate { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime UpdateDate { get; set; }
    }
}