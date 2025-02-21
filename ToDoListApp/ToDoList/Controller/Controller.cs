using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ToDoList.View;
using ToDoList.Model;
using ToDoList.Persistence;

namespace ToDoList.Controller
{
    public class TodoController
    {
        private List<TodoItem> todoItems;

        // public TodoController = Konstruktor (Name muss immer exakt mit Klassennamen übereinstimmen): 
        // Ein Konstruktor ist eine spezielle Methode in einer Klasse, die aufgerufen wird, wenn ein Objekt dieser Klasse erstellt wird.
        // Initialisiert das Objekt, setzt also Startwert für die Eigenschaften vom Objekt oder führt notwendige Aktionen aus.

        // Lädt die gespeicherte To-Do-Liste aus der JSON-Datei, damit die Anwendung beim Start mit den vorhandenen Daten arbeiten kann.
        public TodoController()
        {
            todoItems = TodoStorage.Load(); // Lädt die To-Do-Liste aus der JSON-Datei
        }

        // Hinzufügen eines neuen To-Do-Items
        public void AddTodoItem(string title, Category category, Priority priority)
        {
            // falls Titel leer ist
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Fehlender Titel", nameof(title));

            var newTodo = new TodoItem
            (
                title,category,priority,false
            );

            todoItems.Add(newTodo); // Fügt das neue To-Do-Item zur Liste hinzu
            Save(); // Speichert die aktualisierte Liste
        }

        // Abrufen aller To-Do-Items
        public List<TodoItem> GetTodoItems()
        {
            return todoItems; // Gibt die Liste der To-Do-Items zurück
        }

        // Markieren eines To-Do-Items als erledigt
        public void MarkAsCompleted(TodoItem task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            if (todoItems.Contains(task)) // Prüft, ob das Item in der Liste enthalten ist
            {
                task.IsCompleted = true; // Markiere das Item als "erledigt"
                Save(); // Speichere die Änderungen
            }
        }

        // Markieren eines To-Do-Items als nicht erledigt
        public void MarkAsNotCompleted(TodoItem task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            if (todoItems.Contains(task)) // Prüft, ob das Item in der Liste enthalten ist
            {
                task.IsCompleted = false; // Markiere das Item als "nicht erledigt"
                Save(); // Speichere die Änderungen
            }
        }

        // Löschen eines To-Do-Items
        public void DeleteTodoItem(TodoItem task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));

            if (todoItems.Contains(task)) // Prüft, ob das Item in der Liste enthalten ist
            {
                todoItems.Remove(task); // Entfernt das Item direkt
                Save(); // Speichert die aktualisierte Liste
            }
        }

        // Nach Priorität sortieren (verwendet die ausgelagerte Filterlogik)
        public List<TodoItem> GetSortedTodoItemsByPriority()
        {
            return TodoFilter.GetSortedByPriority(todoItems);
        }

        // Nach Kategorie filtern (verwendet die ausgelagerte Filterlogik)
        public List<TodoItem> GetTasksByCategory(Category category)
        {
            return TodoFilter.GetByCategory(todoItems, category);
        }

        // Getrennte Listen: offene und erledigte To-Do-Items (verwendet die ausgelagerte Filterlogik)
        public (List<TodoItem> Open, List<TodoItem> Completed) GetFilteredTodoItems()
        {
            return TodoFilter.GetFilteredItems(todoItems);
        }

        // Speichern der aktuellen To-Do-Liste
        public void Save()
        {
            TodoStorage.Save(todoItems);
        }
    }
}