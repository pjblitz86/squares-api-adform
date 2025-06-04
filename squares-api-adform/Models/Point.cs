﻿using System.ComponentModel.DataAnnotations;

namespace squares_api_adform.Models

{
    public class Point
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int X { get; set; }

        [Required]
        public int Y { get; set; }
    }
}
