using BackEnd.Api.Models;

namespace BackEnd.Api.Services.Contrato
{
    public interface IEmpleadoService
    {
        Task<List<Empleado>> GetList();
        Task<Empleado> Get(int id);
        Task<Empleado> Add(Empleado empleado);
        Task<bool> Update(Empleado empleado);
        Task<bool> Delete(Empleado empleado);
    }
}
