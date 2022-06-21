using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseManager.Model;
using WPFClientDB.Model;
using DataBaseManager.Repository;

namespace WPFClientDB.Service
{
    public static class DataService
    {

        public static ObservableCollection<ModelFacade> GetData(string connectionString)
        {
            ArrayList itemList = new ArrayList();
            itemList = DataRepository.GetData(connectionString);
            ObservableCollection<ModelFacade> data = DataFacadeConverter.DataToFacade(itemList);
            return data;

        }

        public static bool SaveToCSV(ObservableCollection<ModelFacade> items, string path)
        {
            if(items==null||items.Count<=0)
                throw new ArgumentException("Nothing to save.");

            StringBuilder csv = new StringBuilder();
            foreach(ModelFacade item in items)
            {
                csv.AppendFormat(item.ToCSVString()+"\n", "U+002C");
            }
            using(StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(csv.ToString());
            }

            return true;
        }

        public static bool UpdateData(PipingFluid fluid)
        {
            if(fluid==null) return false;
            DataRepository.UpdateData(fluid);
            return true;
        }
        public static bool UpdateData(PipingPhysical physical)
        {
            if (physical == null) return false;
            DataRepository.UpdateData(physical);
            return true;
        }
        public static bool UpdateData(PipingRun run)
        {
            if (run == null) return false;
            DataRepository.UpdateData(run);
            return true;
        }

        public static bool RemoveRunData(ModelFacade facade)
        {
            if(facade.PipingRun==null) return false;
            DataRepository.RemoveRunData(facade.PipingRun);
            return true;
        }
    }
}
