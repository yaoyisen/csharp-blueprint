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
        [NotifyPropertyChangedFor(nameof(Name))]
        [NotifyPropertyChangedFor(nameof(Documents))]
        [NotifyPropertyChangedFor(nameof(Items))]
        private Project project = project;

        public IEnumerable<DocumentViewModel> Documents => Project.Documents.Select(doc => new DocumentViewModel(doc));
        public IEnumerable<ITreeViewItem> Items => Documents;
        public string Name => Project.Name;
    }
}
