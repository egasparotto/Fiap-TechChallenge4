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

        public async Task<Mesa> GetMesaByNumero(Guid restauranteId, int mesaNumero)
        {
            var filter = Builders<Restaurante>.Filter.And(
                Builders<Restaurante>.Filter.Eq(r => r.Id, restauranteId),
                Builders<Restaurante>.Filter.ElemMatch(r => r.Mesas, m => m.Numero == mesaNumero)
            );

            var restaurante = await GetCollection().Find(filter).FirstOrDefaultAsync();

            // Se o restaurante não for encontrado ou não houver mesa com o número especificado, retorne null
            if (restaurante == null)
                return null;

            return restaurante.Mesas.FirstOrDefault(m => m.Numero == mesaNumero);
        }
    }
}
