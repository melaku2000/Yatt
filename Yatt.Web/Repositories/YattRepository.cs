using Yatt.Models.Dtos;
using Yatt.Models.Enums;
using Yatt.Models.RequestFeatures;
using Yatt.Web.Features;
using Yatt.Web.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Json;

namespace Yatt.Web.Repositories
{
    public class YattRepository<T> : IYattRepository<T> where T : class
    {
        private readonly HttpClient _client;
        private readonly ITokenManagerService tokenManager;
        private readonly JsonSerializerOptions _options;
        public YattRepository(HttpClient client, ITokenManagerService tokenManager)
        {
            _client= client;
            this.tokenManager = tokenManager;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<ResponseDto<T>> Create(string url,T item)
        {
            await SetClientToken();

            var content = JsonSerializer.Serialize(item);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var postResult = await _client.PostAsync(url, bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();
            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
            var resp = JsonSerializer.Deserialize<ResponseDto<T>>(postContent, _options);
            if (resp!.Status == ResponseStatus.Success)
            {
                return resp;
            }
            return null;
        }

        public async Task<ResponseDto<T>> Delete(string url, string id)
        {
            await SetClientToken();

            var response = await _client.DeleteAsync($"{url}/{id}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var resp = JsonSerializer.Deserialize<ResponseDto<T>>(content, _options);

            return resp!;
        }

        public async Task<T> GetById(string url, string id)
        {
            await SetClientToken();
            var response = await _client.GetAsync($"{url}/{id}");
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var resp = JsonSerializer.Deserialize<ResponseDto<T>>(content, _options);

            if (resp!.Status==ResponseStatus.Success)
            {
                return resp.Model;
            }
            return null;
        }

        public async Task<List<T>> GetLists(string url)
        {
            await SetClientToken();
          
            var response = await _client.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var resp = JsonSerializer.Deserialize<ResponseDto<List<T>>> (content, _options);
            if (resp!.Status == ResponseStatus.Success)
            {
                return resp.Model;
            }
            return null;
        }

        public async Task<PagingResponse<T>> GetPagedList(string url, PageParameter pageParameters)
        {
            await SetClientToken();
           
            var queryStringParam = new Dictionary<string, string>
            {
                ["pageSize"] = pageParameters.PageSize.ToString(),
                ["pageNumber"] = pageParameters.PageNumber.ToString(),
                ["searchTerm"] = pageParameters.SearchTerm == null ? "" : pageParameters.SearchTerm,
                ["orderBy"] = pageParameters.OrderBy == null ? "" :pageParameters.OrderBy
            };
            var response = await _client.GetAsync(QueryHelpers.AddQueryString(url, queryStringParam));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }
            var pagingResponse = new PagingResponse<T>
            {
                Items = JsonSerializer.Deserialize<List<T>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(response.Headers.GetValues("X-Pagination").First(), _options)
            };
            return pagingResponse;
        }

        public async Task<ResponseDto<T>> Update(string url,T item)
        {
            await SetClientToken();
           
            var content = JsonSerializer.Serialize(item);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var postResult = await _client.PutAsync(url, bodyContent);
            var postContent = await postResult.Content.ReadAsStringAsync();
            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
            var resp = JsonSerializer.Deserialize<ResponseDto<T>>(postContent, _options);
            if (resp!.Status == ResponseStatus.Success)
            {
                return resp;
            }
            return null;
        }

        private async Task SetClientToken()
        {
            string token = await tokenManager.GetToken();

            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
        }
    }
}
