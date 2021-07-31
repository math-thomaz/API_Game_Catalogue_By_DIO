using API_Game_Catalogue.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Game_Catalogue.Repositories
{
    public class GameRepository : IGameRepository
    {
        private static Dictionary<Guid, Game> games = new Dictionary<Guid, Game>()
        {
            {Guid.Parse("2da754f7-2a90-410d-a671-1ced69ac12b6"), new Game{ Id = Guid.Parse("2da754f7-2a90-410d-a671-1ced69ac12b6"), Name = "Romance Of The Three Kingdoms", Producer = "Koei Tecmo", Price = 60} },
            {Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), new Game{ Id = Guid.Parse("eb909ced-1862-4789-8641-1bba36c23db3"), Name = "Monster Hunter Rise", Producer = "Capcom", Price = 60} },
            {Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), new Game{ Id = Guid.Parse("5e99c84a-108b-4dfa-ab7e-d8c55957a7ec"), Name = "Xenoblade Chronicles 3D", Producer = "Monolith", Price = 45} },
            {Guid.Parse("da033439-f352-4539-879f-515759312d53"), new Game{ Id = Guid.Parse("da033439-f352-4539-879f-515759312d53"), Name = "Bravely Default II", Producer = "Square Enix", Price = 60} },
            {Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), new Game{ Id = Guid.Parse("92576bd2-388e-4f5d-96c1-8bfda6c5a268"), Name = "The Legend Of Zelda Skyward Sword HD", Producer = "Nintendo", Price = 60} },
            {Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), new Game{ Id = Guid.Parse("c3c9b5da-6a45-4de1-b28b-491cbf83b589"), Name = "Fire Emblem Warriors", Producer = "Koei Tecmo", Price = 60} }
        };

        public Task<List<Game>> ListAllGames(int page, int quantity)
        {
            return Task.FromResult(games.Values.Skip((page - 1) * quantity).Take(quantity).ToList());
        }

        public Task<Game> ListGameById(Guid id)
        {
            if (!games.ContainsKey(id))
            {
                return null;
            }

            return Task.FromResult(games[id]);
        }

        public Task<List<Game>> ListGameByProducer(string name, string producer)
        {
            return Task.FromResult(games.Values.Where(game => game.Name.Equals(name) && game.Producer.Equals(producer)).ToList());
        }

        public Task AddGame(Game game)
        {
            games.Add(game.Id, game);
            return Task.CompletedTask;
        }

        public Task UpdateGame(Game game)
        {
            games[game.Id] = game;
            return Task.CompletedTask;
        }

        public Task DeleteGame(Guid id)
        {
            games.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            //Closes connection with database
        }

    }
}
