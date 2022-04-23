using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace NewsMedia.Models
{
    public class NewsReportViewModel

    {
        public int Id { get; set; }
        public string Title { get; set; }

        [Display(Name = "News Report")]
        public string Body { get; set; }

        [Display(Name = "Date Created")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Category")]
        public string CategoryName { get; set; }


        [Display(Name = "Created By")]
        public string? CreationEmail { get; set; }

        [Display(Name = "Published")]
        public Boolean IsPublished { get; set; }

    }
}
