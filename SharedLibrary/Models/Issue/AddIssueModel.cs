using System;
using System.Collections.Generic;
using System.Text;
using SharedLibrary.Data;

namespace SharedLibrary.Models.Issue
{
    public class AddIssueModel
    {

        public DateTime Created { get; set; }
        public string Status { get; set; }
        public string Text { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }

        //public Data.Customer customer { get; set; }
        //public Data.User user { get; set; }
    }
}
