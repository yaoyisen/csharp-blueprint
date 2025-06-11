using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpBlueprint.WinUI3.Controls
{
    public static class Utils
    {
        public static string GetSyntaxNodeName(this Microsoft.CodeAnalysis.SyntaxNode syntaxNode)
        {
            if (syntaxNode is CompilationUnitSyntax compilationUnitSyntax)
            {
                return "Root";
            }
            else if (syntaxNode is NamespaceDeclarationSyntax namespaceDeclarationSyntax)
            {
                return $"Namespace {namespaceDeclarationSyntax.Name}";
            }
            else if (syntaxNode is UsingDirectiveSyntax usingDirectiveSyntax)
            {
                return $"Using {usingDirectiveSyntax.Name}";
            }
            else if (syntaxNode is EnumDeclarationSyntax enumDeclarationSyntax)
            {
                return $"Enum {enumDeclarationSyntax.Identifier}";
            }
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
