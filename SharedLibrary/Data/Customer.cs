using System;
using System.Collections.Generic;

#nullable disable

namespace SharedLibrary.Data
{
    public partial class Customer
    {
        public Customer()
        {
            Issues = new HashSet<Issue>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Issue> Issues { get; set; }
    }
}
