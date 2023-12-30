using BackEnd.Api.Models;
using BackEnd.Api.Services.Contrato;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Api.Services.Implementacion
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly DbempleadoContext _dbContext;
        public EmpleadoService(DbempleadoContext dbContenxt)
        {
            _dbContext = dbContenxt;
        }

        public async Task<List<Empleado>> GetList()
        {
            try
            {
                return await _dbContext.Empleados.Include(dpt => dpt.IdDepartamentoNavigation).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Empleado> Get(int id)
        {
            try
            {
                Empleado? empleado = new Empleado();

                empleado = await _dbContext.Empleados
                                 .Include(departamento => departamento.IdDepartamentoNavigation)
                                 .Where(empleado => empleado.IdEmpleado == id)
                                 .FirstOrDefaultAsync();

                return empleado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Empleado> Add(Empleado empleado)
        {
            try
            {
                _dbContext.Empleados.Add(empleado);
                await _dbContext.SaveChangesAsync();
                return empleado;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Update(Empleado empleado)
        {
            try
            {
                _dbContext.Empleados.Update(empleado);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> Delete(Empleado empleado)
        {
            try
            {
                _dbContext.Empleados.Remove(empleado);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
