using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Model
{
    public enum Category
    {
        Private,
        School
    }

    public enum Priority
    {
        High = 1,
        Medium = 2,
        Low = 3
    }
    public class TodoItem //Klassennamen groß schreiben
    {
        public TodoItem(string title, Category category, Priority priority, bool isComplete) { 
            this.Title = title; 
            this.Category = category;
            this.Priority = priority; 
            this.IsCompleted = isComplete;
        }
        public TodoItem() { Title = string.Empty; } //!!!!!!!!!!!!!!!!!!!! Wird benötigt, wenn wir das Objekt automatisiert laden

        //Eigenschaften der Klasse: 
        public string Title { get; set; }  // Titel des To-Do-Items
        public Category Category { get; set; }   // Kategorie als Enum
        public Priority Priority { get; set; }   // Priorität als Enum
        public bool IsCompleted { get; set; }  // Erledigt oder nicht erledigt - false Standardwert
    }
}
