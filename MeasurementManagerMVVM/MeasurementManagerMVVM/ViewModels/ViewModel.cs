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
using System.Linq;

namespace MeasurementManagerMVVM.ViewModels
{
    class ViewModel : INotifyPropertyChanged
    {
        public ViewModel()
        {
            TownDateLimitsCollection = new ObservableCollection<TownDateLimits>(TownDateLimits.GetTestLimits());
            MeasuringRequestsCollection = new ObservableCollection<MeasuringRequest>(MeasuringRequest.GetMeasurings());
        }

        #region события
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
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
            }
        }
    }
}
