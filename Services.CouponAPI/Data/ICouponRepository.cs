using Services.CouponAPI.Models;

namespace Services.CouponAPI.Data
{
    public interface ICouponRepository
    {
        Task<IEnumerable<Coupon>> GetAllCoupons();
        Task<Coupon> GetById(int id);
        Task<Coupon> GetByCode(string code);
        Task<int> CreateCoupon(Coupon coupon);
        Task<bool> UpdateCoupon(Coupon coupon);
        Task<bool> DeleteCoupon(int id);
    }
}
