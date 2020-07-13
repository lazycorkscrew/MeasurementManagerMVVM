using MeasurementManagerMVVM.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MeasurementManagerMVVM.ViewModels
{
    class ViewModel : INotifyPropertyChanged
    {
        public ViewModel()
        {
            TownDateLimitsCollection = new ObservableCollection<TownDateLimits>(TownDateLimits.GetTestLimits());
            MeasuringRequestsCollection = new ObservableCollection<MeasuringRequest>(MeasuringRequest.GetMeasurings());
            CurrentMeasuringRequestsCollection = MeasuringRequestsCollection;
        }

        #region события
            public event PropertyChangedEventHandler PropertyChanged;
            public void OnPropertyChanged([CallerMemberName] string prop = "")
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            }
        #endregion

        #region Кнопки

        public bool IsAddCommandEnabled //Можно ли назначить замер на текущую дату и время для текущего заказа
        {
            get
            {
                return SelectedDate!= null & SelectedMeasuring!= null & SelectedTownDateLimit != null & SelectedTownDateLimit?.MeasurementsLimit>0;
            }
        }

        public bool IsCalendarEnabled //Нужно ли включать календарь
        {
            get
            {
                return SelectedMeasuring != null;
            }
        }

        public bool IsCancelCommandEnabled ////Можно ли отменить замер для текущего заказа
        {
            get
            {
                return SelectedMeasuring?.Appointed != null;
            }
        }

        private RelayCommand addCommand;
        public RelayCommand AddCommand => addCommand ?? (addCommand = new RelayCommand(obj =>
        {
            if(SelectedMeasuring == null)
            {
                MessageBox.Show("Заказ для замера не выделен.","Внимание");
                return;
            }

            if(SelectedMeasuring.Appointed == null)
            {
                SelectedMeasuring.Appoint(SelectedDate.AddHours(SelectedTownDateLimit.Interval.HourBegin), SelectedTownDateLimit);
                //SelectedMeasuring.Appointed = SelectedDate.AddHours(SelectedTownDateLimit.Interval.HourBegin);
                //SelectedTownDateLimit.MeasurementsLimit--;
                OnPropertyChanged("SelectedTownDateLimitsCollection");
                OnPropertyChanged("TownDateLimitsCollection");
                OnPropertyChanged("IsCancelCommandEnabled");
                OnPropertyChanged("IsAddCommandEnabled");
            }
            else
            {
                if(MessageBox.Show("Для данного заказа уже назначена дата и время. Переназначить?","Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    SelectedMeasuring.CancelAppoint();
                    SelectedMeasuring.Appoint(SelectedDate.AddHours(SelectedTownDateLimit.Interval.HourBegin), SelectedTownDateLimit);
                    OnPropertyChanged("SelectedTownDateLimitsCollection");
                    OnPropertyChanged("TownDateLimitsCollection");
                    OnPropertyChanged("IsCancelCommandEnabled");
                    OnPropertyChanged("IsAddCommandEnabled");
                }
            }
        }));

        private RelayCommand cancelCommand;
        public RelayCommand CancelCommand => cancelCommand ?? (cancelCommand = new RelayCommand(obj =>
        {
            if (SelectedMeasuring.Appointed != null)
            {
                SelectedMeasuring.CancelAppoint();
                OnPropertyChanged("SelectedTownDateLimitsCollection");
                OnPropertyChanged("TownDateLimitsCollection");
                OnPropertyChanged("IsCancelCommandEnabled");
                OnPropertyChanged("IsAddCommandEnabled");
            }
            else
            {
                MessageBox.Show("Для данного заказа дата и время не назначены", "Внимание");
                
            }
        }));

        #endregion

        private ObservableCollection<TownDateLimits> _townDateLimitsCollection;


        public ObservableCollection<TownDateLimits> TownDateLimitsCollection 
        {
            get
            {
                return _townDateLimitsCollection;
            }
            set
            {
                _townDateLimitsCollection = value;
                OnPropertyChanged("TownDateLimitsCollection");
            }
        }

        private string _filterText;
        public string FilterText
        {
            get
            {
                return _filterText;
            }
            set
            {
                _filterText = value;
                if (_filterText == string.Empty)
                {
                    CurrentMeasuringRequestsCollection = MeasuringRequestsCollection;
                }
                else
                {
                    CurrentMeasuringRequestsCollection = new ObservableCollection<MeasuringRequest>
                        (from mrc in MeasuringRequestsCollection
                        where mrc.AddressString.Contains(_filterText) || mrc.Fname.Contains(_filterText) || mrc.Lname.Contains(_filterText) ||
                        mrc.Patronymic.Contains(_filterText) || mrc.Phone.Contains(_filterText)
                        select mrc);
                }

                OnPropertyChanged("CurrentMeasuringRequestsCollection");
            }
        }

        private IntervalLimit _selectedTownDateLimit;
        public IntervalLimit SelectedTownDateLimit 
        { 
            get
            {
                return _selectedTownDateLimit;
            }
            set
            {
                _selectedTownDateLimit = value;
                OnPropertyChanged("IsAddCommandEnabled");
            }
        }

        public ObservableCollection<IntervalLimit> SelectedTownDateLimitsCollection
        {
            get
            {
                TownDateLimits townDateLimit = (from tdl in _townDateLimitsCollection.ToArray() 
                    where tdl.Date.Date == SelectedDate.Date &&
                    tdl.Town == SelectedMeasuring?.ClientAddress.Town
                    select tdl).FirstOrDefault();

                try
                {
                    return new ObservableCollection<IntervalLimit>(from l in townDateLimit?.Limits select l);
                }
                catch
                {
                    return null;
                }
            }
        }

        public ObservableCollection<MeasuringRequest> MeasuringRequestsCollection { get; set; }
        public ObservableCollection<MeasuringRequest> CurrentMeasuringRequestsCollection { get; set; }

        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get { return _selectedDate; } 
            set  
            { 
                _selectedDate = value;
                OnPropertyChanged("SelectedTownDateLimitsCollection"); 
                
            }
        }

        private MeasuringRequest _selectedMeasuring;
        public MeasuringRequest SelectedMeasuring
        {
            get { return _selectedMeasuring; }
            set 
            {
                _selectedMeasuring = value; 
                OnPropertyChanged("SelectedMeasuring"); 
                OnPropertyChanged("SelectedTownDateLimitsCollection");
                OnPropertyChanged("IsCalendarEnabled");
                OnPropertyChanged("IsCancelCommandEnabled");
                OnPropertyChanged("IsAddCommandEnabled");
            }
        }
    }
}
