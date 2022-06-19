using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseManager.Model;
using WPFClientDB.Model;

namespace WPFClientDB.Service
{
    public static class DataFacadeConverter
    {
        public static ObservableCollection<ModelFacade> DataToFacade(ArrayList list)
        {
            ObservableCollection<ModelFacade> result = new ObservableCollection<ModelFacade>();
            
            List<PipingFluid> fluids = new List<PipingFluid>();
            fluids = list[0] as List<PipingFluid>;
            List<PipingPhysical> physicals = new List<PipingPhysical>();
            physicals = list[1] as List<PipingPhysical>;
            List<PipingRun> runs = new List<PipingRun>();
            runs = list[2] as List<PipingRun>;
            List<RunToFluid> runToFluids = new List<RunToFluid>();
            runToFluids = list[3] as List<RunToFluid>;
            List<RunToPhysical> runToPhysicals = new List<RunToPhysical>();
            runToPhysicals = list[4] as List<RunToPhysical>;

            foreach(var run in runs)
            {
                var phys_OID = runToPhysicals.Find(f=>f.Oidfrom==run.Oid).Oidto;
                var phys = physicals.Find(f => phys_OID.Equals(f.Oid));
                var flu_OID = runToFluids.Find(f => f.Oidfrom == run.Oid).Oidto;
                var flu = fluids.Find(f=>flu_OID.Equals(f.Oid));
                result.Add(new ModelFacade(flu,phys,run));
            }
            return result;
        }
    }
}
