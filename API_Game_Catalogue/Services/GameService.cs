using API_Game_Catalogue.Entities;
using API_Game_Catalogue.Exceptions;
using API_Game_Catalogue.InputModel;
using API_Game_Catalogue.Repositories;
using API_Game_Catalogue.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Game_Catalogue.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task<List<GameViewModel>> ListAllGames(int page, int quantity)
        {
            var games = await _gameRepository.ListAllGames(page, quantity);

            return games.Select(game => new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price
            }).ToList();
        }

        public async Task<GameViewModel> ListGameById(Guid id)
        {
            var game = await _gameRepository.ListGameById(id);

            if (game == null)
            {
                return null;
            }

            return new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price
            };
        }

        public async Task<GameViewModel> AddGame(GameInputModel game)
        {
            var gameEntity = await _gameRepository.ListGameByProducer(game.Name, game.Producer);

            if (gameEntity.Count > 0)
            {
                throw new GameAlreadyRegisteredException();
            }

            var newGame = new Game
            {
                Id = Guid.NewGuid(),
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price
            };

            await _gameRepository.AddGame(newGame);

            return new GameViewModel
            {
                Id = newGame.Id,
                Name = game.Name,
                Producer = game.Producer,
                Price = game.Price
            };
        }

        public async Task UpdateGame(Guid id, GameInputModel game)
        {
            var gameEntity = await _gameRepository.ListGameById(id);

            if (gameEntity == null)
            {
                throw new GameNotRegisteredException();
            }

            gameEntity.Name = game.Name;
            gameEntity.Producer = game.Producer;
            gameEntity.Price = game.Price;

            await _gameRepository.UpdateGame(gameEntity);
        }

        public async Task UpdateGamePrice(Guid id, double price)
        {
            var gameEntity = await _gameRepository.ListGameById(id);

            if (gameEntity == null )
            {
                throw new GameNotRegisteredException();
            }

            gameEntity.Price = price;

            await _gameRepository.UpdateGame(gameEntity);
        }

        public async Task DeleteGame(Guid id)
        {
            var game = await _gameRepository.ListGameById(id);

            if (game == null)
            {
                throw new GameNotRegisteredException();
            }
            await _gameRepository.DeleteGame(id);
        }

        public void Dispose()
        {
            _gameRepository?.Dispose();
        }
    }
}
