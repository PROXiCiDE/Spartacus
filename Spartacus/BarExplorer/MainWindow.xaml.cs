using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using BarExplorer.Properties;
using Microsoft.Win32;
using ProjectCeleste.GameFiles.Tools.Bar;
using SpartacusUtils.Bar;
using SpartacusUtils.Helpers;
using SpartacusUtils.Utilities;

namespace BarExplorer
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ICollectionView _barCollectionView1;
        private ObservableCollection<BarFileEntry> _barFileContents;
        private bool _isTextSearchEnabled;
        private string _textSearchFilter;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            _isTextSearchEnabled = false;
        }

        public ObservableCollection<BarFileEntry> BarFileContents
        {
            get => _barFileContents;
            set
            {
                _barFileContents = value;
                if (_barFileContents.Any())
                    IsTextSearchEnabled = true;
                OnPropertyChanged(nameof(BarFileContents));
            }
        }

        public string TextSearchFilter
        {
            get => _textSearchFilter;
            set
            {
                _textSearchFilter = value;
                BarCollectionView.Refresh();
                OnPropertyChanged(nameof(TextSearchFilter));
            }
        }

        //TODO: Make this work
        public bool IsTextSearchEnabled
        {
            get => _isTextSearchEnabled;
            set
            {
                _isTextSearchEnabled = value;
                OnPropertyChanged(nameof(IsTextSearchEnabled));
            }
        }

        public ICollectionView BarCollectionView
        {
            get => _barCollectionView1;
            private set
            {
                _barCollectionView1 = value;
                OnPropertyChanged(nameof(BarCollectionView));
            }
        }

        public static ObservableCollection<BarFileEntry> GetBarFileEntities(string filename)
        {
            var collection = new ObservableCollection<BarFileEntry>();
            var barFile = new BarFileReader(filename);
            barFile.GetEntries().ForEach(x => { collection.Add(new BarFileEntry(x)); });
            return collection;
        }

        private void OpenBarFile(string filename)
        {
            BarFileContents = GetBarFileEntities(filename);
            if (BarFileContents != null && BarFileContents.Any())
            {
                BarCollectionView = CollectionViewSource.GetDefaultView(BarFileContents);
                BarCollectionView.Filter = (obj) =>
                {
                    if (string.IsNullOrEmpty(TextSearchFilter))
                        return true;
                    return TextSearchFilter != null &&
                           obj is BarFileEntry entity &&
                           (bool)entity.FileName?.ToLower()?.Contains(TextSearchFilter.ToLower());
                };
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void MenuItemFileOpen_OnClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog()
            {
                Filter = "(*.bar)|*.bar"
            };

            var results = fileDialog.ShowDialog();
            if (results == true)
                OpenBarFile(fileDialog.FileName);
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}