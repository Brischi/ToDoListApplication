using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ToDoList.Controller
{

    //Navigation zwischen den Seiten
    public class NavigationController
    {
        private readonly MainWindow _mainWindow;

        public NavigationController(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        public void NavigateTo(UserControl view)
        {
            if (view == null)
            {
                throw new Exception("View darf nicht null sein.");
            }

            if (_mainWindow.MainContent == null)
            {
                throw new Exception("MainContent wurde nicht initialisiert.");
            }

            _mainWindow.MainContent.Content = view;
        }
    }
}
