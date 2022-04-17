using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using NewsMedia.Data;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

//// The purpose of this service is to call the Report web api to perform the following: 
//       Get
//       Get{id}
//       Put{id}
//       Create
//       Delete{id}
//       Delete{ReportID}
//       Get(Search Parameters - CreatedBy and ReportID}

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

        public async Task<CommentItem> GetCommentItem(int CommentId)

        {
            var CommentID = CommentId.ToString();
            return await Client.GetFromJsonAsync<CommentItem>("api/CommentItems/" + CommentID);
        }

        public async Task CreateCommentItem(CommentItem commentItem)

        {
            await Client.PostAsJsonAsync<CommentItem>("api/CommentItems", commentItem);
            return;
        }

        public async Task UpdateCommentItem(int CommentId, CommentItem commentItem)

        {
            var commentID = CommentId.ToString();
            await Client.PutAsJsonAsync("api/CommentItems/" + commentID, commentItem);
            return;

        }

        public async Task DeleteCommentItem(int CommentId)

        {
            var CommentID = CommentId.ToString();
            await Client.DeleteAsync("api/CommentItems/" + CommentID);
            return;
        }

        // BC - Call to Comments webapi for search with created by and/or reportid 
        public async Task<IEnumerable<CommentItem>> GetCommentListByFilter(string createdBySearch, int reportIDSearch)

        {
            var searchQuery = new QueryBuilder();

            if (!string.IsNullOrEmpty(createdBySearch))
            {
                searchQuery.Add("createdBySearch", createdBySearch.ToString());
            }

            if (reportIDSearch != 0)
            {
                searchQuery.Add("reportIdSearch", reportIDSearch.ToString());
            }
            return await Client.GetFromJsonAsync<IEnumerable<CommentItem>>("api/CommentItems/FilterComments" + searchQuery);
        }
    }
}

