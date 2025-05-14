using CSharpBlueprint.WinUI3.ViewModel;
using Microsoft.CodeAnalysis;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
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
            this.DragOver += (s, e) =>
            {
                if (e.DataView.Properties["node"] is UIElement node)
                {
                    e.AcceptedOperation = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Move;
                    e.DragUIOverride.IsContentVisible = false;
                    var position = e.GetPosition(this);
                    Canvas.SetTop(node, position.Y);
                    Canvas.SetLeft(node, position.X);
                    e.Handled = true;
                }
            };
        }

        private void BluePrintCanvas_DragEnter(object sender, DragEventArgs e)
        {
            throw new NotImplementedException();
        }

        public List<SyntaxNode> Nodes
        {
            get { return (List<SyntaxNode>)GetValue(NodesProperty); }
            set
            {
                SetValue(NodesProperty, value);
                this.Canvas.Children.Clear();
                foreach (var item in (List<SyntaxNode>)value)
                {
                    var a = new BluePrintNode(this, item);
                    this.Canvas.Children.Add(a);

                }
                //this.Canvas.Children.Add(new TextBlock() { Text = "123" });
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



    }
}
