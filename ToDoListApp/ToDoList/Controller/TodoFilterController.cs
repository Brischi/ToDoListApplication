using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Windows.Shapes;
using ToDoList.Model;

namespace ToDoList.Controller
{
    public static class TodoFilter
    {
        //Filter
        public static List<TodoItem> GetSortedByPriority(List<TodoItem> todoItems)
        {
            return todoItems
                .Where(todo => !todo.IsCompleted)
                .OrderBy(todo => todo.Priority)
                .ToList();
        }

        public static List<TodoItem> GetByCategory(List<TodoItem> todoItems, Category category)
        {
            return todoItems
                .Where(todo => todo.Category == category && !todo.IsCompleted)
                .OrderBy(todo => todo.Priority)
                .ToList();
        }

        public static (List<TodoItem> Open, List<TodoItem> Completed) GetFilteredItems(List<TodoItem> todoItems)
        {
            var open = todoItems.Where(todo => !todo.IsCompleted).ToList();
            var completed = todoItems.Where(todo => todo.IsCompleted).ToList();
            return (open, completed);
        }

        //LINQ:Erweiterungsmethoden für die Abfragefuktionen
        //.ToList(): Die Ergebnisse der LINQ-Abfragen(Where und OrderBy) werden in eine neue List<TodoItem> umgewandelt. Dadurch kann ich die Ergebnisse als List<TodoItem> weiterverarbeiten oder zurückgeben.
    }
}