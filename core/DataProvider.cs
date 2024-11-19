using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace CSharpBlueprint.Core
{
    public class DataProvider
    {
        public DataProvider()
        {
            
        }

        static public AdhocWorkspace CreateEmpty()
        {
            var workspace = new AdhocWorkspace();
            workspace.AddSolution(
               SolutionInfo.Create(SolutionId.CreateNewId(), VersionStamp.Default));
            return workspace;
        }

        static public AdhocWorkspace CreateEmptyWithTestData()
        {
            string code = "class A {}";
            var workspace = CreateEmpty();
            string projectName = "NewProject";
            var project = workspace.AddProject(
                ProjectInfo.Create(ProjectId.CreateNewId(), VersionStamp.Default, projectName, projectName, LanguageNames.CSharp));
            workspace.AddDocument(project.Id, "NewFile.cs", SourceText.From(code));
            return workspace;
        }
    }
}
