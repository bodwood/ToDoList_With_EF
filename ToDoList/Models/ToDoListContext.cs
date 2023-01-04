using Microsoft.EntityFrameworkCore;

namespace ToDoList.Models
{
  public class ToDoListContext : DbContext  //inherits DbContext to ensure all default built-in DbContext functionality
  {
    public DbSet<Category> Categories { get; set; } //represents the categories table
    public DbSet<Item> Items { get; set; }  //represents the items table
    public DbSet<Tag> Tags { get; set; }  //added for Tag class (represents the tags table)
    public DbSet<ItemTag> ItemTags { get; set; }  //added for ItemTag class (ef core creates table for this in database) (represents the itemstags table)

    public ToDoListContext(DbContextOptions options) : base(options) { }  /*constructor inherits from ToDoListContext class
                                                                            base represents the DbContext class
                                                                            */
  }
}