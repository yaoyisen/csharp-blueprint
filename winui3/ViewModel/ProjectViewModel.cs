using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CSharpBlueprint.WinUI3.ViewModel
{
    public partial class ProjectViewModel(Project project) : ObservableObject, ITreeViewItem
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Name), nameof(Documents), nameof(ChildItems))]
        private partial Project Project { get; set; } = project;

        public IEnumerable<DocumentViewModel> Documents => Project.Documents.Select(doc => new DocumentViewModel(doc));
        public IEnumerable<ITreeViewItem> ChildItems => Documents;
        public string Name => Project.Name;
    }
}
