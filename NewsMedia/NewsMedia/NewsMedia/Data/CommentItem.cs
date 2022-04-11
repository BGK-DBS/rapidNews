namespace NewsMedia.Data
{
    public class CommentItem
    {
        public int Id { get; set; }

        public string CreatedBy { get; set; }

        public string CommentText { get; set; } = string.Empty;

        public int ReportId { get; set; }   

        public DateTime DateCreated { get; set; }
    }
}
