using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NewsItems.Model;

namespace NewsItems.Data
{
    public class NewsItemsContext(DbContextOptions<NewsItemsContext> options) : DbContext(options)
    {
        public DbSet<NewsItems.Model.NewsItem> NewsItem { get; set; } = default!;
    }
}
