using System;
using System.Collections.Generic;
using System.Text;

namespace SharedLibrary.Models.Issue
{
    public class AddIssueModel
    {

        public DateTime Created { get; set; }
        public string Information { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }
        public int StatusId { get; set; }

    }
}
