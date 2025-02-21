using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ToDoList.Controller;
using ToDoList.Model;
using ToDoList.Persistence;
using ToDoList.View;

namespace ToDoList.View
{
    public partial class PriorityView : UserControl
    {
        private readonly TodoController controller;

        public PriorityView()
        {
            InitializeComponent();
            controller = new TodoController(); // Instanziiert den Controller
            UpdatePriorityList(); // Lädt und zeigt die sortierte Liste an
        }

        // Aktualisiert die To-Do-Liste nach Priorität
        private void UpdatePriorityList()
        {
            // Sortiere die To-Do-Liste nach Priorität
            var sortedTodos = controller.GetSortedTodoItemsByPriority();

            // Verknüpft die sortierte Liste mit der ListBox
            PriorityListBox.ItemsSource = sortedTodos;
        }

        // Markiert ein ausgewähltes To-Do-Item  durch Doppelklick als erledigt
        private void PriorityListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedTask = PriorityListBox.SelectedItem as TodoItem;

            if (selectedTask == null)
            {
                MessageBox.Show("Bitte einen Task auswählen.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Markiert das Item als erledigt und aktualisiert die Liste
            controller.MarkAsCompleted(selectedTask);
            UpdatePriorityList();
        }
    }
}