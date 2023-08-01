using AutoMapper;
using Mango.Services.CouponAPI.Data;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Mango.Services.CouponAPI.Controller
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponAPIController : ControllerBase
    {
        //Dependency Injection
        private readonly DbContextCouponApi _dbase;
        private readonly ResponseDto _response;
        //Injects the IMapper interface
        private IMapper _mapper;

        //Constructor where we are to inject the database context
        public CouponAPIController(DbContextCouponApi dbaseContext, IMapper mapper)
        {
            _dbase = dbaseContext;
            _mapper = mapper;

            //Remember that the user is not going to enter any response, and it is not going to be declared when the class is called
            _response = new ResponseDto();
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                //Become into list the Coupons which are in the database
                IEnumerable<Coupon> objList = _dbase.Coupons.ToList();
                //_response.Result = objList;
                //In this part it is good idea to use mapper to reflect in the response the CouponDto
                //IEnumerable<CouponDto> = destino
                //objList = resource
                _response.Result = _mapper.Map<IEnumerable<CouponDto>>(objList);
                return _response;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
        //Get request to get a specific coupon with the Id
        [HttpGet]
        [Route("{id}")]
        public Object Get(int id)
        {
            try
            {
                Coupon couponId = _dbase.Coupons.First(c => c.CouponId == id);
                _response.Result = couponId;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        //Get the coupon by the code
        [HttpGet]
        [Route("GetByCode/{code}")]
        public ResponseDto GetByCode(string code)
        {
            try
            {
                Coupon obj = _dbase.Coupons.First(c => c.CouponCode.ToLower() == code.ToLower());
                _response.Result = _mapper.Map<CouponDto>(obj);

            }

            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            return _response;
        }

        [HttpPost]
        //It does not make sense at all to add it bcos it wont have a specific information I mean is too much and is extracted from the response
        //[Route("{couponDto}")]
        public ResponseDto Post([FromBody] CouponDto couponDto) 
        {
            try
            {
                //In this case the user write as input the CouponDto and it is mapping into the object
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _dbase.Coupons.Add(obj);
                _dbase.SaveChanges();
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message; 
            }
            return _response;
        }

        [HttpPut]
        public ResponseDto Put([FromBody] CouponDto couponDto)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);
                _dbase.Coupons.Update(obj);
                _dbase.SaveChanges();

                _response.Result = _mapper.Map<CouponDto>(obj);
            }
            catch( Exception ex) 
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message; 
            }
            return _response;
        }

        [HttpDelete]
        [Route("{id}")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Coupon obj = _dbase.Coupons.First(c => c.CouponId == id);
                _dbase.Coupons.Remove(obj);
                _dbase.SaveChanges();

                _response.Result = _mapper.Map<IEnumerable<CouponDto>>(obj);    
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


    }
}
