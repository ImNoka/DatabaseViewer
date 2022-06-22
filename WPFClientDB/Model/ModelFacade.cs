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
    /// <summary>
    /// Data model using Facade pattern for view.
    /// </summary>
    public class ModelFacade : INotifyPropertyChanged
    {

        internal PipingFluid PipingFluid { get; }
        internal PipingPhysical PipingPhysical { get; }
        internal PipingRun PipingRun { get; }

        /// <summary>
        /// Enum object. Used for SetField method.
        /// </summary>
        private enum EntityType
        {
            PipingFluid,
            PipingPhysical,
            PipingRun
        }

        // View datatable props.
        #region External params

        public string RunName
        {
            get { return PipingRun.RunName; }
            set
            {
                PipingRun.RunName = value;
                SetField(EntityType.PipingRun, value);
            }
        }
        public string? ItemTag
        {
            get { return PipingRun.ItemTag; }
            set
            {
                PipingRun.ItemTag = value;
                SetField(EntityType.PipingRun, value);
            }
        }
        public long Npd
        {
            get { return PipingRun.Npd; }
            set
            {
                PipingRun.Npd = value;
                SetField(EntityType.PipingRun, value);
            }
        }
        public double? RunLength
        {
            get { return PipingPhysical.RunLength; }
            set
            {
                PipingPhysical.RunLength = value;
                SetField(EntityType.PipingPhysical, value);
            }
        }
        public double? LineWeight
        {
            get { return PipingPhysical.LineWeight; }
            set
            {
                PipingPhysical.LineWeight = value;
                SetField(EntityType.PipingPhysical, value);
            }
        }
        public double? RunDiam
        {
            get { return PipingPhysical.RunDiam; }
            set 
            { 
                PipingPhysical.RunDiam = value;
                SetField(EntityType.PipingPhysical, value);     
            }
        }
        public double? PressureRating
        {
            get { return PipingFluid.PressureRating; }
            set 
            { 
                PipingFluid.PressureRating = value;
                SetField(EntityType.PipingFluid, value);
            }
        }
        public string? FluidCode
        {
            get { return PipingFluid.FluidCode; }
            set 
            { 
                PipingFluid.FluidCode = value;
                SetField(EntityType.PipingFluid, value);
            }
        }
        public double? Temp
        {
            get { return PipingFluid.Temp; }
            set 
            { 
                PipingFluid.Temp = value;
                SetField(EntityType.PipingFluid,value);
            }
        }
        #endregion


        /// <summary>
        /// Creates new ModelFacade object from entities.
        /// </summary>
        /// <param name="fluid">PipingFluid</param>
        /// <param name="physical">PipingPhysical</param>
        /// <param name="run">PipingRun</param>
        public ModelFacade(  PipingFluid fluid,
                                PipingPhysical physical,
                                PipingRun run)
        {
            PipingFluid = fluid;
            PipingPhysical = physical;
            PipingRun = run;
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

        /// <summary>
        /// Method used for implement PropertyChanged and
        /// initializing query to DataBaseManager.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">Chose target entity</param>
        /// <param name="value">New value</param>
        /// <param name="propertyName">Entity's property name</param>
        /// <returns></returns>
        private bool SetField<T>(EntityType type,
                                 T value, 
                                 [CallerMemberName] string propertyName = "")
        {
            
            switch (type)
            {
                case EntityType.PipingFluid:
                    if(DataService.UpdateData(PipingFluid))
                        OnPropertyChanged(propertyName);
                    break;
                case EntityType.PipingPhysical:
                    if(DataService.UpdateData(PipingPhysical))
                        OnPropertyChanged(propertyName);
                    break;
                case EntityType.PipingRun:
                    if(DataService.UpdateData(PipingRun))
                        OnPropertyChanged(propertyName);
                    break;
                default:
                    throw new Exception("Property change error.");
            }
            return true;
        }



    }
}
