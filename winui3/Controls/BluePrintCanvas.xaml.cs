using CSharpBlueprint.WinUI3.ViewModel;
using Microsoft.CodeAnalysis;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System.Collections.Generic;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CSharpBlueprint.WinUI3.Controls
{
    public sealed partial class BluePrintCanvas : UserControl
    {
        public BluePrintCanvas()
        {
            this.InitializeComponent();
            this.AllowDrop = true;
            this.DragOver += Canvas_DragOver;
        }


        public List<SyntaxNode> Nodes
        {
            get { return (List<SyntaxNode>)GetValue(NodesProperty); }
            set
            {
                SetValue(NodesProperty, value);
                Canvas.Children.Clear();
                foreach (var item in value)
                {
                    var a = new BluePrintNode(this, item);
                    Canvas.Children.Add(a);
                }
            }
        }

        public static readonly DependencyProperty NodesProperty =
            DependencyProperty.Register("Nodes", typeof(List<SyntaxNode>), typeof(BluePrintCanvas), new PropertyMetadata(null));


        public DocumentViewModel DocumentVM
        {
            get { return (DocumentViewModel)GetValue(DocumentVMProperty); }
            set { SetValue(DocumentVMProperty, value); }
        }

        public static readonly DependencyProperty DocumentVMProperty =
            DependencyProperty.Register("DocumentVM", typeof(DocumentViewModel), typeof(BluePrintCanvas), new PropertyMetadata(null));


        #region event handler

        private void BreadcrumbBar_ItemClicked(BreadcrumbBar sender, BreadcrumbBarItemClickedEventArgs args)
        {
            if (args.Item is SyntaxNode syntaxNode)
            {
                DocumentVM.CurrentNode = syntaxNode;
            }
        }

        private void Canvas_DragOver(object sender, DragEventArgs args)
        {
            if (args.DataView.Properties["node"] is UIElement node)
            {
                args.AcceptedOperation = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Move;
                args.DragUIOverride.IsContentVisible = false;
                var position = args.GetPosition(Canvas);
                Canvas.SetTop(node, position.Y);
                Canvas.SetLeft(node, position.X);
                args.Handled = true;
            }
        }

        #endregion event handler
    }
}
