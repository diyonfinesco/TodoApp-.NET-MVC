using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;
using TodoApp.Services.TodoService;

namespace TodoApp.Controllers;

public class TodoController : Controller
{
    private readonly ILogger<TodoController> _logger;
    private readonly ITodoService _todoService;

    public TodoController(ILogger<TodoController> logger, ITodoService todoService)
    {
        _logger = logger;
        _todoService = todoService;
    }

    public async Task<IActionResult> Index()
    {
        var todos = await _todoService.GetAll();
        return View(todos);
    }

    public IActionResult CreatePage()
    {
        return View();
    }

    public async Task<IActionResult> CreateTodo([Bind("Id, Title, Description")] TodoModel newTodo)
    {
        if (!ModelState.IsValid)
            return View("CreatePage", newTodo);

        await _todoService.Create(newTodo);
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> EditPage(int id)
    {
        var todo = await _todoService.FindById(id);

        if (todo == null) return RedirectToAction("Index");

        return View(todo);
    }
    
    [HttpPost]
    public async Task<IActionResult> UpdateTodo(int id, [Bind("Id,Title,Description")] TodoModel updateTodo)
    {
        if (!ModelState.IsValid)
            return View("EditPage", updateTodo);

        await _todoService.Update(id, updateTodo);
        TempData["SuccessMessage"] = "Todo updated successfully!";
        return RedirectToAction("Index");
    }
    
    public async Task<IActionResult> DeletePage(int id)
    {
        var todo = await _todoService.FindById(id);

        if (todo == null) return RedirectToAction("Index");

        return View(todo);
    }
    
    [HttpPost]
    public async Task<IActionResult> DeleteTodo(int id)
    {
        var todo = await _todoService.FindById(id);

        if (todo != null)
        {
            await _todoService.Delete(id);
            TempData["SuccessMessage"] = "Todo deleted successfully!";
        }
        
        return RedirectToAction("Index");
    }
}