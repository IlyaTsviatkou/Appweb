using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Appweb.Domain.Core;

namespace Appweb.Services.Business
{
    interface IRepository : IDisposable
    {
        Task Create(Collection collection);
        Task Update(Collection collection);
        bool Any(string id);
        Task Remove(Collection collection);
        Task<Collection> FirstOrDefaultAsync(string? id);
    }
}
