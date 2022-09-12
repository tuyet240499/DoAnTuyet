using BanHang.EF;
using BanHang.Services.Infrastructure;

namespace BanHang.Services.Infrastructure
{
    public class DbFactory : IDbFactory
    {

        private BanHangDbContext dbContext;
        public void Dispose()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }

        public BanHangDbContext Init()
        {
            return dbContext ?? (dbContext = new BanHangDbContext());
        }
    }
}