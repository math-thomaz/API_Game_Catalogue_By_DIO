using API_Game_Catalogue.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace API_Game_Catalogue.Repositories
{
    public class GameSqlServerRepository : IGameRepository
    {
        private readonly SqlConnection sqlConnection;

        public GameSqlServerRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public async Task<List<Game>> ListAllGames(int page, int quantity)
        {
            var games = new List<Game>();

            var command = $"select * from Games order by id offset {((page - 1) * quantity)} rows fetch next {quantity} rows only";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                games.Add(new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Name"],
                    Producer = (string)sqlDataReader["Producer"],
                    Price = (double)sqlDataReader["Price"]
                });
            }

            await sqlConnection.CloseAsync();

            return games;
        }

        public async Task<Game> ListGameById(Guid id)
        {
            Game game = null;

            var command = $"select * from Games where Id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                game = new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Name"],
                    Producer = (string)sqlDataReader["Producer"],
                    Price = (double)sqlDataReader["Price"]
                };
            }

            await sqlConnection.CloseAsync();

            return game;
        }

        public async Task<List<Game>> ListGameByProducer(string name, string producer)
        {
            var games = new List<Game>();

            var command = $"select * from Games where Name = '{name}' and Producer = '{producer}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                games.Add(new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Name"],
                    Producer = (string)sqlDataReader["Producer"],
                    Price = (double)sqlDataReader["Price"]
                });
            }

            await sqlConnection.CloseAsync();

            return games;
        }

        public async Task AddGame(Game game)
        {
            var command = $"insert Games (Id, Name, Producer, Price) values ('{game.Id}', '{game.Name}', '{game.Producer}', {game.Price.ToString().Replace(",", ".")})";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task UpdateGame(Game game)
        {
            var command = $"update Games set Name = '{game.Name}', Producer = '{game.Producer}', Price = {game.Price.ToString()}";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task DeleteGame(Guid id)
        {
            var command = $"delete from Games where id = '{id}'";

            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            await sqlCommand.ExecuteNonQueryAsync();
            await sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }

    }
}
