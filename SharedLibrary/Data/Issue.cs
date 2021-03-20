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
        public string Status { get; set; }
        public string Text { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual User User { get; set; }


    }
}
