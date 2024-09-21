using EFCoreQuerying.Context;
using Microsoft.EntityFrameworkCore;

namespace EFCoreQuerying.Queries;

public class Queries
{
   public async Task<IList<Quesiton1>> GetTitlesWithRatingAndPublishAfter(double rating, int publishedYear, MyDbContext myDbContext)
   {
       return await myDbContext.Books
           .Where(i => i.PublishedYear > publishedYear)
           .Include(books => books.Reviews.Where(r => r.Rating > rating))
           .Select(i=> new Quesiton1
           {
               AuthorName= i.Author.Name,
               BookName = i.Title,
               Rating = i.Reviews.Average(r=> r.Rating),
               TotalSales = i.Sales.Count
               
           })
           .ToListAsync();
   }
}

public class Quesiton1
{
    public string AuthorName { get; set; }
    public string BookName { get; set; }
    public double? Rating { get; set; }
    public int TotalSales { get; set; }
    
}
