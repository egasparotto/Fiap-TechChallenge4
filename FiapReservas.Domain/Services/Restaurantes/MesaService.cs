using FiapReservas.Domain.Entities.Restaurantes;
using FiapReservas.Domain.Interfaces.Repositories.Restaurantes;
using FiapReservas.Domain.Interfaces.Services.Restaurantes;
using FiapReservas.Domain.Services.Base;

namespace FiapReservas.Domain.Services.Restaurantes
{
    public class MesaService : BaseService<Mesa, IMesaRepository>, IMesaService
    {
        public MesaService(IMesaRepository repository) : base(repository)
        {
        }
    }
}
