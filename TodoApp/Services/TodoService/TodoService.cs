using Microsoft.EntityFrameworkCore;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Services.TodoService;

public class TodoService: ITodoService
{
    private readonly TodoAppContext _context;

    public TodoService(TodoAppContext context)
    {
        _context = context;
    }

    public async Task Create(TodoModel newTodo)
    {
        _context.Todos.Add(newTodo);
        await _context.SaveChangesAsync();
    }

    public Task<TodoModel> GetOne()
    {
        throw new NotImplementedException();
    }

    
    public async Task<IEnumerable<TodoModel>> GetAll()
    {
        var todos = await _context.Todos.ToListAsync();
        return todos;
    }
    
    public async Task<TodoModel> Update(int id, TodoModel updatedTodo)
    {
        var existingTodo = await FindById(id);

        if (existingTodo == null)
        {
            throw new Exception("Todo not found!");
        }
        
        _context.Todos.Update(updatedTodo);
        await _context.SaveChangesAsync();

        return existingTodo;
    }

    public async Task<bool> Delete(int id)
    {
        var todo = await FindById(id);
        if (todo == null) return false;
        
        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();
        
        return true;
    }

    public async Task<TodoModel?> FindById(int id)
    {
        return await _context.Todos.FirstOrDefaultAsync(todo => todo.Id == id);
    }
}