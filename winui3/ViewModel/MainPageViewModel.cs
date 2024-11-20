using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpBlueprint.WinUI3.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        public partial SolutionViewModel? CurrentSolution { get; set; } = null;

        [RelayCommand]
        public void CreateEmptySolution()
        {
            var workspace = Core.DataProvider.CreateEmptyWithTestData();
            CurrentSolution = new(workspace);
        }
    }
}
