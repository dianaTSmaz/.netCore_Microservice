using CouponsWeb.Models;

namespace CouponsWeb.Service.IService
{
    public interface ICouponService
    {
        public Task<ResponseDto?> GetAllCouponsAsync();
        public Task<ResponseDto?> GetCouponAsync(string CouponCode);
        public Task<ResponseDto?> GetCoupoinById(int id);
        public Task<ResponseDto?> CreateCouponsAsync(CouponDto couponDto);

        public Task<ResponseDto?> UpdateCouponsAync(CouponDto couponDto);

        public Task<ResponseDto?> DeleteCouponAsync(int id);

    }
}
