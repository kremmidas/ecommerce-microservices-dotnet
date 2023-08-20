using AutoMapper;
using Services.CouponAPI.Dtos;
using Services.CouponAPI.Models;

namespace Services.CouponAPI.Profiles
{
    public class CouponsProfile : Profile
    {
        public CouponsProfile()
        {
            CreateMap<CouponDto, Coupon>();
            CreateMap<Coupon, CouponDto>();
        }
    }
}
