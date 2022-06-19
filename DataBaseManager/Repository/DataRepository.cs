using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseManager.Model;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace DataBaseManager.Repository
{
    public static class DataRepository
    {
        public static ArrayList GetData(string connectionString)
        {
            ArrayList list = new ArrayList();
            List<PipingFluid> fluids = new List<PipingFluid>();
            List<PipingPhysical> physicals = new List<PipingPhysical>();
            List<PipingRun> runs = new List<PipingRun>();
            List<RunToFluid> runToFluids = new List<RunToFluid>();
            List<RunToPhysical> runToPhysical = new List<RunToPhysical>();

            using(test_dbContext context = new test_dbContext(connectionString))
            {
                fluids = context.PipingFluids.ToList();
                physicals = context.PipingPhysicals.ToList();
                runs = context.PipingRuns.ToList();
                runToFluids = context.RunToFluids.ToList();
                runToPhysical = context.RunToPhysicals.ToList();
            }
            list.Add(fluids);
            list.Add(physicals);
            list.Add(runs);
            list.Add(runToFluids);
            list.Add(runToPhysical);
            return list;

        }
        public static List<PipingFluid> GetPipingFluids(string connectionString)
        {
            
            List<PipingFluid> fluids = new List<PipingFluid>();
            using(test_dbContext context = new test_dbContext(connectionString))
            {
                context.PipingFluids.Load();
                fluids = context.PipingFluids.ToList();
            }
            return fluids;
        }

        public static List<PipingPhysical> GetPipingPhysicals(string connectionString)
        {
            throw new NotImplementedException();
        }
            

    }
}
