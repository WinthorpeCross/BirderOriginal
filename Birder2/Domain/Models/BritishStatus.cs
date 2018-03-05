using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Birder2.Models
{
    public class BritishStatus
    {
        [Key]
        public int BritishStatusId { get; set; }

        [Required]
        [Display(Name = "Status in Britain")]
        public string BirderStatusInBritain { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastUpdateDate { get; set; }

        public ICollection<Bird> Birds { get; set; }
    }
}
