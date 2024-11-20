using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpBlueprint.WinUI3.ViewModel
{
    public partial class DocumentViewModel(Document document) : ObservableObject, ITreeViewItem
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Name), nameof(Text))]
        private partial Document Document { get; set; } = document;
       
        public string Text
        {
            get => Document.GetTextAsync().Result.ToString();
            set
            {
                Document = Document.WithText(SourceText.From(value));
            }
        }

        public string Name => Document.Name;

        public IEnumerable<ITreeViewItem> Items => [];
    }
}
