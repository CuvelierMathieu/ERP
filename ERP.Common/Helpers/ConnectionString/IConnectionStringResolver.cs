using System;

namespace ERP.Common.Helpers.ConnectionString
{
    public interface IConnectionStringResolver : IDisposable
    {
        string GetConnectionString();
    }
}