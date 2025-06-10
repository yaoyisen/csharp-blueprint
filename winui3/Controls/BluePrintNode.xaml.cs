using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.ComponentModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CSharpBlueprint.WinUI3.Controls
{

    public sealed partial class BluePrintNode : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private BluePrintCanvas _parentCanvas;
        private SyntaxNode _syntax;
        private string Header = "header";

        private List<SyntaxNode> inputs = [];
        private List<SyntaxNode> outputs = [];
        public BluePrintNode(BluePrintCanvas canvas, SyntaxNode syntax)
        {
            this.InitializeComponent();
            this.CanDrag = true;
            this._parentCanvas = canvas;
            this._syntax = syntax;
            Header = syntax.ToString();
            if (syntax is ClassDeclarationSyntax classDeclarationSyntax)
            {
                Header = $"Class {classDeclarationSyntax.Identifier}";
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



        #region event handler

        private void BluePrintNode_DoubleTapped(object sender, Microsoft.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {
            _parentCanvas.DocumentVM.BreadcrumbItems.Add(_syntax);
            _parentCanvas.DocumentVM.CurrentNode = _syntax;
        }

        private void BluePrintNode_DragStarting(UIElement sender, DragStartingEventArgs e)
        {
            e.AllowedOperations = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Move;
            e.Data.Properties.Add("node", this);
        }

        #endregion event handler
    }
}
