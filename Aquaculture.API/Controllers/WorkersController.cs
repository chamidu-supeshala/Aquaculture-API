using Aquaculture.API.Data;
using Aquaculture.API.Dto;
using Aquaculture.API.Helpers;
using Aquaculture.API.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aquaculture.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWorkerRepository _workerRepository;

        public WorkersController(IWorkerRepository workerRepository, IMapper mapper)
        {
            _mapper = mapper;
            _workerRepository = workerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWorkers()
        {
            List<Worker> workers = await _workerRepository.GetAll();
            return Ok(_mapper.Map<List<Worker>, List<WorkerDto>>(workers));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkerById([FromRoute] long id)
        {
            Worker worker = await _workerRepository.GetById(id);
            if (worker == null)
                return NotFound();

            return Ok(_mapper.Map<Worker, WorkerDto>(worker));
        }

        [HttpPost]
        public async Task<IActionResult> AddWorker([FromForm] WorkerDto workerDto)
        {
            try
            {
                Worker workerToSave = _mapper.Map<WorkerDto, Worker>(workerDto);

                IFormFile file = Request.Form.Files.FirstOrDefault(f => f.Length > 0);
                if (file != null)
                {
                    string path = ImageUploadHelper.SaveImage(file);
                    workerToSave.ImageUrl = path;
                }

                Worker worker = await _workerRepository.Add(workerToSave);
                return Ok(_mapper.Map<Worker, WorkerDto>(worker));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorker([FromForm] WorkerDto workerDto, [FromRoute] long id)
        {
            try
            {
                Worker workerToUpdate = _mapper.Map<WorkerDto, Worker>(workerDto);

                IFormFile file = Request.Form.Files.FirstOrDefault(f => f.Length > 0);
                if (file != null)
                {
                    string path = ImageUploadHelper.SaveImage(file);
                    workerToUpdate.ImageUrl = path;
                }

                workerToUpdate.WorkerId = id;
                Worker worker = await _workerRepository.Update(workerToUpdate);
                return Ok(_mapper.Map<Worker, WorkerDto>(worker));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkerById([FromRoute] long id)
        {
            await _workerRepository.DeleteById(id);
            return Ok();
        }

    }
}
