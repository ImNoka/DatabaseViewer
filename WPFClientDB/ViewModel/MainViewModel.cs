using Microsoft.Win32;
using System.IO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClientDB.Model;
using WPFClientDB.Service;

namespace WPFClientDB.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        string connectionString;
        private string stateString;

        private ObservableCollection<ModelFacade> items;

        public string StateString
        {
            get { return stateString; }
            set 
            {   
                stateString = value;
                OnPropertyChanged("StateString");
            }
        }

        public ObservableCollection<ModelFacade> Items
        {
            get { return items; }
            set 
            {       
                items = value; 
                OnPropertyChanged("Items"); 
            }
        }

        private RelayCommand openFile;
        private RelayCommand saveCSV;
        private RelayCommand callMessage;


        public RelayCommand OpenFile
        {
            get
            {
                return openFile
                  ?? (openFile = new RelayCommand((obj) =>
                  {
                      OpenFileDialog openFile = new OpenFileDialog();
                      openFile.Filter = "SQLite database files(*.db3)|*.db3";
                      openFile.Title = "Open database file";
                      
                      if(openFile.ShowDialog() == true)
                      {
                          connectionString = openFile.FileName;
                          Items = DataService.GetData(connectionString);
                          StateString = $"File opened: {openFile.FileName}";
                      }

                  }));
            }
        }

        public RelayCommand SaveCSV
        {
            get
            {
                return saveCSV
                    ?? (saveCSV = new RelayCommand((obj) =>
                    {
                        SaveFileDialog saveFile = new SaveFileDialog();
                        saveFile.Filter = "CSV(*.csv)|*.csv";
                        saveFile.Title = "Save CSV";
                        if(saveFile.ShowDialog() == true)
                        {
                            try
                            {
                                DataService.SaveToCSV(Items, saveFile.FileName);
                                StateString = $"Saved to: {saveFile.FileName}";
                            }
                            catch (ArgumentException ex)
                            {
                                StateString = $"Exception: {ex.Message}";
                            }
                        }
                    }));
            }
        }

        public RelayCommand CallMessage
        {
            get
            {
                return callMessage
                    ?? (callMessage = new RelayCommand((obj) =>
                    {

                    }));
            }
        }


        public MainViewModel()
        {
            //Items = DataService.GetData("");
        }


        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
