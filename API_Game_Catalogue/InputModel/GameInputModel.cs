using System;
using System.ComponentModel.DataAnnotations;

namespace API_Game_Catalogue.InputModel
{
    public class GameInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "The name must have between 3 and 100 characters.")]
        public string Name { get; set; }

        [Required]
        [StringLength(100, MinimumLength =1, ErrorMessage = "The name must have between 1 and 100 characters.")]
        public string Producer { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "The price must have 1 dolar minimum and 1000 dolar maximum.")]
        public double Price { get; set; }
    }
}
