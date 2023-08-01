using CouponsWeb.Models;
using CouponsWeb.Service.IService;
using CouponsWeb.Utility;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using static CouponsWeb.Utility.StartingDetails;

namespace CouponsWeb.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        //Constructor
        public BaseService(IHttpClientFactory httpClientFactory) 
        {
            _httpClientFactory = httpClientFactory; 
        }


        //Method to make the requests, here we have to implement the 
        //httpclient to make the requests
        public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
        {
            HttpClient client = _httpClientFactory.CreateClient("MangoAPI");
            HttpRequestMessage message = new();
            message.Headers.Add("Accept","application/json");

            //here we have to add the token

            message.RequestUri = new Uri(requestDto.Url);
            if (requestDto.Data != null) 
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(requestDto),Encoding.UTF8,"application/json");
            }

            HttpResponseMessage? apiResponse = null;

            switch(requestDto.ApiType)
                {
                case ApiType.POST:
                    message.Method = HttpMethod.Post;
                    break;

                case ApiType.PUT:
                    message.Method = HttpMethod.Put;
                    break;

                case ApiType.DELETE:
                    message.Method = HttpMethod.Delete;
                    break;

                default:
                    message.Method=HttpMethod.Get;
                    break;
                }

            //Here we are going to wait until the request of the user is processed 
            apiResponse = await client.SendAsync(message);

            //After that we can check the status code of the request
            //Here we are going to return a ResponseDto object (we change the IsSuccess and Message)
            try
            {
                switch (apiResponse.StatusCode)
                {
                    case System.Net.HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not Found" };
                    case System.Net.HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unauthorized" };
                    case System.Net.HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "Internal Server Error" };
                    case System.Net.HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "Access Denied" };
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        return apiResponseDto;
                }


            }
            catch (Exception ex)
            {
                //Object Initializer
                var dto = new ResponseDto
                {
                    Message = ex.Message.ToString(),
                    IsSuccess = false
                };
                return dto;

            }

        }  

    }
}
