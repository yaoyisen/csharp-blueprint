using CSharpBlueprint.WinUI3.Controls.Slots;
using CSharpBlueprint.WinUI3.ViewModel;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CSharpBlueprint.WinUI3.Controls
{
    public sealed partial class BluePrintCanvas : UserControl
    {
        public BluePrintCanvas()
        {
            InitializeComponent();
            AllowDrop = true;
            DragOver += Canvas_DragOver;
        }

        public void ConnentSlot(NodeSlot s1, NodeSlot s2)
        {
            BindableBezierPath path = new(s1.BindPoint, s2.BindPoint);
            Canvas.Children.Add(path);
        }

        public SyntaxNode CurrentNode
        {
            get { return (SyntaxNode)GetValue(CurrentNodeProperty); }
            set
            {
                SetValue(CurrentNodeProperty, value);
                Canvas.Children.Clear();
                if (value is CompilationUnitSyntax compilationUnitSyntax)
                {
                    CompilationUnitSyntax(compilationUnitSyntax);
                }
                else if (value is ClassDeclarationSyntax classDeclarationSyntax)
                {
                    ClassDeclarationSyntax(classDeclarationSyntax);
                }
                else if (value is MethodDeclarationSyntax methodDeclarationSyntax)
                {
                    MethodDeclarationSyntax(methodDeclarationSyntax);
                }
            }
        }


        public static readonly DependencyProperty CurrentNodeProperty =
            DependencyProperty.Register("Nodes", typeof(SyntaxNode), typeof(BluePrintCanvas), new PropertyMetadata(null));


        public DocumentViewModel DocumentVM
        {
            get { return (DocumentViewModel)GetValue(DocumentVMProperty); }
            set { SetValue(DocumentVMProperty, value); }
        }

        public static readonly DependencyProperty DocumentVMProperty =
            DependencyProperty.Register("DocumentVM", typeof(DocumentViewModel), typeof(BluePrintCanvas), new PropertyMetadata(null));


        #region content generator

        private void CompilationUnitSyntax(CompilationUnitSyntax syntax)
        {
            double left = 0;
            foreach (MemberDeclarationSyntax member in syntax.Members)
            {
                var node = new BluePrintNode(this, member);
                node.Left += left += 20;
                Canvas.Children.Add(node);
            }
        }

        private void ClassDeclarationSyntax(ClassDeclarationSyntax syntax)
        {
            double left = 0;
            foreach (MemberDeclarationSyntax member in syntax.Members)
            {
                var node = new BluePrintNode(this, member);
                node.Left += left += 20;
                Canvas.Children.Add(node);
            }
        }

        private void MethodDeclarationSyntax(MethodDeclarationSyntax syntax)
        {
            double left = 0;
            foreach (var parameterSyntax in syntax.ParameterList.Parameters)
            {
                var node = new BluePrintNode(this, parameterSyntax);
                node.Left += left += 120;
                Canvas.Children.Add(node);
            }
            if (syntax.Body is not null)
            {
                BluePrintNode? last = null;
                foreach (var statementSyntax in syntax.Body.Statements)
                {
                    var node = new BluePrintNode(this, statementSyntax);
                    if (last != null)
                    {
                        NodeSlot lastSlot = new NodeSlot(last);
                        last.outputs.Add(lastSlot);
                        NodeSlot nodeSlot = new NodeSlot(node);
                        node.inputs.Add(nodeSlot);
                        ConnentSlot(lastSlot, nodeSlot);
                    }
                    last = node;
                    node.Left += left += 120;
                    Canvas.Children.Add(node);
                }
            }
            if (syntax.ReturnType is not null)
            {
                var node = new BluePrintNode(this, syntax.ReturnType);
                node.Left += left += 120;
                Canvas.Children.Add(node);
            }
        }




        #endregion content generator


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
            if (args.DataView.Properties["node"] is BluePrintNode node)
            {
                args.AcceptedOperation = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Move;
                args.DragUIOverride.IsContentVisible = false;
                var position = args.GetPosition(Canvas);
                node.Left = position.X;
                node.Top = position.Y;
                args.Handled = true;
            }
        }

        #endregion event handler
    }
}
