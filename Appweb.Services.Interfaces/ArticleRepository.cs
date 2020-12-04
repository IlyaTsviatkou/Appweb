using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Appweb.Domain.Core;
using Appweb.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Appweb.Services.Business
{
    public class ArticleRepository : IRepository
    {
        private readonly ApplicationContext _context;
        public ArticleRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task Create(Collection collection)
        {
            _context.Add(collection);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Collection collection)
        {
            _context.Update(collection);
            await _context.SaveChangesAsync();
        }

        public bool Any(string id)
        {
            return _context.Collections.Any(e => e.CollectionID == id); ;
        }
        public async Task Remove(Collection collection)
        {
            _context.Collections.Remove(collection);
            await _context.SaveChangesAsync();
        }

        public async Task<Collection> FirstOrDefaultAsync(string? id)
        {
            return await _context.Collections.FirstOrDefaultAsync(m => m.CollectionID == id);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
