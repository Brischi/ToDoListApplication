using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ToDoList.Controller;
using ToDoList.Model;

namespace ToDoList.View
{
    public partial class CategoryView : UserControl
    {
        private readonly TodoController controller;

        public CategoryView()
        {
            InitializeComponent();
            controller = new TodoController();
            UpdateSchoolList();
        }

        private void UpdateSchoolList()
        {
            // Verbindet die gefilterte Liste der Tasks mit der ListBox
            SchoolListBox.ItemsSource = controller.GetTasksByCategory(Category.School);
        }

        private void CategoryListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Holt das ausgewählte To-Do-Item
            var selectedTask = SchoolListBox.SelectedItem as TodoItem;

            if (selectedTask == null)
            {
                MessageBox.Show("Bitte einen Task auswählen.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Markiert das To-Do als abgeschlossen
            controller.MarkAsCompleted(selectedTask);

            // Aktualisiert die Liste, um abgeschlossene Aufgaben auszublenden
            UpdateSchoolList();
        }
    }
}