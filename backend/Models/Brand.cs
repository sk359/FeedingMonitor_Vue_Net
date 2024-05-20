using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
  public class Brand
  {
    public int id { get; set; }
    
    [Required]
    public string name { get; set; }

    public DateTime? created { get; set; }

    public ICollection<Product> Products { get; } = new List<Product>();

  }

}