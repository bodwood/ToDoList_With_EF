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

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Tag tag)
    {
      _db.Tags.Add(tag);  //adds tag object to database of table Tags
      _db.SaveChanges();  //saves database changes 
      return RedirectToAction("Index"); //redirects to index after action is performed
    }

    public ActionResult Details(int id)
    {
      Tag thisTag = _db.Tags
                      .Include(tag => tag.JoinEntities) //loads JoinEntities property of each Tag
                      .ThenInclude(join => join.Item) //loads the Iteam object associated with each ItemTag
                      .FirstOrDefault(tag => tag.TagId == id);  //find any tag where the TagId of a client is equal to id
      return View(thisTag);
    }

    public ActionResult AddItem(int id)
    {
      Tag thisTag = _db.Tags.FirstOrDefault(tags => tags.TagId == id);  //find any Tag where the TagId of a tag is equal to id
      ViewBag.ItemId = new SelectList(_db.Items, "ItemId", "Description");  //
      return View(thisTag); //pass each item that's assocated with a tag into the view
    }

    [HttpPost]
    public ActionResult AddItem(Tag tag, int itemId)
    {
      #nullable enable
      /*
      Because joinEntity variable below can be an ItemTag object or null, we must make it a nullable type.
      This is done by using ? at the end of the type "ItemTag?"
      */
      ItemTag? joinEntity = _db.ItemTags.FirstOrDefault(join => (join.ItemId == itemId && join.TagId == tag.TagId)); //checks for duplicat join relationships
      /*
      Creates a database query with the FirstOrDefault() method that returns the first ItemTag object that contains
      a matching ItemId and TagId; if a matching ItemTag object can't be found, the default is returned, which is null.
      */
      #nullable disable
      if (joinEntity == null && itemId != 0)  //prevents the creation of a join relationship when there's no item in the select list
      {
        _db.ItemTags.Add(new ItemTag() { ItemId = itemId, TagId = tag.TagId });
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = tag.TagId });
    }

    public ActionResult Edit(int id) //pass specific tag we want to edit into edit method
    {
      Tag thisTag = _db.Tags.FirstOrDefault(tags => tags.TagId == id); //grabs the first tag in the tags db that has the same id
      return View(thisTag);
    }

    [HttpPost]
    public ActionResult Edit(Tag tag) 
    {
      _db.Tags.Add(tag);  //adds tag to database
      _db.SaveChanges();  //saves db changes
      return RedirectToAction("Index"); //returns index view after edit
    }

    public ActionResult Delete(int id)
    {
      Tag thisTag = _db.Tags.FirstOrDefault(tags => tags.TagId == id);
      return View(thisTag);
    }

    [HttpPost, ActionName("Delete")]
      public ActionResult DeleteConfirmed(int id)
      {
        Tag thisTag = _db.Tags.FirstOrDefault(tags => tags.TagId == id);
        _db.Tags.Remove(thisTag); //removes found tag from db
        _db.SaveChanges();  //saves db changes
        return RedirectToAction("Index");
      }
    
    [HttpPost]
    public ActionResult DeleteJoin(int joinId) //joinId is grabbed from Details.cshtml
    {
      ItemTag joinEntry = _db.ItemTags.FirstOrDefault(entry => entry.ItemTagId == joinId);
      _db.ItemTags.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }


  }
}