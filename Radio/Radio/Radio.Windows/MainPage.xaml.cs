﻿using Windows.UI.Xaml.Controls;

// The Hub Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=321224
using Windows.UI.Xaml.Input;
using Radio.Models;
using Radio.ViewModels;

namespace Radio
{
    /// <summary>
    /// A page that displays a grouped collection of items.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();

            DataContext = RadioListViewModel.Instance;
        }

        private void RadioChannelList_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var listBoxItem = (ListBoxItem) sender;
            var radioChannel = (RadioChannel) listBoxItem.DataContext;

            var viewModel = RadioListViewModel.Instance;
            if (viewModel.ChannelTappedCommand.CanExecute(radioChannel))
            {
                viewModel.ChannelTappedCommand.Execute(radioChannel);
            }
        }
    }
}
