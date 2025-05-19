using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.ComponentModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CSharpBlueprint.WinUI3.Controls
{

    public sealed partial class BluePrintNode : UserControl, INotifyPropertyChanged
    {
        private BluePrintCanvas _canvas;
        private SyntaxNode _syntax;
        public BluePrintNode(BluePrintCanvas canvas, SyntaxNode syntax)
        {
            this.InitializeComponent();
            this.CanDrag = true;
            this._canvas = canvas;
            this._syntax = syntax;
            this.DragStarting += (s, e) =>
            {
                e.AllowedOperations = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Move;
                e.Data.Properties.Add("node", this);
            };
            Header = syntax.ToString();
            if (syntax is ClassDeclarationSyntax classDeclarationSyntax)
            {
                Header = $"Class {classDeclarationSyntax.Identifier.ToString()}";
            }
            else if (syntax is MemberDeclarationSyntax memberDeclarationSyntax)
            {
                Header = memberDeclarationSyntax.AttributeLists.ToString();
                if (memberDeclarationSyntax is MethodDeclarationSyntax methodDeclarationSyntax)
                {
                    Header = methodDeclarationSyntax.Identifier.ToString();
                    inputs = [.. methodDeclarationSyntax.ParameterList.Parameters];
                    outputs = [methodDeclarationSyntax.ReturnType];
                }
            }
        }

        private string Header = "header";

        private List<SyntaxNode> inputs = [];
        private List<SyntaxNode> outputs = [];

        public event PropertyChangedEventHandler? PropertyChanged;

        private void Border_DoubleTapped(object sender, Microsoft.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {
            _canvas.DocumentVM.CurrentNode = _syntax;
        }
    }
}
