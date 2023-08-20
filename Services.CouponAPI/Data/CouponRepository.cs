using Microsoft.EntityFrameworkCore;
using Services.CouponAPI.Dtos;
using Services.CouponAPI.Models;
using System.Threading;

namespace Services.CouponAPI.Data
{
    public class CouponRepository : ICouponRepository

    {
        private readonly AppDbContext _db;

        public CouponRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Coupon>> GetAllCoupons()
        {
            var coupons = await _db.Coupons.ToListAsync();
            return coupons;
        }

        public async Task<Coupon> GetByCode(string code)
        {
            var coupon = await _db.Coupons.FirstOrDefaultAsync(coupon => coupon.CouponCode.ToLower() == code.ToLower());
            return coupon;
        }

        public async Task<Coupon> GetById(int id)
        {
            var coupon = await _db.Coupons.FirstOrDefaultAsync(coupon => coupon.CouponId == id);
            return coupon;
        }

        public async Task<int> CreateCoupon(Coupon coupon)
        {
            _db.Coupons.AddAsync(coupon);
            await _db.SaveChangesAsync();
            return coupon.CouponId;
        }

        public async Task<bool> UpdateCoupon(Coupon coupon)
        {
            try
            {
                _db.Coupons.Update(coupon);
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<bool> DeleteCoupon(int id)
        {
            var coupon = await _db.Coupons.FirstOrDefaultAsync(coupon => coupon.CouponId == id);

            if (coupon == null)
            {
                return false;
            }
            try
            {
                await _db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

    }
}
