using Microsoft.EntityFrameworkCore;

namespace ToDoList.Models
{
  public class ToDoListContext : DbContext
  {
    public DbSet<Category> Categories { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Tag> Tags { get; set; }  //added for Tag class
    public DbSet<ItemTag> ItemTags { get; set; }  //added for ItemTag class (ef core creates table for this in database)

    public ToDoListContext(DbContextOptions options) : base(options) { }
  }
}