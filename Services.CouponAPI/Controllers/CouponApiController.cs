using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.CouponAPI.Data;
using Services.CouponAPI.Dtos;
using Services.CouponAPI.Models;
using System.Collections.Generic;

namespace Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponApiController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICouponRepository _couponRepository;

        public CouponApiController(IMapper mapper, ICouponRepository couponRepository)
        {
            _mapper = mapper;
            _couponRepository = couponRepository;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CouponDto>>> GetAllCouponsAsync()
        {
            var couponitems = await _couponRepository.GetAllCoupons();
            if (couponitems == null || !couponitems.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<CouponDto>>(couponitems));
        }

        [HttpGet]
        [Route("GetById/{id:int}")]
        public async Task<ActionResult<CouponDto>> GetCouponById(int id)
        {
            var couponitem = await _couponRepository.GetById(id);
            if (couponitem == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CouponDto>(couponitem));
        }

        [HttpGet]
        [Route("GetByCode/{code}")]
        public async Task<ActionResult<CouponDto>> GetCouponByCode(string code)
        {
            var couponitem = await _couponRepository.GetByCode(code);
            if (couponitem == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CouponDto>(couponitem));
        }
        [HttpPost]
        public async Task<ActionResult> CreateCoupon([FromBody] CouponDto couponDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                var coupon = _mapper.Map<Coupon>(couponDto);
                var coupon_id = await _couponRepository.CreateCoupon(coupon);
                return CreatedAtAction(nameof(GetCouponById), new { id = coupon_id }, couponDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return StatusCode(500, "An error occurred while creating the coupon.");
            }
        }

        [HttpPut]
        [Route("UpdateCoupon/{id:int}")]
        public async Task<ActionResult> UpdateCoupon(int id, [FromBody] CouponDto updatedCouponDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var existingCoupon = await _couponRepository.GetById(id);
            if (existingCoupon == null)
            {
                return NotFound();
            }
            _mapper.Map(updatedCouponDto,existingCoupon);
            try
            {
                if (await _couponRepository.UpdateCoupon(existingCoupon))
                {
                    return Ok(existingCoupon);
                }
                else
                {
                    return StatusCode(500, "An error occurred while updating the coupon.");
                }
            }
            catch (Exception)
            {

                return StatusCode(500, "An error occurred while updating the coupon.");
            }
        }
        [HttpPost("id:int")]

        public async Task<ActionResult> DeleteCoupon(int id)
        {
            if (await _couponRepository.DeleteCoupon(id))
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

    }
}
