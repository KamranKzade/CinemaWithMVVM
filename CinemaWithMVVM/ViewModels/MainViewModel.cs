using CinemaWithMVVM.Commands;
using CinemaWithMVVM.Models;
using CinemaWithMVVM.Views;
using CinemaWithMVVM.Views.UserControls;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace CinemaWithMVVM.ViewModels;


public class MainViewModel : BaseViewModel
{
    string jsonStr;
    private List<string> _MovieDataBase;
    HttpClient HttpClient = new HttpClient();

    public RelayCommand SearchCommand { get; set; }
    public RelayCommand KeyDownCommand { get; set; }

    public Movie _movie { get; set; }



    public MainViewModel(UniformGrid uniformGrid)
    {
        _MovieDataBase = new List<string>();
        _movie = new Movie();


        _MovieDataBase = JsonSerializer.Deserialize<List<string>>(File.ReadAllText("../../../DataBase/MovieDataBase.json"))!;



        foreach (var m in _MovieDataBase)
        {
            jsonStr = HttpClient.GetStringAsync($"http://www.omdbapi.com/?apikey=82bcd4c7&t={m}&plot=full").Result;

            if (!jsonStr.Contains("Error"))
            {
                var movie = JsonSerializer.Deserialize<Movie>(jsonStr);
                var uc = new UserControl_Movie();
                var ucVm = new UCViewModel();
                ucVm.Movie = movie;
                uc.DataContext = ucVm;
                uniformGrid.Children.Add(uc);
            }
        }


        SearchCommand = new RelayCommand(async (o) =>
        {
            await Search_Film(uniformGrid, o);
        });


        KeyDownCommand = new RelayCommand(async (o) =>
        {
            await Search_Film(uniformGrid, o);
        });


    }

    private void Uc_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        MoreInformationAboutTheFilm more = new();
        var moreVm = new AboutTheFilmViewModel();
        moreVm.Movie = _movie;
        more.DataContext = moreVm;

        more.ShowDialog();
    }
    
    private async Task Search_Film(UniformGrid uniformGrid, object o)
    {
        var TextBox_Search = o as TextBox;

        if (string.IsNullOrWhiteSpace(TextBox_Search!.Text))
        {
            MessageBox.Show("Please Enter Movie Name", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            return;
        }

        jsonStr = await HttpClient.GetStringAsync($@"http://www.omdbapi.com/?apikey=82bcd4c7&t={TextBox_Search.Text}");

        if (!jsonStr.Contains("Error"))
        {
            var movie = JsonSerializer.Deserialize<Movie>(jsonStr);

            var uc = new UserControl_Movie();
            var ucVm = new UCViewModel();
            ucVm.Movie = movie;
            uc.DataContext = ucVm;

            _movie = movie!;

            uniformGrid!.Children.Add(uc);

            uc.MouseDoubleClick += Uc_MouseDoubleClick;
            _MovieDataBase!.Add(movie?.Title!);
            string str = JsonSerializer.Serialize(_MovieDataBase);
            File.WriteAllText("../../../DataBase/MovieDataBase.json", str);
            return;
        }
        else
            MessageBox.Show("No Result Found", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
    }

}