using Microsoft.AspNetCore.Mvc;
using NewsMedia.Data;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace NewsMedia.Services
{
    public class CommentsApiClient
    {
        public HttpClient Client { get; set; }

        public CommentsApiClient(HttpClient client)
        {

            client.BaseAddress = new System.Uri("https://localhost:44366");

            client.DefaultRequestHeaders.Add("Accept", "application/json");

            Client = client;

        }

        public async Task<IEnumerable<CommentItem>> GetCommentList()

        {
            return await Client.GetFromJsonAsync<IEnumerable<CommentItem>>("api/CommentItems");
        }


        //public async Task<NewsReport> GetReportItem(int ReportId)

        //{
        //    var reportID = ReportId.ToString();
        //    return await Client.GetFromJsonAsync<NewsReport>("api/ReportItems/" + reportID);
        //}

        //public async Task CreateReportItem(NewsReport reportItem)

        //{
        //    await Client.PostAsJsonAsync<NewsReport>("api/ReportItems", reportItem);
        //    return;
        //}

        //public async Task UpdateReportItem(int ReportId, NewsReport reportItem)

        //{
        //    var reportID = ReportId.ToString();
        //    await Client.PutAsJsonAsync("api/ReportItems/" + reportID, reportItem);
        //    return;

        //}
        //public async Task DeleteReportItem(int ReportId)

        //{
        //    var reportID = ReportId.ToString();
        //    await Client.DeleteAsync("api/ReportItems/" + reportID);
        //    return;
        //}

    }
}

