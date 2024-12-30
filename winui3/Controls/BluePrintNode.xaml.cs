using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace CSharpBlueprint.WinUI3.Controls
{

    public sealed partial class BluePrintNode : UserControl, INotifyPropertyChanged
    {
        public BluePrintNode(SyntaxNode syntax)
        {
            this.InitializeComponent();
            this.CanDrag = true;
            this.DragStarting += (s, e) =>
            {
                e.AllowedOperations = Windows.ApplicationModel.DataTransfer.DataPackageOperation.Move;
                e.Data.Properties.Add("node", this);
            };
            if (syntax is ClassDeclarationSyntax classDeclarationSyntax)
            {
                Header = classDeclarationSyntax.Identifier.ToString();
            }
        }

        private string Header = "header";

        public event PropertyChangedEventHandler? PropertyChanged;

    }
}
