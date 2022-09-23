using BanHang.EF;

using System;

namespace BanHang.Services.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        BanHangDbContext Init();
    }
}