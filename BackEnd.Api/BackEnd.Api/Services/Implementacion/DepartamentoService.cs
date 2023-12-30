using BackEnd.Api.Models;
using BackEnd.Api.Services.Contrato;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Api.Services.Implementacion
{
    public class DepartamentoService : IDepartamentoService
    {
        private readonly DbempleadoContext _dbContext;

        public DepartamentoService(DbempleadoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Departamento>> GetList()
        {
            try
            {
                return await _dbContext.Departamentos.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
