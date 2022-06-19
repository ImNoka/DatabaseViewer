using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseManager.Model;

namespace WPFClientDB.Model
{
    public class ModelFacade
    {

        private PipingFluid PipingFluid;
        private PipingPhysical PipingPhysical;
        private PipingRun PipingRun;
        //private RunToFluid RunToFluid { get; set; }
        //private RunToPhysical RunToPhysical { get; set; }

        #region External params

        public string RunName
        {
            get { return PipingRun.RunName; }
            set { PipingRun.RunName = value; }
        }
        public string? ItemTag
        {
            get { return PipingRun.ItemTag; }
            set { PipingRun.ItemTag = value; }
        }
        public long Npd
        {
            get { return PipingRun.Npd; }
            set { PipingRun.Npd = value; }
        }
        public double? RunLength
        {
            get { return PipingPhysical.RunLength; }
            set { PipingPhysical.RunLength = value; }
        }
        public double? LineWeight
        {
            get { return PipingPhysical.LineWeight; }
            set { PipingPhysical.LineWeight = value; }
        }
        public double? RunDiam
        {
            get { return PipingPhysical.RunDiam; }
            set { PipingPhysical.RunDiam = value; }
        }
        public double? PressureRating
        {
            get { return PipingFluid.PressureRating; }
            set { PipingFluid.PressureRating = value; }
        }
        public string? FluidCode
        {
            get { return PipingFluid.FluidCode; }
            set { PipingFluid.FluidCode = value; }
        }
        public double? Temp
        {
            get { return PipingFluid.Temp; }
            set { PipingFluid.Temp = value; }
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
    }
}
