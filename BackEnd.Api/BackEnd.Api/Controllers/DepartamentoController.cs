using AutoMapper;
using BackEnd.Api.DTOs;
using BackEnd.Api.Models;
using BackEnd.Api.Services.Contrato;
using BackEnd.Api.Services.Implementacion;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {   
        private readonly IDepartamentoService _departamentoService;
        private readonly IMapper _mapper;
        public DepartamentoController( IDepartamentoService departamentoService, IMapper mapper) {
            _departamentoService = departamentoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Lista()
        {
            var listaDEpartamento = await _departamentoService.GetList();
            var listaDepartamentoDto = _mapper.Map<IEnumerable<DepartamentoDto>>(listaDEpartamento);

            if (listaDepartamentoDto.Count() <= 0)
                return NotFound();

            return Ok(listaDepartamentoDto);
        }
    }
}
