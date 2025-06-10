using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpBlueprint.WinUI3.Controls
{
    public static class Utils
    {
        public static string GetSyntaxNodeName(this Microsoft.CodeAnalysis.SyntaxNode syntaxNode)
        {
            if (syntaxNode is ClassDeclarationSyntax classDeclarationSyntax)
            {
                return $"Class {classDeclarationSyntax.Identifier}";
            }
            else if (syntaxNode is MethodDeclarationSyntax methodDeclarationSyntax)
            {
                return methodDeclarationSyntax.Identifier.ToString();
            }
            else if (syntaxNode is PropertyDeclarationSyntax propertyDeclarationSyntax)
            {
                return propertyDeclarationSyntax.Identifier.ToString();
            }
            else if (syntaxNode is FieldDeclarationSyntax fieldDeclarationSyntax)
            {
                return fieldDeclarationSyntax.Declaration.Variables.FirstOrDefault()?.Identifier.ToString() ?? "Field";
            }
            return syntaxNode.ToString();
        }
    }
}
