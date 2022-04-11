namespace NewsMedia.Data
{
    public class CommentItem
    {
        public int Id { get; set; }

        public string CreatedBy { get; set; }

        public string CommentText { get; set; } = string.Empty;

        public int ReportId { get; set; }   // BC change to int from string

        public DateTime DateCreated { get; set; }
    }
}
