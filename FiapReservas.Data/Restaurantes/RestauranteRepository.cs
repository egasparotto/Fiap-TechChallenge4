using FiapReservas.Data.Base;
using FiapReservas.Domain.Entities.Restaurantes;
using FiapReservas.Domain.Interfaces.Repositories.Restaurantes;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace FiapReservas.Data.Restaurantes
{
    public class RestauranteRepository : BaseRepository<Restaurante>, IRestauranteRepository
    {
        protected override string CollectionName => "Restaurante";

        
    }
}
