using FiapReservas.Domain.Entities.Reservas;
using FiapReservas.Domain.Entities.Restaurantes;
using FiapReservas.Domain.Interfaces.Repositories.Reservas;
using FiapReservas.Domain.Interfaces.Repositories.Restaurantes;
using FiapReservas.Domain.Interfaces.Services.Reservas;
using FiapReservas.Domain.Interfaces.Services.Restaurantes;
using FiapReservas.Domain.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapReservas.Domain.Services.Reservas
{

    public class ReservaService : BaseService<Reserva, IReservaRepository>, IReservaService
    {
        IReservaRepository _repository;
        public ReservaService(IReservaRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<Reserva> Reservar(Reserva reserva)
        {
            await _repository.Insert(reserva);
            return reserva;
        }
    }
}
