using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using NewsMedia.Data;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

// The purpose of this service is to call the Report web api to perform the following: 
//       Get
//       Get{id}
//       Put{id}
//       Create
//       Delete{id}


namespace NewsMedia.Services
{
    public class ReportsApiClient
    {
        public HttpClient Client { get; set; }

        public ReportsApiClient(HttpClient client)
        {

            client.BaseAddress = new System.Uri("https://localhost:7011");

            client.DefaultRequestHeaders.Add("Accept", "application/json");

            Client = client;

        }

        public async Task<IEnumerable<NewsReport>> GetReportList()

        {
            return await Client.GetFromJsonAsync<IEnumerable<NewsReport>>("api/ReportItems");
        }

        public async Task<IEnumerable<NewsReport>> GetReportListByFilter(string creationEmail, int searchCategory)

        {
            var searchQuery = new QueryBuilder();

            if (!string.IsNullOrEmpty(creationEmail))
            {
                searchQuery.Add("creationEmail", creationEmail.ToString());
            }

            if (searchCategory != 0)
            {
                    searchQuery.Add("categoryId", searchCategory.ToString());
            }
            return await Client.GetFromJsonAsync<IEnumerable<NewsReport>>("api/ReportItems/FilterReports" + searchQuery);
        }

        public async Task<NewsReport> GetReportItem(int ReportId)

        {
            var reportID = ReportId.ToString();
            return await Client.GetFromJsonAsync<NewsReport>("api/ReportItems/" + reportID);
        }

        public async Task CreateReportItem(NewsReport reportItem)

        {
            await Client.PostAsJsonAsync<NewsReport>("api/ReportItems", reportItem);
            return;
        }

        public async Task UpdateReportItem(int ReportId, NewsReport reportItem)

        {
            var reportID = ReportId.ToString();
            await Client.PutAsJsonAsync("api/ReportItems/" + reportID, reportItem);
            return;

        }
        public async Task DeleteReportItem(int ReportId)

        {
            var reportID = ReportId.ToString();
            await Client.DeleteAsync("api/ReportItems/" + reportID);
            return;
        }

    }
}

