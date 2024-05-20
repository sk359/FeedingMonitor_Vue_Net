using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
  public class Product
  {
    public int id { get; set; }
    
    [Required]
    public string name { get; set; }

    [Required]
    public string food_type { get; set; }

    public DateTime? created { get; set; }

    [Column("stamm_brand_id")]
    public int? BrandId { get; set; }  // relationship discovery
    public Brand? Brand { get; set; }

    public override string ToString()
    {
      return String.Format("{0} - {1}", name, food_type);
    }

  }

}