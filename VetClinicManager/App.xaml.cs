using System.Windows;

namespace VetClinicManager;
public partial class App : Application
{
    public App()
    {
        var mainWindow = new MainWindow();
        mainWindow.Show();
    }
}


