using API_Game_Catalogue.InputModel;
using API_Game_Catalogue.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API_Game_Catalogue.Services
{
    public interface IGameService : IDisposable
    {
        Task<List<GameViewModel>> ListAllGames(int page, int quantity);
        Task<GameViewModel> ListGameById(Guid id);
        Task<GameViewModel> AddGame(GameInputModel game);
        Task UpdateGame(Guid id, GameInputModel game);
        Task UpdateGamePrice(Guid id, double price);
        Task DeleteGame(Guid id);
    }
}
