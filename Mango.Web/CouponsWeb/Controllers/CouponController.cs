using CouponsWeb.Models;
using CouponsWeb.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace CouponsWeb.Controllers
{
    public class CouponController : Controller

    {
        //We are going to apply dependency injection to use the Coupon API Controller
        //Por que en la interfaz de IcouponService se iplementan las acciones del controlador de la API
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService) 
        {
            _couponService = couponService; 
        }

        public async Task <IActionResult> CouponIndex()
        {
            List<CouponDto>? list = new();
            ResponseDto? response = await _couponService.GetAllCouponsAsync();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
            }
           // else {
             //   throw new Exception("information not loaded");
            //}
         
            return View(list);  
        }

        public async Task<IActionResult> CouponCreate()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CouponCreate(CouponDto couponDto) 
        {
            //Validation from server side
            if (ModelState.IsValid) 
            {
                ResponseDto? response = await _couponService.CreateCouponsAsync(couponDto);

                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Coupon created successfully";
                    return RedirectToAction(nameof(CouponIndex));
                }
                else 
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(couponDto);
        }

        /*Delete action
         Part 1 , in this section we are going to get the information about the Coupon that the
        user selects*/
        public async Task<IActionResult> CouponDelete(int CouponId) 
        {

            ResponseDto? response = await _couponService.GetCoupoinById(CouponId);
            if (response != null && response.IsSuccess) 
            {
                CouponDto? model = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        /*Delete action for the botton which is in the list*/
        [HttpPost]
        public async Task<IActionResult> CouponDelete(CouponDto couponDto) 
        {
            //In this section is where is called the method to delete the coupon
            ResponseDto? response = await _couponService.DeleteCouponAsync(couponDto.CouponId);
            if (response != null && response.IsSuccess) 
            {
                return RedirectToAction(nameof(CouponIndex));
            }
            return View(couponDto);
        }
    }
}
