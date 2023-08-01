using CouponsWeb.Models;
using CouponsWeb.Service.IService;
using CouponsWeb.Utility;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;

namespace CouponsWeb.Service
{

    //This is the class CouponService that it is used to implement the interface service and assigned correctly the url with the according request
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;
        public CouponService(IBaseService baseService)
        {
            _baseService = baseService; 
            
        }
       public async Task<ResponseDto?> CreateCouponsAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StartingDetails.ApiType.POST,
                Data = couponDto,
                Url = StartingDetails.CouponAPIBase + "/api/coupon"
            });

        }

        public async Task<ResponseDto?> DeleteCouponAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = StartingDetails.ApiType.DELETE,
                Url = StartingDetails.CouponAPIBase + "/api/coupon/"+ id
            }) ;
        }

        public async Task<ResponseDto?> GetAllCouponsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            //Object Initilizer    
            {
                //here we are going to use the enumerator to specofy the kind of request 
                ApiType = StartingDetails.ApiType.GET,
                Url = StartingDetails.CouponAPIBase + "/api/coupon"
               
            });
        }

        async Task<ResponseDto?> ICouponService.GetCoupoinById(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StartingDetails.ApiType.GET,
                Url = StartingDetails.CouponAPIBase + "/api/coupon/" + id
            }) ;
        }

        async Task<ResponseDto?> ICouponService.GetCouponAsync(string CouponCode)
        {
            return await _baseService.SendAsync(new RequestDto() 
            {
                ApiType = StartingDetails.ApiType.GET,
                Url = StartingDetails.CouponAPIBase + "api/coupon/GetByCode/"+ CouponCode
            });
        }

        async Task<ResponseDto?> ICouponService.UpdateCouponsAync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StartingDetails.ApiType.PUT,
                Data = couponDto,
                Url = StartingDetails.CouponAPIBase + "api/coupon" }) ;
        }
    }
}
