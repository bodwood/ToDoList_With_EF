using System.Collections.Generic;

namespace ToDoList.Models
{
  public class Category
  {
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public List<Item> Items { get; set; } //lets ef core recognize when there is a relationship between two entities (many part of one to many)
  }
}