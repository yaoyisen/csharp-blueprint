using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpBlueprint.WinUI3.ViewModel
{
    public enum DisplayMode
    {
        Code,
        BluePrint,
    }

    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CurrentDisplay))]
        public partial SolutionViewModel? CurrentSolution { get; private set; } = null;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CurrentDisplay))]
        public partial DocumentViewModel? CurrentSelected { get; set; } = null;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CurrentDisplay))]
        public partial DisplayMode DisplayMode { get; set; } = DisplayMode.Code;

        public string[] DisplayModeOptions = Enum.GetNames<DisplayMode>();

        public object? CurrentDisplay
        {
            get
            {
                if (CurrentSelected != null)
                {
                    switch (DisplayMode)
                    {
                        case DisplayMode.Code:
                            return CurrentSelected.Text;
                        case DisplayMode.BluePrint:
                            return null;
                        default:
                            return null;
                    }
                }
                return null;
            }
        }


        [RelayCommand]
        public void CreateEmptySolution()
        {
            var workspace = Core.DataProvider.CreateEmptyWithTestData();
            CurrentSolution = new(workspace);
        }
    }
}
