using CinemaWithMVVM.Commands;
using CinemaWithMVVM.Models;

namespace CinemaWithMVVM.ViewModels;

public class Comment_UCViewModel : BaseViewModel
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

    public RelayCommand FirstStartCommand { get; set; }
    public RelayCommand SecondStartCommand { get; set; }
    public RelayCommand ThirdStartCommand { get; set; }
    public RelayCommand FourthStartCommand { get; set; }
    public RelayCommand FifthStartCommand { get; set; }

    public Comment_UCViewModel()
    {
        Movie = new();
        FirstStartCommand = new RelayCommand((o) =>
        {
            Movie.Ratings![0].Value="2.0/10";
        });

        SecondStartCommand = new RelayCommand((o) =>
        {
            Movie.Ratings![0].Value = "4.0/10";
        });

        ThirdStartCommand = new RelayCommand((o) =>
        {
            Movie.Ratings![0].Value = "6.0/10";
        });

        FourthStartCommand = new RelayCommand((o) =>
        {
            Movie.Ratings![0].Value = "8.0/10";
        });

        FifthStartCommand = new RelayCommand((o) =>
        {
            Movie.Ratings![0].Value = "10.0/10";
        });
    }

}
