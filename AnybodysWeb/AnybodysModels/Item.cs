﻿using System.ComponentModel.DataAnnotations;

namespace AnybodysModels
{
    public class Item
    {
        public int Id { get; set; }
        [StringLength(250)]
        public string Name { get; set; }
        public byte[]? ImageData { get; set; }
        [StringLength(2048)]
        public string ImagePath { get; set; }
        public virtual int? CategoryId { get; set; }
        public virtual Category? Category { get; set; }
    }
}
