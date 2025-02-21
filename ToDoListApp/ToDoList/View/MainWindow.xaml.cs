using ToDoList.Controller;
using ToDoList.Model;
using ToDoList.View;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using System.Windows.Media.Imaging;

namespace ToDoList
{
    public partial class MainWindow : Window
    {
        private readonly NavigationController controller;

        public MainWindow()
        {
            InitializeComponent();
            controller = new NavigationController(this);

            // Lade die Standardansicht
            controller.NavigateTo(new Home());

            //Symbol laden
            this.Icon = ConvertByteArrayToImage(Properties.Resources.Logo);
        }

        //Umwandlung der Resource in ein BitmapImage
        public BitmapImage? ConvertByteArrayToImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0)
                return null;

            BitmapImage image = new BitmapImage();
            using (MemoryStream ms = new MemoryStream(imageData))
            {
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = ms;
                image.EndInit();
            }
            image.Freeze(); // Optional: Freeze to make it cross-thread accessible
            return image;
        }

        // Navigation zwischen den Ansichten
        private void NavigateToPriorityView(object sender, RoutedEventArgs e) // Priority
        {
            controller.NavigateTo(new PriorityView());
        }

        private void NavigateToCategoryView(object sender, RoutedEventArgs e) // School
        {
            controller.NavigateTo(new CategoryView());
        }

        private void NavigateToCategory2View(object sender, RoutedEventArgs e) // Private
        {
            controller.NavigateTo(new Category2View());
        }

        private void NavigateToHomeView(object sender, RoutedEventArgs e) // Home
        {
            controller.NavigateTo(new Home());
        }
    }
}
