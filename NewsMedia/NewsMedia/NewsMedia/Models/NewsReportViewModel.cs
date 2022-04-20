using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace NewsMedia.Models
{
    public class NewsReportViewModel

    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime CreationDate { get; set; }

        public string CategoryName { get; set; }

        public string? CreationEmail { get; set; }

        public Boolean IsPublished { get; set; }

    }
}
