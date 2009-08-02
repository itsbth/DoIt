using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows;
using SearchableCatalog;
using DoItLib;
using System.ComponentModel;


namespace WPFGui
{
    class DoItDataProvider : DependencyObject, INotifyPropertyChanged
    {

        private DoItManager _manager;

        public DoItDataProvider(DoItManager manager)
        {
            _manager = manager;
        }

        private static void OnSearchStringChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((DoItDataProvider)d).OnSearchStringChanged(e);
        }

        private void OnSearchStringChanged(DependencyPropertyChangedEventArgs e)
        {
            SearchResults = new ObservableCollection<CatalogItem>(_manager.Search((string)e.NewValue).Take(10));
            PropertyChanged(this, new PropertyChangedEventArgs("SearchString"));
        }

        public string SearchString
        {
            get { return (string)GetValue(SearchStringProperty); }
            set { SetValue(SearchStringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SearchString.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchStringProperty =
            DependencyProperty.Register("SearchString", typeof(string), typeof(DoItDataProvider),
            new UIPropertyMetadata("", new PropertyChangedCallback(OnSearchStringChanged)));

        public ObservableCollection<CatalogItem> SearchResults
        {
            get { return (ObservableCollection<CatalogItem>)GetValue(SearchResultsProperty); }
            set { SetValue(SearchResultsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SearchResults.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SearchResultsProperty =
            DependencyProperty.Register("SearchResults", typeof(ObservableCollection<CatalogItem>), typeof(DoItDataProvider), new UIPropertyMetadata(null));



        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
