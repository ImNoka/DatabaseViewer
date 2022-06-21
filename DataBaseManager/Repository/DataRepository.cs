using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseManager.Model;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Transactions;
using System.Collections;
using Microsoft.Data.Sqlite;

namespace DataBaseManager.Repository
{
    public static class DataRepository
    {
        private static test_dbContext db;
        private static List<PipingFluid> Fluids { get; set; }
        private static List<PipingPhysical> Physicals { get; set; }
        private static List<PipingRun> Runs { get; set; }
        private static List<RunToFluid> RunToFluids { get; set; }
        private static List<RunToPhysical> RunToPhysicals { get; set; }

        public static ArrayList GetData(string connectionString)
        {
            db = new test_dbContext(connectionString);
            ArrayList list = new ArrayList();
            List<PipingFluid> fluids = new List<PipingFluid>();
            List<PipingPhysical> physicals = new List<PipingPhysical>();
            List<PipingRun> runs = new List<PipingRun>();
            List<RunToFluid> runToFluids = new List<RunToFluid>();
            List<RunToPhysical> runToPhysical = new List<RunToPhysical>();
                fluids = db.PipingFluids.ToList();
                physicals = db.PipingPhysicals.ToList();
                runs = db.PipingRuns.ToList();
                runToFluids = db.RunToFluids.ToList();
                runToPhysical = db.RunToPhysicals.ToList();
                Fluids = fluids;
                Physicals = physicals;
                Runs = runs;
                RunToFluids = runToFluids;
                RunToPhysicals = runToPhysical;
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
            
        public static bool UpdateData(PipingFluid obj)
        {
            System.Diagnostics.Debug.WriteLine("Find fluid");
            var fluid = db.PipingFluids.FirstOrDefault(f=>f.Oid==obj.Oid);
            System.Diagnostics.Debug.WriteLine($"{fluid.Oid} found");
            if (fluid !=null)
            {
                fluid.FluidCode = obj.FluidCode;
                fluid.PressureRating = obj.PressureRating;
                fluid.Temp = obj.Temp;
                using(var dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {

                        db.Database.ExecuteSqlInterpolated($"UPDATE PipingFluid SET FluidCode={fluid.FluidCode}, PressureRating={fluid.PressureRating}, Temp={fluid.Temp} WHERE OID={fluid.Oid}");
                        dbTransaction.Commit();
                    }
                    catch (Exception ex)
                    { dbTransaction.Rollback(); return false; }
                }
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public static bool UpdateData(PipingPhysical obj)
        {
            var physical = db.PipingPhysicals.FirstOrDefault(f => f.Oid == obj.Oid);
            if (physical != null)
            {
                physical.RunLength = obj.RunLength;
                physical.LineWeight = obj.LineWeight;
                physical.RunDiam = obj.RunDiam;
                using (var dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {

                        db.Database.ExecuteSqlInterpolated($"UPDATE PipingPhysical SET RunLength={physical.RunLength}, LineWeight={physical.LineWeight}, RunDiam={physical.RunDiam} WHERE OID={physical.Oid}");
                        dbTransaction.Commit();
                    }
                    catch (Exception ex)
                    { dbTransaction.Rollback(); return false; }
                }
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public static bool UpdateData(PipingRun obj)
        {
            var run = db.PipingRuns.FirstOrDefault(f => f.Oid == obj.Oid);
            if (run != null)
            {
                run.RunName = obj.RunName;
                run.ItemTag = obj.ItemTag;
                run.Npd = obj.Npd;
                using (var dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {

                        db.Database.ExecuteSqlInterpolated($"UPDATE PipingRun SET RunName={run.RunName}, ItemTag={run.ItemTag} WHERE OID={run.Oid}");
                        dbTransaction.Commit();
                    }
                    catch (Exception ex)
                    { dbTransaction.Rollback(); return false; }
                }
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public static bool RemoveRunData(PipingRun obj)
        {
            var run = db.PipingRuns.FirstOrDefault(f=>f.Oid==obj.Oid);
            System.Diagnostics.Debug.WriteLine("Found: "+run.Oid);
            if (run!= null)
            {
                using(var dbTransaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.Database.ExecuteSqlInterpolated($"DELETE FROM PipingRun WHERE OID={run.Oid}");
                        db.Database.ExecuteSqlInterpolated($"DELETE FROM RunToPhysical WHERE OIDFrom={run.Oid}");
                        db.Database.ExecuteSqlInterpolated($"DELETE FROM RunToFluid WHERE OIDFrom={run.Oid}");
                        dbTransaction.Commit();
                    }
                    catch (Exception ex)
                    { 
                        dbTransaction.Rollback();
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                        return false;
                    }
                }
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
