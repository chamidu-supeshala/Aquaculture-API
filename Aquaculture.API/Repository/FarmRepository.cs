using Aquaculture.API.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aquaculture.API.Repository
{
    public class FarmRepository : IFarmRepository
    {
        private readonly AquacultureContext _context;

        public FarmRepository(AquacultureContext context)
        {
            _context = context;
        }

        public async Task<List<Farm>> GetAll()
        {
            return await _context.Farms
                .Include(f => f.Workers)
                .ToListAsync();
        }

        public async Task<Farm> GetById(long farmId)
        {
            return await _context.Farms
                .FindAsync(farmId);
        }

        public async Task<Farm> Add(Farm farm)
        {
            _context.Farms.Add(farm);
            await _context.SaveChangesAsync();
            return farm;
        }

        public async Task<Farm> Update(Farm farm)
        {
            _context.Farms.Update(farm);
            await _context.SaveChangesAsync();
            return farm;
        }

        public async Task<long> DeleteById(long farmId)
        {
            _context.Farms.Remove(new Farm() { FarmId = farmId});
            await _context.SaveChangesAsync();
            return farmId;
        }
    }
}
