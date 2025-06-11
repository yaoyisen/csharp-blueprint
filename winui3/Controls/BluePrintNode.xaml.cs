using CSharpBlueprint.WinUI3.Controls.Slots;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CSharpBlueprint.WinUI3.Controls
{

    public sealed partial class BluePrintNode : UserControl, INotifyPropertyChanged
    {
        public double Left
        {
            get => field;
            set
            {
                field = value;
                Canvas.SetLeft(this, value);
                foreach (var item in inputs)
                {
                    item.BindPoint.X = value;
                }
                foreach (var item in outputs)
                {
                    item.BindPoint.X = value;
                }
            }
        }

        public double Top
        {
            get => field;
            set
            {
                field = value;
                Canvas.SetTop(this, value);
                foreach (var item in inputs)
                {
                    item.BindPoint.Y = value;
                }
                foreach (var item in outputs)
                {
                    item.BindPoint.Y = value;
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public BluePrintCanvas ParentCanvas;
        private SyntaxNode _syntax;
        private string Header = "header";

        public List<NodeSlot> inputs = [];
        public List<NodeSlot> outputs = [];

        public BluePrintNode(BluePrintCanvas canvas, SyntaxNode syntax)
        {
            this.InitializeComponent();
            this.CanDrag = true;
            this.ParentCanvas = canvas;
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
                    inputs = [.. methodDeclarationSyntax.ParameterList.Parameters.Select(n => NodeSlot.SyntaxTo(n, this))];
                    outputs = [NodeSlot.SyntaxTo(methodDeclarationSyntax.ReturnType, this)];
                }
            }
        }



        #region event handler

        private void BluePrintNode_DoubleTapped(object sender, Microsoft.UI.Xaml.Input.DoubleTappedRoutedEventArgs e)
        {
            ParentCanvas.DocumentVM.BreadcrumbItems.Add(_syntax);
            ParentCanvas.DocumentVM.CurrentNode = _syntax;
        }

        private void BluePrintNode_DragStarting(UIElement sender, DragStartingEventArgs e)
        {
            e.AllowedOperations = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Move;
            e.Data.Properties.Add("node", this);
        }

        #endregion event handler
    }
}
