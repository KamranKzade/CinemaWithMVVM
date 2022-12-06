using CinemaWithMVVM.Commands;
using CinemaWithMVVM.Models;
using CinemaWithMVVM.Views.UserControls;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace CinemaWithMVVM.ViewModels;


public class MainViewModel : BaseViewModel
{
    string jsonStr;
    private List<string> _movieDataBase;
    HttpClient httpClient = new HttpClient();


    public UniformGrid uniformGrid { get; set; }

    //public Window MyWindow { get; set; }
    public RelayCommand SearchCommand { get; set; }




    public MainViewModel()
    {
        _movieDataBase = new List<string>();



      //  _movieDataBase = JsonSerializer.Deserialize<List<string>>(File.ReadAllText("../../../DataBase/MovieDataBase.json"))!;


        SearchCommand = new RelayCommand(async (o) =>
        {
            var TextBox_Search = o as TextBox;

            if (string.IsNullOrWhiteSpace(TextBox_Search!.Text))
            {
                MessageBox.Show("Please Enter Movie Name", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            jsonStr = await httpClient.GetStringAsync($@"http://www.omdbapi.com/?apikey=82bcd4c7&t={TextBox_Search.Text}");

            if (!jsonStr.Contains("Error"))
            {
                var movie = JsonSerializer.Deserialize<Movie>(jsonStr);

                var uc = new UserControl_Movie();
                var ucVm = new UCViewModel();
                ucVm.Movie = movie;
                uc.DataContext = ucVm;

                uniformGrid!.Children.Add(uc);

                // MoreInformationAboutTheFilm more = new(movie);
                // more.ShowDialog();

                _movieDataBase!.Add(movie?.Title!);
                string str = JsonSerializer.Serialize(_movieDataBase);
                File.WriteAllText("../../../DataBase/MovieDataBase.json", str);
                return;
            }
            else
                MessageBox.Show("No Result Found", "Information", MessageBoxButton.OK, MessageBoxImage.Information);


        });

        // MyWindow!.Loaded += Window_Loaded;
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        foreach (var m in _movieDataBase)
        {
            jsonStr = httpClient.GetStringAsync($"http://www.omdbapi.com/?apikey=82bcd4c7&t={m}&plot=full").Result;

            if (!jsonStr.Contains("Error"))
            {
                var movie = JsonSerializer.Deserialize<Movie>(jsonStr);
                var ucVm = new UCViewModel();
                ucVm.Movie = movie;
                var uc = new UserControl_Movie();

                uc.DataContext = ucVm;
                uniformGrid.Children.Add(uc);
            }
        }
    }
}