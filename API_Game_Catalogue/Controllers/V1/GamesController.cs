using API_Game_Catalogue.Exceptions;
using API_Game_Catalogue.InputModel;
using API_Game_Catalogue.Services;
using API_Game_Catalogue.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_Game_Catalogue.Controllers.V1
{
    [Route("api/V1/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        /// <summary>
        /// List all games with pagination.
        /// </summary>
        /// <remarks>
        /// Its not possible to return a game without pagination parameter.
        /// </remarks>
        /// <param name="page">Indicates which page are being displayed. 1 page minimum.</param>
        /// <param name="quantity">Quantity of items to be listed. 1 item minimum and 50 maximum.</param>
        /// <response code="200">Returns a list of items.</response>
        /// <response code="204">There is no game to be listed.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> ListAllGames([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int quantity = 5)
        {
            var games = await _gameService.ListAllGames(page, quantity);

            if (games.Count() == 0)
                return NoContent();

            return Ok(games);
        }

        /// <summary>
        /// It will list a game by its Id code.
        /// </summary>
        /// <param name="id">Game's Id code</param>
        /// <response code="200">Returns a game that matches with the Id entered by user.</response>
        /// <response code="204">There is no game with such Id.</response>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GameViewModel>> ListGameById([FromRoute] Guid id)
        {
            var game = await _gameService.ListGameById(id);

            if (game == null)
            {
                return NoContent();
            }
            return Ok(game);
        }

        /// <summary>
        /// Adds a game to the catalogue.
        /// </summary>
        /// <param name="gameInputModel">Game's data to be entered.</param>
        /// <response code="200">Indicates success when all info are correct.</response>
        /// <response code="422">Indicates error when the game are already registered.</response>
        [HttpPost]
        public async Task<ActionResult<GameViewModel>> AddGame([FromBody] GameInputModel gameInputModel)
        {
            try
            {
                var game = await _gameService.AddGame(gameInputModel);

                return Ok(game);
            }
            catch (GameAlreadyRegisteredException ex)
            {
                return UnprocessableEntity("This game is already registered.");
            }
            return Ok();
        }

        /// <summary>
        /// Update a game in the catalogue.
        /// </summary>
        /// <param name="id">Game Id to be updated.</param>
        /// <param name="gameInputModel">The game info to be updated.</param>
        /// <response code="200">Game info updated with success.</response>
        /// <response code="404">There is no game with such Id.</response>
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid id, [FromBody] GameInputModel gameInputModel)
        {
            try
            {
                await _gameService.UpdateGame(id, gameInputModel);

                return Ok();
            }
            catch (GameNotRegisteredException ex)
            {
                return NotFound("Game not found or registered.");
            }
        }

        /// <summary>
        /// Update the price of game.
        /// </summary>
        /// <param name="id">Game Id to be updated.</param>
        /// <param name="price">The new price of it game.</param>
        /// <response code="200">Game's price updated with success.</response>
        /// <response code="404">There is no game with such Id.</response>
        [HttpPatch("{id:guid}/price/{price:double}")]
        public async Task<ActionResult> UpdateGamePrice([FromRoute] Guid id, [FromRoute] double price)
        {
            try
            {
                await _gameService.UpdateGamePrice(id, price);

                return Ok();
            }
            catch (GameNotRegisteredException ex)
            {
                return NotFound("Game not found or registered.");
            }
        }

        /// <summary>
        /// Remove a game from its catalogue.
        /// </summary>
        /// <param name="id">Game Id to be removed.</param>
        /// <response code="200">Game removed with success.</response>
        /// <response code="404">There is no game with such Id.</response>
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteGame([FromRoute] Guid id)
        {
            try
            {
                await _gameService.DeleteGame(id);

                return Ok();
            }
            catch (GameNotRegisteredException ex)
            {
                return NotFound("Game not found or registered.");
            }
        }
    }
}
