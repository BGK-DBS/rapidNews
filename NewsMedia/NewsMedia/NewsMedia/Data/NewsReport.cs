using System;
using System.ComponentModel.DataAnnotations;

namespace NewsMedia.Data
{
    public class NewsReport
    {

        public int Id { get; set; }
        public string Title { get; set; }

        [Display(Name = "News Report")]
        public string Body { get; set; }

        [Display(Name = "Date Created")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Date Last Modified")]
        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Created By")]
        public string CreationEmail { get; set; }

        [Display(Name = "Published")]
        public Boolean IsPublished { get; set; }

    }
}
