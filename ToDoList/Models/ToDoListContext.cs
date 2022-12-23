using Microsoft.EntityFrameworkCore;

namespace ToDoList.Models
{
  public class ToDoListContext : DbContext //DbContext extends entity framework core
  {
    public DbSet<Item> Items { get; set; }  //represents items table in database. Entity named Items (with 's')
    public ToDoListContext(DbContextOptions options) : base(options) { }  //base represents the parent DBContext class
                                                                          //options will be passed through dependency injection from program.cs
  }
}