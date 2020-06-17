using MeasurementManagerMVVM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MeasurementManagerMVVM.ViewModels
{
    class ViewModel : DependencyObject, INotifyPropertyChanged
    {
        public ViewModel()
        {
            Items = CollectionViewSource.GetDefaultView(Measuring.GetMeasurings());
        }

        public string FilterText
        {
            get { return (string)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); }
        }

        public static readonly DependencyProperty FilterTextProperty =
            DependencyProperty.Register("FilterText", typeof(string), typeof(ViewModel), new PropertyMetadata(""));

        public Measuring SelectedItem
        {
            get { return (Measuring)GetValue(SelectedItemProperty); }
            set 
            { 
                SetValue(SelectedItemProperty, value);
                OnPropertyChanged("GetSelectedItemString");
            }
        }

        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(Measuring), typeof(Measuring), new PropertyMetadata(null));

        public string GetSelectedItemString
        {
            get {return SelectedItem?.Number ?? "(Заказ не выбран)"; }
        }

        public static readonly DependencyProperty GetSelectedItemStringProperty =
            DependencyProperty.Register("GetSelectedItemString", typeof(string), typeof(Measuring), new PropertyMetadata(""));

        public ICollectionView Items
        {
            get { return (ICollectionView)GetValue(ItemsProperty); }
            set 
            { 
                SetValue(ItemsProperty, value);
            }
        }

        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ICollectionView), typeof(ViewModel), new PropertyMetadata(null));

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
