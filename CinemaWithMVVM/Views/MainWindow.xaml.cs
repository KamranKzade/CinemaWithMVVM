﻿using CinemaWithMVVM.ViewModels;
using System.Windows;

namespace CinemaWithMVVM.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        var vm = new MainWindowViewModel();
        this.DataContext = vm;
    }
}
