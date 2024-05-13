using FiapReservas.Domain.Entities.Reservas;
using FiapReservas.Domain.Entities.Restaurantes;
using FiapReservas.Domain.Interfaces.Repositories.Reservas;
using FiapReservas.Domain.Interfaces.Repositories.Restaurantes;
using FiapReservas.Domain.Interfaces.Services.Reservas;
using FiapReservas.Domain.Interfaces.Services.Restaurantes;
using FiapReservas.Domain.Interfaces.Services.SMS;
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
        private readonly ISmsService _smsService;
        public ReservaService(IReservaRepository repository, ISmsService smsService) : base(repository)
        {
            _smsService = smsService;
        }

        public async Task<Reserva> Reservar(Reserva reserva)
        {
            await Insert(reserva);
            var telefone = $"+55{reserva.Telefone}";
            _smsService.EnviarMensagem(telefone, $"Recebemos a sua reserva em {reserva.Restaurante.Nome} e assim que ela for confirmada, vamos te avisar por aqui. Você também pode consultar o status em http://localhost:8080/reserva/{reserva.Id}");
            EnviaSMSRestaurante(reserva);
            
            return reserva;
        }

        private void EnviaSMSRestaurante(Reserva reserva)
        {
            var telefone = $"+55{reserva.Restaurante.Telefone}";
            _smsService.EnviarMensagem(telefone, $"Você recebeu a segunte solicitação de reserva:\nData: {reserva.DataReserva.AddHours(-3):dd/MM/yyyy HH:mm}\nNome: {reserva.Nome}\nTelefone: {long.Parse(reserva.Telefone):(00) 00000-0000}\nEmail: {reserva.Email}\nQuantiade de Pessoas: {reserva.QuantidadePessoas}\nConfirme em http://localhost:8080/confirmar/{reserva.Id}");
        }

        public async Task Confirmar(Reserva reserva)
        {
            reserva.Status = Enums.StatusReserva.Confirmada;
            var telefone = $"+55{reserva.Telefone}";
            _smsService.EnviarMensagem(telefone, $"Que boa notícia, sua reserva em {reserva.Restaurante.Nome} foi confirmada, estamos te aguardadndo.");
            await Update(reserva);
        }
    }
}
