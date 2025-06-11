using Microsoft.CodeAnalysis;
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
            string code = "" +
                "class A {\n" +
                "   public int Foo(int a) \n" +
                "   {\n" +
                "       int c = 3;\n" +
                "       int b = 2;\n" +
                "       return a + b;\n" +
                "   }\n" +
                "}\n";
            var workspace = CreateEmpty();
            string projectName = "NewProject";
            var project = workspace.AddProject(
                ProjectInfo.Create(ProjectId.CreateNewId(), VersionStamp.Default, projectName, projectName, LanguageNames.CSharp));
            workspace.AddDocument(project.Id, "NewFile.cs", SourceText.From(code));
            return workspace;
        }
    }
}
