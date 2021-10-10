using System;
using System.Collections.Generic;

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
        /// <summary>
        /// Date and time of the match. 
        /// In case the user passes the date time of the match as a whole, 
        /// we store the value in this property and 
        /// </summary>
        private DateTime _matchDateTime;
        /// <summary>
        /// Contains the date of the match and it is the final value that MatchDate property receives.
        /// </summary>
        private DateTime _matchDate;
        /// <summary>
        /// Contains the time of the match and it is the final value that MatchTime property receives.
        /// </summary>
        private TimeSpan? _matchTime;
        /// <summary>
        /// Primary key of Match entity.
        /// We do not need to specify it with an annotation as EF core by default checks for property named ID 
        /// and declares it as primary key 
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// Description of Match
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Date of Match. Since we need to store only the Date in the database, we take only the date part. 
        /// </summary>
        public DateTime MatchDate 
        { 
            get { return _matchDate; } 
            set 
            { 
                _matchDateTime = value;
                _matchDate = _matchDateTime.Date;
            } 
        }
        /// <summary>
        /// Time of Match. In case it is not provided, we try to fetch it from the date time property _matchDateTime
        /// </summary>
        public TimeSpan? MatchTime 
        {   
            get { return _matchTime; } 
            set 
            {
                if (value == null) 
                    _matchTime = _matchDateTime.TimeOfDay; 
                else 
                    _matchTime = value;
            }
        }
        /// <summary>
        /// TeamA of Match
        /// </summary>
        public string TeamA { get; set; }
        /// <summary>
        /// TeamB of Match
        /// </summary>
        public string TeamB { get; set; }
        /// <summary>
        /// Sport. Can be either Football or match. 
        /// </summary>
        public Sport Sport { get; set; }

        public ICollection<MatchOdds> MatchOdds { get; set; }
    }
}