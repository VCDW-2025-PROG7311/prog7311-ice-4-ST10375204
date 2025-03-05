using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using memento.Models;

namespace memento.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly Originator _originator;
       private readonly caretaker _caretaker;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
         _caretaker = new caretaker();
          _originator = new Originator(_caretaker);
          
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

     [HttpPost]
    public IActionResult SetContent(string txtEditor)
    {
      
        _originator.SetContent(txtEditor);
          ViewBag.message=txtEditor;
        return View("Index"); 
    }
 [HttpPost]

    public IActionResult getContent()
    {
      string content=_caretaker.getMemento();
      ViewBag.message=content;
     return View("Index"); 
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    
}

public class Originator
{

      private readonly caretaker _caretaker;
    private string _editorContents;
     public Originator(caretaker caretaker)
    {
       _caretaker=new caretaker();
    }

[HttpPost]
    public void SetContent(string content)
    {
        _editorContents = content;
      _caretaker.saveMemento(_editorContents);
   
    }

    public string GetContent()
    {
       
        return _editorContents;
    }
}
 
public class caretaker{

static private List<string> prevEntries = new List<string>();

    public void saveMemento(string newMemento){


    prevEntries.Add(newMemento);
   
    }

    public string getMemento(){
        if(prevEntries.Count==null || prevEntries.Count==0){
        string nullResult="No mementos to reload";

               return nullResult;
        }
        string result=prevEntries[prevEntries.Count()-1];

        
        prevEntries.RemoveAt (prevEntries.Count-1);

        return result;
    }
}

