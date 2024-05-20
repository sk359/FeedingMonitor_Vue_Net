using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Models
{
  public class Feeding
  {
    public int id { get; set; }
    
    [Required]
    public string catname { get; set; }
    
    [Required]
    public string brandname { get; set; }
    
    [Required]
    public string productname { get; set; }    
    
    [Required]
    public string taste { get; set; }    
    
    [Required]
    public string type { get; set; }    
    
    [Required]
    public int eatenpercentage { get; set; }
    public DateTime? created { get; set; }
    
    [Required]
    public DateTime? feedingtime { get; set; }

    // Ab hier z.B. relationships (navigation properties) im Constructor initialisieren
    
  }
}