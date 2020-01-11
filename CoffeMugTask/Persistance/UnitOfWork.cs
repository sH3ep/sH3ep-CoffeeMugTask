using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoffeeMugTask.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        private CmtContext _context;

        public UnitOfWork (CmtContext context)
        {
            _context = context;
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task Dispose()
        {
            await _context.DisposeAsync();
        }
    }
}
