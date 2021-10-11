using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MatchManagerApi.Entities
{
    /// <summary>
    /// Enum for Sports. Each match can be either Football or Basketball
    /// </summary>
    public enum Sport 
    {
        Football = 1,
        Basketball = 2
    }

    public class Match
    {
        private DateTime? _matchDateTime;
        private DateTime? _matchDate;
        private TimeSpan? _matchTime;
        private string _description;
        private string _teamA;
        private string _teamB;
        /// <summary>
        /// Primary key of Match entity.
        /// We do not need to specify it with an annotation as EF core by default checks for property named ID 
        /// and declares it as primary key 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Description of Match
        /// </summary>
        public string Description 
        { 
            get { return _description; } 
            set 
            { 
                if (string.IsNullOrWhiteSpace(value))
                    _description = string.Concat(_teamA,"-",_teamB);
                else 
                    _description = value;
            } 
        }
        /// <summary>
        /// Date of Match. Since we need to store only the Date in the database, we take only the date part. 
        /// </summary>
        [Required]
        public DateTime? MatchDate 
        { 
            get { return _matchDate; } 
            set 
            { 
                if (value.HasValue) 
                {
                    _matchDateTime = value.Value;
                    _matchDate = _matchDateTime.Value.Date;

                    if (!MatchTime.HasValue) 
                        MatchTime = _matchDateTime.Value.TimeOfDay;
                }
            } 
        }
        /// <summary>
        /// Time of Match. 
        /// Since JSON deserialization is not supported for TimeSpan value type, 
        /// the value is taken from Match Date.
        /// </summary>
        public TimeSpan? MatchTime 
        {   
            get { return _matchTime; } 
            set 
            {
                if (value == null) 
                    _matchTime = _matchDateTime.Value.TimeOfDay; 
                else 
                    _matchTime = value;
            }
        }
        /// <summary>
        /// TeamA of Match
        /// </summary>
        [Required]
        public string TeamA 
        { 
            get { return _teamA; } 
            set 
            { 
                _teamA = value; 

                if (string.IsNullOrWhiteSpace(_description) && !string.IsNullOrWhiteSpace(_teamB)) 
                {
                    Description = "";
                }
            } 
        }
        /// <summary>
        /// TeamB of Match
        /// </summary>
        [Required]
        public string TeamB  
        { 
            get { return _teamB; } 
            set 
            { 
                _teamB = value; 

                if (string.IsNullOrWhiteSpace(_description) && !string.IsNullOrWhiteSpace(_teamA)) 
                {
                    Description = "";
                }
            } 
        }
        /// <summary>
        /// Sport. Can be either Football or match. 
        /// </summary>
        public Sport Sport { get; set; }

        public ICollection<MatchOdds> MatchOdds { get; set; }
    }
}