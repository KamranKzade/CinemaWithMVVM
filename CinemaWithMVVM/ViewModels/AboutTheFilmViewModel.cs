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
    public RelayCommand CommentCommand { get; set; }


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

            var uc = new Uc_AboutTheFilm();
            var vm = new Uc_AboutViewModel();
            vm.Movie = Movie;


            //// Add Stars 
            vm.wrapPanel = uc.wrapPanel;

            var str = vm!.Movie?.Ratings![0].Value!.Substring(0, 3);
            double result = (double.Parse(str!) + 1) / 2;
            int count = (int)result;
            
            for (int i = 0; i < count; i++)
                vm.wrapPanel.Children.Add(new Star_Uc());

            vm.wrapPanel.Margin = new System.Windows.Thickness(5, 0, 0, 0);

            uc.DataContext = vm;
            scrollViewer!.Content = (uc);
        });


        YoutubeCommand = new RelayCommand((o) =>
        {
            var scrollViewer = o as ScrollViewer;
            scrollViewer!.Content = string.Empty;


            WebBrowser webBrowser = new WebBrowser();

            webBrowser.Navigate($"https://www.youtube.com/results?search_query={Movie!.Title}+trailer");
            scrollViewer!.Content = webBrowser;

        });

        CommentCommand = new RelayCommand((o) =>
        {
            var scrollViewer = o as ScrollViewer;
            scrollViewer!.Content = string.Empty;

            Comment_UC comment = new Comment_UC();
            var commentVm = new Comment_UCViewModel();
            commentVm.Movie = movie;

            comment.DataContext = commentVm;

            Movie = commentVm.Movie;

            scrollViewer.Content = comment;
        });
    }
}