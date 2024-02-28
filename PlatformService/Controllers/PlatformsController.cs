using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using PlatformService.Data.Interfaces;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.http;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController(
        IPlatformRepo platformRepo,
        IMapper mapper,
        ICommandDataClient commandDataClient) : ControllerBase
    {
        private readonly IPlatformRepo _platformRepo = platformRepo;
        private readonly IMapper _mapper = mapper;
        private readonly ICommandDataClient _commandDataClient = commandDataClient;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlatformReadDto>>> GetPlatforms()
        {
            var data = await _platformRepo.GetAllAsync();
            if (!data.Any())
                return NotFound();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(data));
        }

        [HttpGet("{id}", Name = "GetById")]
        public async Task<ActionResult<PlatformReadDto>> GetById(long id)
        {
            var data = await _platformRepo.GetByIdAsync(id);
            if (data == null)
                return NotFound();
            return Ok(_mapper.Map<PlatformReadDto>(data));
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> Create([FromBody] PlatformCreateDto platformCreateDto)
        {
            var platformModel = _mapper.Map<Platform>(platformCreateDto);
            _platformRepo.Create(platformModel);
            await _platformRepo.SaveChangesAsync();
            var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);
            try
            {
                await _commandDataClient.SendPlatformToCommand(platformReadDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Couldn't send synchronously: {ex.Message}");
            }
            return CreatedAtRoute(nameof(GetById), new { id = platformReadDto.Id }, platformReadDto);
        }
    }
}