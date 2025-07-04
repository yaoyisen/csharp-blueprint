using CommunityToolkit.Mvvm.ComponentModel;
using CSharpBlueprint.WinUI3.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CSharpBlueprint.WinUI3
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>

    public sealed partial class MainPage : Page
    {
        private readonly MainPageViewModel ViewModel;

        public MainPage()
        {
            this.InitializeComponent();
            this.ViewModel = App.Current.Services.GetService<MainPageViewModel>()!;
        }

        public readonly string[] DisplayModeOptions = Enum.GetNames<DisplayMode>();

        private void SolutionResourceTree_ItemInvoked(TreeView sender, TreeViewItemInvokedEventArgs args)
        {
            if (args.InvokedItem is DocumentViewModel viewModel)
            {
                this.ViewModel.CurrentSelected = viewModel;
            }
        }

        private void ChangeDisplayModeCommand_ExecuteRequested(XamlUICommand sender, ExecuteRequestedEventArgs args)
        {
            if (args.Parameter is string name && Enum.TryParse(name, out DisplayMode result))
            {
                this.ViewModel.DisplayMode = result;
            }
        }
    }
}
