using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDoList.Models
{
  public class Item
  {
    public int ItemId { get; set; }
    [Required(ErrorMessage = "The item's description can't be empty!")]
    public string Description { get; set; }
    //validation attribute. Must be directly above the property we want to validat
    [Range(1, int.MaxValue, ErrorMessage = "You must add your item to a category. Have you created a category yet?")]  
    public int CategoryId { get; set; } //foreign key for items (associates items with a category)
    public Category Category { get; set; }  
    public List<ItemTag> JoinEntities { get; } //lets ef core recognize when there is a relationship between two entities (many to many)
  }
}