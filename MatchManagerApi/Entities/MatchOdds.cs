using System.ComponentModel.DataAnnotations;

namespace MatchManagerApi.Entities
{
    public class MatchOdds
    {
        /// <summary>
        /// Primary key of Match Odds entity.
        /// We do not need to specify it with an annotation as EF core by default checks for property named ID 
        /// and declares it as primary key 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Foreign key of Match Odds entity. Points to Match entity
        /// The declararion takes place during Data Context. 
        /// </summary>
        public int MatchId { get; set; }
        /// <summary>
        /// Specifier of the Odd. Can be 1, 2 or X
        /// </summary>
        [Required]
        [RegularExpression("1|X|2", ErrorMessage = "Only 1, X or 2 is allowed for Specifier")]
        public string Specifier { get; set; }
        /// <summary>
        /// Odd value.
        /// </summary>
        [Required]
        public float Odd { get; set; }

        public Match Match { get; set; }
    }
}