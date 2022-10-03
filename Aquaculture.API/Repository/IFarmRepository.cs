using Aquaculture.API.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aquaculture.API.Repository
{
    public interface IFarmRepository
    {
        public Task<List<Farm>> GetAll();
        public Task<Farm> GetById(long farmId);
        public Task<Farm> Add(Farm farm);
        public Task<Farm> Update(Farm farm);
        public Task<long> DeleteById(long farmId);
    }
}
