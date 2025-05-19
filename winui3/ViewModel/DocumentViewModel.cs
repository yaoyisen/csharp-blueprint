using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;

namespace CSharpBlueprint.WinUI3.ViewModel
{
    public partial class DocumentViewModel(Document document) : ObservableObject, ITreeViewItem
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(Name), nameof(Text), nameof(BluePrintNodes))]
        private partial Document Document { get; set; } = document;

        //[ObservableProperty]
        //[NotifyPropertyChangedFor(nameof(BluePrintNodes))]
        //private partial List<SyntaxNode> PathNodes { get; set; } = [];

        public SyntaxNode? CurrentNode
        {
            get => field;
            set
            {
                SetProperty(ref field, value);
                OnPropertyChanged(nameof(BluePrintNodes));
            }
        } = document.GetSyntaxRootAsync().Result;

        public string Text
        {
            get => Document.GetTextAsync().Result.ToString();
            set
            {
                if (value != Text)
                {
                    Document = Document.WithText(SourceText.From(value));
                    CurrentNode = Document.GetSyntaxRootAsync().Result;
                }
            }
        }

        public string Name => Document.Name;

        public IEnumerable<ITreeViewItem> Items => [];


        public List<SyntaxNode> BluePrintNodes
        {
            get
            {
                List<SyntaxNode> nodes = [];
                if (CurrentNode is CompilationUnitSyntax compilationUnitSyntax)
                {
                    foreach (MemberDeclarationSyntax syntax in compilationUnitSyntax.Members)
                    {
                        nodes.Add(syntax);
                    }
                }
                else if (CurrentNode is ClassDeclarationSyntax classDeclarationSyntax)
                {
                    foreach (MemberDeclarationSyntax syntax in classDeclarationSyntax.Members)
                    {
                        nodes.Add(syntax);
                    }
                }
                return nodes;
            }
        }
    }
}
