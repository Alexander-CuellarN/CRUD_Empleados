using BackEnd.Api.Models;

namespace BackEnd.Api.Services.Contrato
{
    public interface IDepartamentoService
    {
        Task<List<Departamento>> GetList();
    }
}
