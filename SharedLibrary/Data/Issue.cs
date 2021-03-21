using System;
using System.Collections.Generic;

#nullable disable

namespace SharedLibrary.Data
{
    public partial class Issue
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Changed { get; set; }
        public string Information { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public int StatusId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Status Status { get; set; }
        public virtual User User { get; set; }
    }
}
