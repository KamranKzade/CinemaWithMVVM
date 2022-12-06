using CinemaWithMVVM.Models;

namespace CinemaWithMVVM.ViewModels;

public class UCViewModel : BaseViewModel
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
    public UCViewModel()
    {

    }

}
