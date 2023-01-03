namespace ToDoList.Models
{
  public class ItemTag
  {
    public int ItemTagId { get; set; }
    public int ItemId { get; set; }
    public Item Item { get; set; }  //navigation property (defines relationpships between classes)
    public int TagId { get; set; }
    public Tag Tag { get; set; }  //nagivation property (defines relationpships between classes)
  }
}