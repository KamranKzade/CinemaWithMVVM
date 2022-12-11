using CinemaWithMVVM.Models;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace CinemaWithMVVM.ViewModels;

public class Uc_AboutViewModel : BaseViewModel
{
    private Movie? movie;
    public Movie? Movie
    {
        get { return movie; }
        set
        {
            movie = value;
            OnPropertyChanged();
        }
    }

    public WrapPanel wrapPanel { get; set; }

    public Uc_AboutViewModel()
    {
        wrapPanel = new ();
    }
}
