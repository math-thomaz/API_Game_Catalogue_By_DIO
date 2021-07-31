using System;

namespace API_Game_Catalogue.Exceptions
{
    public class GameAlreadyRegisteredException : Exception
    {
        public GameAlreadyRegisteredException()
            : base("Game already registered.")
        { }
    }
}
