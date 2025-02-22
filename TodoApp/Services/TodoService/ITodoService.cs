using TodoApp.Models;

namespace TodoApp.Services.TodoService;

public interface ITodoService
{
    public Task Create(TodoModel newTodo);
    public Task<IEnumerable<TodoModel>> GetAll();
    public Task<TodoModel> GetOne();
    public Task<TodoModel> Update(int id, TodoModel updatedTodo);
    public Task<bool> Delete(int id);
    public Task<TodoModel?> FindById(int id);
}