using System.Collections.Generic;

namespace CSharpBlueprint.WinUI3.ViewModel
{
    public partial interface ITreeViewItem
    {
        public string Name { get; }
        public IEnumerable<ITreeViewItem> ChildItems { get; }
    }
}
