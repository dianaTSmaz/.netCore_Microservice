using CouponsWeb.Utility;
using static CouponsWeb.Utility.StartingDetails;

namespace CouponsWeb.Models
{
    public class RequestDto
    {
        public ApiType ApiType { get; set; } = ApiType.GET;

        public string Url { get; set; }
        public object Data { get; set; }

        public string AccessToken { get; set; }
    }
}