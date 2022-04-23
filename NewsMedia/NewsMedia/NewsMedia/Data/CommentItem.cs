using System.ComponentModel.DataAnnotations;

namespace NewsMedia.Data
{
    public class CommentItem
    {
        public int Id { get; set; }

        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Display(Name = "Comment")]
        public string CommentText { get; set; } = string.Empty;

        [Display(Name = "Report Id")]
        public int ReportId { get; set; }   // BC change to int from string

        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
    }
}
