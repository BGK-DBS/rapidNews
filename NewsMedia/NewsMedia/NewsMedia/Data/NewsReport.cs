using System;
using System.ComponentModel.DataAnnotations;

namespace NewsMedia.Data
{
    public class NewsReport
    {

        public int Id { get; set; }
        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime CreationDate { get; set; }

        public string Category { get; set; }

        public string? CreationEmail { get; set; }
    }
}
