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
    public class FarmsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFarmRepository _farmRepository;

        public FarmsController(IFarmRepository farmRepository, IMapper mapper)
        {
            _mapper = mapper;
            _farmRepository = farmRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFarms()
        {
            List<Farm> farms = await _farmRepository.GetAll();
            return Ok(_mapper.Map<List<Farm>, List<FarmDto>>(farms));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFarmById([FromRoute] long id)
        {
            Farm farm = await _farmRepository.GetById(id);
            if (farm == null)
                return NotFound();

            return Ok(_mapper.Map<Farm, FarmDto>(farm));
        }

        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> AddFarm([FromForm] FarmDto farmDto)
        {
            try
            {
                IFormFile file = Request.Form.Files.FirstOrDefault(f => f.Length > 0);
                if (file == null)
                    return Content("File not selected");

                string path = ImageUploadHelper.SaveImage(file);

                Farm farmToSave = _mapper.Map<FarmDto, Farm>(farmDto);
                farmToSave.ImageUrl = path;

                Farm farm = await _farmRepository.Add(farmToSave);
                return Ok(_mapper.Map<Farm, FarmDto>(farm));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFarm([FromForm] FarmDto farmDto, [FromRoute] long id)
        {
            try
            {
                Farm farmToSave = _mapper.Map<FarmDto, Farm>(farmDto);

                IFormFile file = Request.Form.Files.FirstOrDefault(f => f.Length > 0);
                if (file != null)
                {
                    string path = ImageUploadHelper.SaveImage(file);
                    farmToSave.ImageUrl = path;
                }

                farmToSave.FarmId = id;
                Farm farm = await _farmRepository.Update(farmToSave);
                return Ok(_mapper.Map<Farm, FarmDto>(farm));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFarmById([FromRoute] long id)
        {
            await _farmRepository.DeleteById(id);
            return Ok();
        }
    }
}
