using System;

namespace API_Game_Catalogue.Exceptions
{
    public class GameNotRegisteredException : Exception
    {
        public GameNotRegisteredException()
            : base("Game not found or registered.")
        { }
    }
}
