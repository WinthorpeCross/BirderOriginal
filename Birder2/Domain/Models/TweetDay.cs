using System;
using System.ComponentModel.DataAnnotations;

namespace Birder2.Models
{
    public class TweetDay
    {
        [Key]
        public int TweetDayId { get; set; }

        public DateTime DisplayDay { get; set; }

        [Url]
        public string TweetUrl { get; set; }

        [Url]
        public string ImageUrl { get; set; }

        [Url]
        public string InformationUrl { get; set; }


        public int BirdId { get; set; }

        public Bird Bird { get; set; }
    }
}
