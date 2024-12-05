using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CSharpBlueprint.WinUI3.Controls
{
    public sealed partial class BluePrintCanvas : UserControl
    {
        public BluePrintCanvas()
        {
            this.InitializeComponent();
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
                    this.Canvas.Children.Add(new BluePrintNode(item));   
                }
                //this.Canvas.Children.Add(new TextBlock() { Text = "123" });
            }
        }

        public static readonly DependencyProperty NodesProperty =
            DependencyProperty.Register("Nodes", typeof(List<SyntaxNode>), typeof(BluePrintCanvas), new PropertyMetadata(null));

      

    }
}
