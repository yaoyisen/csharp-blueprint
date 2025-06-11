using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.UI.Xaml.Controls;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CSharpBlueprint.WinUI3.Controls.Slots
{
    public sealed partial class NodeSlot : UserControl
    {
        public readonly ObservablePoint BindPoint = new(0, 0);
        public readonly BluePrintNode ParentNode;

        public NodeSlot(BluePrintNode parentNode)
        {
            ParentNode = parentNode;


        }

        public static NodeSlot SyntaxTo(SyntaxNode syntax, BluePrintNode parentNode)
        {
            if (syntax is ParameterSyntax parameterSyntax)
            {
                return new NodeSlot(parentNode);
            }
            else if (syntax is TypeSyntax typeSyntax)
            {
                return new NodeSlot(parentNode);
            }
            else
            {
                return new NodeSlot(parentNode);
            }
        }
    }
}
