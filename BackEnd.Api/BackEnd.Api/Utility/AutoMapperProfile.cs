using AutoMapper;
using BackEnd.Api.DTOs;
using BackEnd.Api.Models;
using System.Globalization;

namespace BackEnd.Api.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Departamento, DepartamentoDto>().ReverseMap();
            
            CreateMap<Empleado, EmpleadoDto>()
                .ForMember(Destino => Destino.NombreDepartamento,
                           opt => opt.MapFrom(origen => origen.IdDepartamentoNavigation.Nombre))
                .ForMember(destino => destino.FechaContrato,
                            options => options.MapFrom(origen => origen.FechaContrato.Value.ToString("dd/MM/yy")));

            CreateMap<EmpleadoDto, Empleado>().
                ForMember(destino => destino.IdDepartamentoNavigation,
                           options => options.Ignore())
                .ForMember(destino => destino.FechaContrato,
                           option => option.MapFrom(origen => DateTime.ParseExact(origen.FechaContrato, "dd/MM/yy", CultureInfo.InvariantCulture)));

        }
    }
}
