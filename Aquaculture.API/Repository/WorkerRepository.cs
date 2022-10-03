using Aquaculture.API.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aquaculture.API.Repository
{
    public class WorkerRepository : IWorkerRepository
    {
        private readonly AquacultureContext _context;

        public WorkerRepository(AquacultureContext context)
        {
            _context = context;
        }

        public async Task<List<Worker>> GetAll()
        {
            return await _context.Workers
                .ToListAsync();
        }

        public async Task<Worker> GetById(long workerId)
        {
            return await _context.Workers
                .Include(w => w.Farm)
                .FirstOrDefaultAsync(w => w.WorkerId == workerId);
        }

        public async Task<Worker> Add(Worker worker)
        {
            _context.Workers.Add(worker);
            await _context.SaveChangesAsync();
            return worker;
        }

        public async Task<Worker> Update(Worker worker)
        {
            _context.Workers.Update(worker);
            await _context.SaveChangesAsync();
            return worker;
        }

        public async Task<long> DeleteById(long workerId)
        {
            _context.Workers.Remove(new Worker() { WorkerId = workerId });
            await _context.SaveChangesAsync();
            return workerId;
        }
    }
}
