using NewsMedia.Data;
using System.Collections.Generic;

namespace NewsMedia.Models
{
    public class ReportComments
    {
        public NewsReportViewModel NewsReportItem { get; set; }
        
        public List<CommentItem> CommentsList { get; set; }

        public CommentItem CommentItem { get; set; }
    }
}
