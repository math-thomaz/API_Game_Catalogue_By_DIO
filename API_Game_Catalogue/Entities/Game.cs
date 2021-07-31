using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Game_Catalogue.Entities
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Producer { get; set; }
        public double Price { get; set; }
    }
}
