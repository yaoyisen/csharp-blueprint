using CommunityToolkit.Mvvm.ComponentModel;
using CSharpBlueprint.WinUI3.ViewModel;
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

    [INotifyPropertyChanged]
    public sealed partial class MainPage : Page
    {

        [ObservableProperty]
        public partial SolutionViewModel? CurrentSolutionViewModel { get; private set; } = null;


        public MainPage()
        {
            this.InitializeComponent();
        }

        private void CreateEmptySolutionCommand_ExecuteRequested(XamlUICommand sender, ExecuteRequestedEventArgs args)
        {
            var workspace = Core.DataProvider.CreateEmptyWithTestData();
            CurrentSolutionViewModel = new(workspace);
        }
    }
}
