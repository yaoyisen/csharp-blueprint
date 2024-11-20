using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CSharpBlueprint.WinUI3.ViewModel
{
    public partial class SolutionViewModel(Workspace workspace) : ObservableObject, ITreeViewItem
    {
        private readonly Workspace m_workspace = workspace;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Name), nameof(Projects), nameof(Items))]
        public partial Solution Solution { get; set; } = workspace.CurrentSolution;

        public IEnumerable<ProjectViewModel> Projects => Solution.Projects.Select(project => new ProjectViewModel(project));

        public IEnumerable<ITreeViewItem> Items => Projects;

        public string Name => Solution.Id.Id.ToString();

    }
}
