using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System.Collections.Generic;
using System.Linq;

namespace ToDoList.Controllers
{
  public class ItemsController : Controller
  {

   private readonly ToDoListContext _db;  //holds database connection as a ToDoListContext type

   public ItemsController(ToDoListContext db) //ToDoListContext db is we used use as a service in program.cs
   {
    _db = db; //set value of _db to value of ToDoListContext db.
   }

   public ActionResult Index()
   {
    List<Item> model = _db.Items.ToList();  //translates the dataset into C# types that we can use in the view
    return View(model); //we return our model list to the Index view
   }
  }
}