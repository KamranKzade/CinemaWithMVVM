using CinemaWithMVVM.Commands;
using CinemaWithMVVM.Models;
using CinemaWithMVVM.Views.UserControls;
using System.Windows.Controls;

namespace CinemaWithMVVM.ViewModels;

public class AboutTheFilmViewModel : BaseViewModel
{
    private Movie? movie;

    public RelayCommand AboutFilmCommand { get; set; }
    public RelayCommand YoutubeCommand { get; set; }


    public Movie? Movie
    {
        get { return movie; }
        set
        {
            movie = value;
            OnPropertyChanged();
        }
    }

    public AboutTheFilmViewModel()
    {
        AboutFilmCommand = new RelayCommand((o) =>
        {
            var scrollViewer = o as ScrollViewer;
            scrollViewer!.Content = string.Empty;

            var vm = new Uc_AboutViewModel();
            vm.Movie = Movie;
            var uc = new Uc_AboutTheFilm();
            uc.DataContext = vm;
            scrollViewer!.Content = (uc);

        });


        YoutubeCommand = new RelayCommand((o) =>
        {
            var scrollViewer = o as ScrollViewer; 
            scrollViewer!.Content = string.Empty;


            WebBrowser webBrowser = new WebBrowser();

            webBrowser.Navigate($"https://www.youtube.com/results?search_query={Movie!.Title}+trailer");
            scrollViewer!.Content= webBrowser;

        });
    }
}