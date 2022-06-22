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

        /// <summary>
        /// Creates new DataContext with file path and gets all data from tables.
        /// Forms new ArrayList of entities lists.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns>ArrayList, where:
        /// [0] - PipingFluid list,
        /// [1] - PipingPhysical list,
        /// [2] - PipingRun list,
        /// [3] - RunToFluid list,
        /// [4] - RunToPhysical list.</returns>
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
            list.Add(fluids);
            list.Add(physicals);
            list.Add(runs);
            list.Add(runToFluids);
            list.Add(runToPhysical);
            return list;

        }
        
        /// <summary>
        /// Creates update PipingFluid transaction to database without using tracking.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool UpdateData(PipingFluid obj)
        {
            var fluid = db.PipingFluids.FirstOrDefault(f=>f.Oid==obj.Oid);
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
        /// <summary>
        /// Creates update PipingPhysical transaction to database without using tracking.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Creates update PipingRun transaction to database without using tracking.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Creates delete transaction to database without using tracking.
        /// Deletes rows from PipingRun, RunToPhysical, RunToFluid tables.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool RemoveRunData(PipingRun obj)
        {
            var run = db.PipingRuns.FirstOrDefault(f=>f.Oid==obj.Oid);
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



        /// <summary>
        /// Test method.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        private static List<PipingFluid> GetPipingFluids(string connectionString)
        {

            List<PipingFluid> fluids = new List<PipingFluid>();
            using (test_dbContext context = new test_dbContext(connectionString))
            {
                context.PipingFluids.Load();
                fluids = context.PipingFluids.ToList();
            }
            return fluids;
        }
    }
}
