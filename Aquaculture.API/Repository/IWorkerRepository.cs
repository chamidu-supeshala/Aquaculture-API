using Aquaculture.API.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aquaculture.API.Repository
{
    public interface IWorkerRepository
    {
        public Task<List<Worker>> GetAll();
        public Task<Worker> GetById(long workerId);
        public Task<Worker> Add(Worker worker);
        public Task<Worker> Update(Worker worker);
        public Task<long> DeleteById(long farmId);
    }
}
