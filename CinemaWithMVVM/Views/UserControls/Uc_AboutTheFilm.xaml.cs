using CinemaWithMVVM.ViewModels;
using System.Windows.Controls;

namespace CinemaWithMVVM.Views.UserControls;


public partial class Uc_AboutTheFilm : UserControl
{
    public Uc_AboutTheFilm()
    {
        InitializeComponent();

        var Vm = new Uc_AboutViewModel();
        this.DataContext = Vm;
    }
}
