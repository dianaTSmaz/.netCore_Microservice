using CouponsWeb.Models;

namespace CouponsWeb.Service.IService
{
    public interface IBaseService
    {
      public Task<ResponseDto?> SendAsync(RequestDto requestDto);
    }
}
