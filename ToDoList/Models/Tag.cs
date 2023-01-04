using System.Collections.Generic;

namespace ToDoList.Models
{
  public class Tag
  {
    public int TagId { get; set; }
    public string Title { get; set; }
    public List<ItemTag> JoinEntities { get; } //lets ef core recognize when there is a relationship between two entities (many to many)
  }
}