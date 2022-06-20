using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataBaseManager.Model;
using WPFClientDB.Service;

namespace WPFClientDB.Model
{
    public class ModelFacade : INotifyPropertyChanged
    {

        private PipingFluid PipingFluid;
        private PipingPhysical PipingPhysical;
        private PipingRun PipingRun;

        private enum ParentType
        {
            PipingFluid,
            PipingPhysical,
            PipingRun
        }

        //private RunToFluid RunToFluid { get; set; }
        //private RunToPhysical RunToPhysical { get; set; }

        #region External params

        public string RunName
        {
            get { return PipingRun.RunName; }
            set
            {
                PipingRun.RunName = value;
                SetField(ParentType.PipingRun, value);
            }
        }
        public string? ItemTag
        {
            get { return PipingRun.ItemTag; }
            set
            {
                PipingRun.ItemTag = value;
                SetField(ParentType.PipingRun, value);
            }
        }
        public long Npd
        {
            get { return PipingRun.Npd; }
            set
            {
                PipingRun.Npd = value;
                SetField(ParentType.PipingRun, value);
            }
        }
        public double? RunLength
        {
            get { return PipingPhysical.RunLength; }
            set
            {
                PipingPhysical.RunLength = value;
                SetField(ParentType.PipingPhysical, value);
            }
        }
        public double? LineWeight
        {
            get { return PipingPhysical.LineWeight; }
            set
            {
                PipingPhysical.LineWeight = value;
                SetField(ParentType.PipingPhysical, value);
            }
        }
        public double? RunDiam
        {
            get { return PipingPhysical.RunDiam; }
            set 
            { 
                PipingPhysical.RunDiam = value;
                SetField(ParentType.PipingPhysical, value);     
            }
        }
        public double? PressureRating
        {
            get { return PipingFluid.PressureRating; }
            set 
            { 
                PipingFluid.PressureRating = value;
                SetField(ParentType.PipingFluid, value);
            }
        }
        public string? FluidCode
        {
            get { return PipingFluid.FluidCode; }
            set 
            { 
                PipingFluid.FluidCode = value;
                SetField(ParentType.PipingFluid, value);
            }
        }
        public double? Temp
        {
            get { return PipingFluid.Temp; }
            set 
            { 
                PipingFluid.Temp = value;
                SetField(ParentType.PipingFluid,value);
            }
        }
        #endregion

        public ModelFacade(  PipingFluid piping,
                                PipingPhysical physical,
                                PipingRun pipingRun)
        {
            PipingFluid = piping;
            PipingPhysical = physical;
            PipingRun = pipingRun;
            //RunToFluid = runToFluid;
            //RunToPhysical = runToPhysical;
        }


        public string ToCSVString()
        {
            string csv =    $"{RunName}," +
                            $"{ItemTag}," +
                            $"{Npd}," +
                            $"{RunLength}," +
                            $"{LineWeight}," +
                            $"{RunDiam}," +
                            $"{PressureRating}," +
                            $"{FluidCode}," +
                            $"{Temp}";
            return csv;
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        private bool SetField<T>(ParentType type,
                                 T value, 
                                 [CallerMemberName] string propertyName = "")
        {
            //if(EqualityComparer<T>.Default.Equals(field, value))return false;
            //field = value;
            OnPropertyChanged(propertyName);
            switch (type)
            {
                case ParentType.PipingFluid:
                    DataService.UpdateData(PipingFluid);
                    break;
                case ParentType.PipingPhysical:
                    DataService.UpdateData(PipingPhysical);
                    break;
                case ParentType.PipingRun:
                    DataService.UpdateData(PipingRun);
                    break;
            }
            return true;
        }



    }
}
