using System;
using System.Collections.Generic;

namespace EFCoreQuerying.EF;

public partial class Book
{
    public int BookId { get; set; }

    public string? Title { get; set; }

    public string? Genre { get; set; }

    public int? AuthorId { get; set; }

    public int? PublishedYear { get; set; }

    public virtual Author? Author { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();
}
