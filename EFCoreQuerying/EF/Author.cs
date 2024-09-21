using System;
using System.Collections.Generic;

namespace EFCoreQuerying.EF;

public partial class Author
{
    public int AuthorId { get; set; }
    public int? Age { get; set; }

    public string? Name { get; set; }

    public int? BirthYear { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
