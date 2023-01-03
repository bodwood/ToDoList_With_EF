using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ToDoList.Controllers
{
  public class TagsController : Controller
  {
    private readonly ToDoListContext _db; //connect to database

    public TagsController(ToDoListContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Tags.ToList());
    }

    public ActionResult Details(int id)
    {
      Tag thisTag = _db.Tags
                      .Include(tag => tag.JoinEntities) //loads JoinEntities property of each Tag
                      .ThenInclude(join => join.Item) //loads the Iteam object associated with each ItemTag
                      .FirstOrDefault(tag => tag.TagId == id);  //find any tag where the TagId of a client is equal to id
      return View(thisTag);
    }
  }
}