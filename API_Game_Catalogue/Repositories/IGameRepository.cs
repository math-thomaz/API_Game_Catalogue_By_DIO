using API_Game_Catalogue.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_Game_Catalogue.Repositories
{
    public interface IGameRepository : IDisposable
    {
        Task<List<Game>> ListAllGames(int page, int quantity);
        Task<Game> ListGameById(Guid id);
        Task<List<Game>> ListGameByProducer(string name, string producer);
        Task AddGame(Game game);
        Task UpdateGame(Game game);
        Task DeleteGame(Guid id);
    }
}
