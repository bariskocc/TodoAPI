using Microsoft.AspNetCore.Mvc;

namespace TodoAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private List<TodoItem> _todoItems;

    public TodoController()
    {
        // Örnek veri oluşturma
        _todoItems = new List<TodoItem>
        {
            new TodoItem { Id = 1, Title = "Todo 1", IsCompleted = false },
            new TodoItem { Id = 2, Title = "Todo 2", IsCompleted = true },
            new TodoItem { Id = 3, Title = "Todo 3", IsCompleted = false },
            new TodoItem { Id = 4, Title = "Todo 4", IsCompleted = false }
        };
    }

    [HttpGet]
    public IActionResult Get() 
    {
        return Ok(_todoItems);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var todoItem = _todoItems.FirstOrDefault(item => item.Id == id);
        if (todoItem == null)
            return NotFound();

        return Ok(todoItem);
    }

    [HttpPost]
    public IActionResult Create(TodoItem todoItem)
    {
        todoItem.Id = _todoItems.Max(item => item.Id) + 1;
        _todoItems.Add(todoItem);

        return CreatedAtAction(nameof(GetById), new { id = todoItem.Id }, todoItem);
    }
     [HttpPut("{id}")]
    public IActionResult Update(int id, TodoItem todoItem)
    {
        var existingTodoItem = _todoItems.FirstOrDefault(item => item.Id == id);
        if (existingTodoItem == null)
            return NotFound();

        existingTodoItem.Title = todoItem.Title;
        existingTodoItem.IsCompleted = todoItem.IsCompleted;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var todoItem = _todoItems.FirstOrDefault(item => item.Id == id);
        if (todoItem == null)
            return NotFound();

        _todoItems.Remove(todoItem);

        return NoContent();
    }
}
