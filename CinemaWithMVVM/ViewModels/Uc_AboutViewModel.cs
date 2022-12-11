using CinemaWithMVVM.Models;

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

    public Uc_AboutViewModel()
    {

    }
}
