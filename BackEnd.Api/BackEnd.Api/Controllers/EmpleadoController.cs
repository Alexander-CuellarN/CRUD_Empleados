using AutoMapper;
using BackEnd.Api.DTOs;
using BackEnd.Api.Models;
using BackEnd.Api.Services.Contrato;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoService _empleadoService;
        private readonly IMapper _mapper;

        public EmpleadoController(IEmpleadoService empleadoService, IMapper mapper)
        {
            _empleadoService = empleadoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                var listEmploy = await _empleadoService.GetList();
                var listEmployDto = _mapper.Map<List<EmpleadoDto>>(listEmploy);

                if (listEmployDto == null || listEmploy.Count() <= 0)
                {
                    return NotFound();
                }

                return Ok(listEmployDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var employ = await _empleadoService.Get(id);

                if(employ == null)
                    return NotFound();

                var employDto = _mapper.Map<EmpleadoDto>(employ);
                return Ok(employDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmpleadoDto empleadoDto)
        {
            try
            {
                var employToCreate = _mapper.Map<Empleado>(empleadoDto);
                employToCreate.FechaCreacion = DateTime.Now;
                var result = await _empleadoService.Add(employToCreate);
                var employDto = _mapper.Map<EmpleadoDto>(result);

                return CreatedAtAction("get", new { id = employDto.IdEmpleado }, employDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //[HttpPut("{id:int}")]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] EmpleadoDto empleadoDto)
        {
            try
            {
                var employ = _mapper.Map<Empleado>(empleadoDto);

                if (id != employ.IdEmpleado)
                    return BadRequest();

                await _empleadoService.Update(employ);
                var employDto = _mapper.Map<EmpleadoDto>(await _empleadoService.Get(id));
                return CreatedAtAction("Get", new { id = employ.IdEmpleado }, employDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var employToDelete = await _empleadoService.Get(id);

                if(employToDelete == null) 
                    return BadRequest();

                await _empleadoService.Delete(employToDelete);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
