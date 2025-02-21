using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ToDoList.Controller;
using ToDoList.Model;

namespace ToDoList.View
{
 
    public partial class Category2View : UserControl
    {
        private readonly TodoController controller;

        public Category2View()
        {
            InitializeComponent();
            controller = new TodoController();
            UpdatePrivateList();
        }
        /// <summary>
        /// Test
        /// </summary>
        private void UpdatePrivateList()
        {
            PrivateListBox.ItemsSource = controller.GetTasksByCategory(Category.Private);
        }

        private void Category2ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Hole das ausgewählte To-Do-Itemmm
            var selectedTask = PrivateListBox.SelectedItem as TodoItem;

            if (selectedTask == null)
            {
                MessageBox.Show("Bitte einen Task auswählen.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Markiere das To-Do als abgeschlossen
            controller.MarkAsCompleted(selectedTask);

            // Aktualisiere die Liste, um abgeschlossene Aufgaben auszublenden
            UpdatePrivateList();
        }
    }
}
