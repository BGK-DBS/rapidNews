using System;
using System.ComponentModel.DataAnnotations;

namespace NewsMedia.Data
{
    public class NewsReport
    {

        public int Id { get; set; }
        public string Title { get; set; }

        public string Body { get; set; }

        [Display(Name = "Creation Date")]
        public DateTime CreationDate { get; set; }

        public DateTime LastModifiedDate { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Email Address")]
        public string CreationEmail { get; set; }

        [Display(Name = " Is Publish?")]
        public Boolean IsPublished { get; set; }

    }
}
