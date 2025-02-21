using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ToDoList.Controller;
using ToDoList.Model;

namespace ToDoList.View
{

    public partial class Home : UserControl
    {
        private TodoController controller;

        public Home()
        {
            InitializeComponent();
            controller = new TodoController();  // Erstellt eine neue Instanz vom TodoController und ruft seinen Konstruktor auf. Dadurch möglich auf die Methoden und Eigenschaften des Controllers über die Variable 'controller' zuzugreifen.
            UpdateTodoList();  // Aktualisiert die ListBox mit den geladenen Daten aus der JSON, falls vorhanden
        }

       
        // Hinzufügen eines neuen To-Do-Items
        private void AddTodo_Click(object sender, RoutedEventArgs e)
        {
            string title = NewTodoTextBox.Text;  // Holt den Text aus dem Textfeld

            // Kategorie aus Category-ComboBox auslesen und in Enum umwandeln. 
            var selectedCategoryItem = CategoryComboBox.SelectedItem as ComboBoxItem;
            if (selectedCategoryItem == null) // Fehlermeldung, falls keine Kategorie ausgewählt wurde
            {
                MessageBox.Show("Bitte eine Kategorie auswählen.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // hier wird der Variablen selectedCategory der Enum-Wert zugewiesen, der für das jeweilige ToDoItem in der Combobox angegeben wurde
            Category selectedCategory = selectedCategoryItem.Content.ToString() switch
            {
                "School" => Category.School,
                "Private" => Category.Private,
                _ => throw new Exception("Ungültige Kategorie") //_ => im switch-ausdruck wildcard-zweig = default im klassischen switch
                                                                
            };
            

            // Priorität aus Priority-ComboBox auslesen und in Enum umwandeln
            var selectedPriorityItem = PriorityComboBox.SelectedItem as ComboBoxItem;
            if (selectedPriorityItem == null)
            {
                MessageBox.Show("Bitte eine Priorität auswählen.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            Priority selectedPriority = selectedPriorityItem.Content.ToString() switch
            {
                "High" => Priority.High,
                "Medium" => Priority.Medium,
                "Low" => Priority.Low,
                _ => throw new Exception("Ungültige Kategorie")  
            };

            // Überprüfet, ob alle Felder gültig sind
            if (!string.IsNullOrWhiteSpace(title))
            {
                // Fügt das neue To-Do-Item hinzu
                controller.AddTodoItem(title, selectedCategory, selectedPriority);

                // Aktualisiert die Liste und setzt die Eingaben zurück
                UpdateTodoList();
                NewTodoTextBox.Text = string.Empty;
                CategoryComboBox.SelectedIndex = -1;  // Zurücksetzen der Kategorie
                PriorityComboBox.SelectedIndex = -1;  // Zurücksetzen der Priorität         
                CategoryComboBox.Text = "Category";   // Text wieder setzen
                PriorityComboBox.Text = "Priority";  // Text wieder setzen
            }
            else
            {
                MessageBox.Show("Bitte einen Titel eingeben.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Warning); 
            }
        }

        // Add-Button ist nur aktiviert, wenn Text im Textfeld steht und dieser nicht nur aus Leerzeichen besteht
        private void NewTodoTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string title = NewTodoTextBox.Text;
            AddTodoButton.IsEnabled = !string.IsNullOrWhiteSpace(title);  // Aktiviert/deaktiviert den Button
        }

        // Löschen eines Tasks mit Button
        private void DeleteTodo_Click(object sender, RoutedEventArgs e)
        {
            // Überprüft, ob ein Task in einer der beiden Listen ausgewählt ist
            var selectedTask = TodoListBox.SelectedItem as TodoItem ?? TodoListBox_Done.SelectedItem as TodoItem;

            if (selectedTask == null)
            {
                // Zeigt eine Fehlermeldung an, wenn kein Task ausgewählt wurde
                MessageBox.Show("Bitte wähle ein Item aus einer Liste aus.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Übergabe an den Controller
            controller.DeleteTodoItem(selectedTask);

            // UI aktualisieren
            UpdateTodoList();
        }

        // Aktualisiert die Liste (offen und erledigt)
        private void UpdateTodoList()
        {
            // Leert die bestehenden ListBoxen
            TodoListBox.Items.Clear();
            TodoListBox_Done.Items.Clear();

            // Abruf der gefilterten Listen vom Controller
            var todoLists = controller.GetFilteredTodoItems();

            // offene Tasks
            foreach (var todo in todoLists.Open)
            {
                TodoListBox.Items.Add(todo);
            }

            // erledigte Tasks
            foreach (var todo in todoLists.Completed)
            {
                TodoListBox_Done.Items.Add(todo);
            }
        }

        // Tasks verschieben mit Doppelklick
        private void TodoListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedTask = TodoListBox.SelectedItem as TodoItem;
            if (selectedTask == null)
            {
                MessageBox.Show("Bitte einen Task auswählen.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            controller.MarkAsCompleted(selectedTask); // Übergib das Item direkt
            UpdateTodoList(); // Aktualisiert die Listen
        }

        // Tasks verschieben mit Doppelklick
        private void TodoListBox_Done_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedTask = TodoListBox_Done.SelectedItem as TodoItem;
            if (selectedTask == null)
            {
                MessageBox.Show("Bitte einen Task auswählen.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            controller.MarkAsNotCompleted(selectedTask); // Übergib das Item direkt
            UpdateTodoList(); // Aktualisiere die Listen
        }
    }
}
