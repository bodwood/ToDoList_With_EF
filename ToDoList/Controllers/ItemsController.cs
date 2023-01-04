using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Models;
using System.Collections.Generic;
using System.Linq;  //translates dataset into C# types

namespace ToDoList.Controllers
{
  public class ItemsController : Controller
  {
    private readonly ToDoListContext _db; //database connection

    public ItemsController(ToDoListContext db)
    {
      _db = db; //set _db to db
    }

    public ActionResult Index()
    {
      List<Item> model = _db.Items  //checks db for object named items
                            .Include(item => item.Category)
                            .ToList(); //Linq turns dbset into a list that's used to create the model for the Index view
      return View(model);
    }

    public ActionResult Create()
    {
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
      return View();
    }

    [HttpPost]
    public ActionResult Create(Item item)
    {
      if (!ModelState.IsValid)
      {
        ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
        return View(item);
      }
      else{
      _db.Items.Add(item);  //adds item to Items table within db
      _db.SaveChanges();
      return RedirectToAction("Index");
      }
    }

    public ActionResult Details(int id) //id passed in from Index.cshtml ("Details", new { id = item.ItemId })
    {
      Item thisItem = _db.Items
                          .Include(item => item.Category)
                          .Include(item => item.JoinEntities) //grabs the join entities
                          .ThenInclude(join => join.Tag)  //loads the Tag object for each join entity
                          .FirstOrDefault(item => item.ItemId == id); /* find any items where the ItemId of an item 
                                                                         is equal to the id we passed into this method */
      return View(thisItem);
    }

    public ActionResult Edit(int id)
    {
      Item thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);  //finds specific item in items table
      ViewBag.CategoryId = new SelectList(_db.Categories, "CategoryId", "Name");
      return View(thisItem);  //passes item to view
    }

    [HttpPost]
    public ActionResult Edit(Item item)
    {
      _db.Items.Update(item);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)  //takes id from form
    {
      Item thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);  //grabs item with same id as parameter
      return View(thisItem);  //returns the item
    }

    [HttpPost, ActionName("Delete")]  //allows us to utilize the proper Delte action even though we named our method DeleteConfirmed
    public ActionResult DeleteConfirmed(int id)
    {
      Item thisItem = _db.Items.FirstOrDefault(item => item.ItemId == id);
      _db.Items.Remove(thisItem);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddTag(int id)
    {
      Item thisItem = _db.Items.FirstOrDefault(items => items.ItemId == id);
      ViewBag.TagId = new SelectList(_db.Tags, "TagId", "Title");
      return View(thisItem);
    }

    [HttpPost]
    public ActionResult AddTag(Item item, int tagId)
    {
      #nullable enable
      ItemTag? joinEntity = _db.ItemTags.FirstOrDefault(join => (join.TagId == tagId && join.ItemId == item.ItemId));
      #nullable disable
      if (joinEntity == null && tagId != 0)
      {
        _db.ItemTags.Add(new ItemTag() { TagId = tagId, ItemId = item.ItemId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = item.ItemId });
    }  

    [HttpPost]
    public ActionResult DeleteJoin(int joinId)
    {
      ItemTag joinEntry = _db.ItemTags.FirstOrDefault(entry => entry.ItemTagId == joinId);
      _db.ItemTags.Remove(joinEntry); //removes join from ItemTags db
      _db.SaveChanges();  //saves changes
      return RedirectToAction("Index");
    }

  }
}