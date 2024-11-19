using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpBlueprint.WinUI3.ViewModel
{
    public partial interface ITreeViewItem
    {
        public string Name { get; }
        public IEnumerable<ITreeViewItem> Items { get; }
    }
}
