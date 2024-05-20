using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using backend.Data;
using backend.Models;

namespace backend.Data
{

  public interface IFeedingRepository
{
    public void Commit(); 
    IEnumerable<Feeding> GetAllFeedingsForCat(string catname);
    public Feeding? GetFeedingById(int fid);
    IEnumerable<Feeding> GetAll();
    public Feeding AddNewFeeding(Feeding feed); 
    public void DeleteFeedingById(Feeding feeding);
    public IEnumerable<Brand> GetAllBrands();
    public IEnumerable<Brand> GetAllBrandsWithProducts();
    public Brand AddNewBrand(Brand brand); 
}

public class FeedingRepository : IFeedingRepository
{
    private readonly FeedingContext _context;

    public FeedingRepository(FeedingContext context)
    {
      Console.WriteLine("Initializing Repo");
        _context = context;        
    }

    public void Commit() 
    {
      _context.SaveChanges();
    }

    public IEnumerable<Feeding> GetAll()
    {
      var feedings = _context.Feedings.ToList();
      return feedings;
    }


    public IEnumerable<Feeding> GetAllFeedingsForCat(string catname)
    {
        var feedings = _context.Feedings//.Include(p => p.Category)
                                   .Where(p => p.catname == catname)
                                   .OrderByDescending( p => p.id)
                                   .ToList();
        return feedings;
    }

    public Feeding? GetFeedingById(int fid)
    {
        var feeding = _context.Feedings.Find(fid);                                   
        return feeding;
    }

    public Feeding AddNewFeeding(Feeding feed) 
    {
      feed.created = DateTime.Now;
      _context.Add(feed);
      return feed;
    }

    public void DeleteFeedingById(Feeding feeding)
    {
      _context.Feedings.Remove(feeding);
    }

    public IEnumerable<Brand> GetAllBrands()
    {
      return _context.Brands.OrderBy( b => b.name ).ToList();
    }

    public IEnumerable<Brand> GetAllBrandsWithProducts()
    {
      return _context.Brands.OrderBy( b => b.name ).Include(b => b.Products).ToList();
    }

    public IEnumerable<Product> GetAllProducts()
    {
      return _context.Products.ToList();
    }

    public Brand AddNewBrand(Brand brand) 
    {
      brand.created = DateTime.Now;
      _context.Add(brand);
      return brand;
    }

}

}