using Newtonsoft.Json;
using System;
using System.Collections.Generic;

#nullable disable

namespace SharedLibrary.Data
{
    public partial class Status
    {
        public Status()
        {
            Issues = new HashSet<Issue>();
        }

        public int Id { get; set; }
        public string StatusText { get; set; }

        [JsonIgnore]
        public virtual ICollection<Issue> Issues { get; set; }
    }
}
