using System;
namespace Assignment.Domain.Common
{
	public class BaseEntity
	{
        public DateTime CreatedOn { get; set; }

        public int CreatedBy { get; set; }

        public DateTime ModifiedOn { get; set; }

        public int ModifiedBy { get; set; }
    }
}

