using Xunit;
using DataBaseManager.Model;
using DataBaseManager.Repository;
using System.Collections.Generic;
using System.Diagnostics;
using System.Collections;

namespace WPFClientDBTests
{
    public class DataRepositoryTests
    {
        [Fact]
        public void Test1()
        {

        }

        [Fact]
        public void ConvertationVarTest()
        {
            ArrayList list = new ArrayList();
            list = DataRepository.GetData("");
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

            foreach (var run in runs)
            {
                var phys_OID = runToPhysicals.Find(f => f.Oidfrom == run.Oid);
                var phys = physicals.Find(f => phys_OID.Equals(f.Oid));
            }
        }

        [Fact]
        public void ConvertationFindTest()
        {
            ArrayList list = new ArrayList();
            list = DataRepository.GetData("");
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

            foreach (var run in runs)
            {
                var phys_OID = runToPhysicals.Find(f => f.Oidfrom == run.Oid);
                var phys = physicals.Find(f => runToPhysicals.Find(f=>f.Oidfrom==run.Oid).Equals(f.Oid));
            }
        }


        [Fact]
        public void GetDataTest()
        {
            ArrayList list = new ArrayList();
            list = DataRepository.GetData("");
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
            foreach (PipingFluid fluid in fluids)
                Debug.WriteLine(fluid.Oid+
                                "\nFC: "+fluid.FluidCode+
                                "\nPrRating "+fluid.PressureRating);
            foreach (PipingPhysical physical in physicals)
                Debug.WriteLine(physical.Oid+
                                "\nRunLength: "+physical.RunLength+
                                "\nLineWeight: "+physical.LineWeight);
            Assert.Equal(4, fluids.Count);
            Assert.Equal(6,physicals.Count);
        }

        [Fact]
        public void GetPipingFluidsTest()
        {
            List<PipingFluid> pipingFluids = new List<PipingFluid>();
            pipingFluids = DataRepository.GetPipingFluids("");
            Debug.WriteLine(pipingFluids.Count);
            Trace.WriteLine(pipingFluids.Count);
            foreach(PipingFluid fluid in pipingFluids)
            System.Diagnostics.Debug.WriteLine( fluid.Oid+
                                                "\nFC: "+fluid.FluidCode+
                                                "\nPrRating: "+fluid.PressureRating+
                                                "\nTemp: "+fluid.Temp);
            Assert.Equal(4, pipingFluids.Count);
        }
        [Fact]
        public void UpdateDataTest()
        {
            Debug.WriteLine("Start test");
            DatabaseData data = new DatabaseData(@"C:\Projects\Csh\К собеседованию дотнет\Нефтехимпроект\test_db — копия.db3");
            Debug.WriteLine($"Data initialized. Sample: {data.Fluids[0].Oid}");

            List<PipingFluid> pipingFluids = new List<PipingFluid>();
            pipingFluids = data.Fluids;
            pipingFluids[0].Temp = 999.0;
            DataRepository.UpdateData(pipingFluids[0]);
        }

        [Fact]
        public void DeleteDataTest()
        {
            DatabaseData data = new DatabaseData(@"C:\Projects\Csh\К собеседованию дотнет\Нефтехимпроект\test_db — копия — копия.db3");
            PipingRun run = data.Runs[0];
            Debug.WriteLine("Got: " + run.Oid);
            Assert.True(DataRepository.RemoveRunData(run));
        }



        struct DatabaseData
        {
            public List<PipingFluid> Fluids = new List<PipingFluid>();
            public List<PipingPhysical> Physicals = new List<PipingPhysical>();
            public List<PipingRun> Runs = new List<PipingRun>();
            public List<RunToFluid> RunToFluids = new List<RunToFluid>();
            public List<RunToPhysical> RunToPhysicals = new List<RunToPhysical>();
            public DatabaseData(string connectionString)
            {
                ArrayList list = DataRepository.GetData(connectionString);
                Fluids = list[0] as List<PipingFluid>;
                Physicals = list[1] as List<PipingPhysical>;
                Runs = list[2] as List<PipingRun>;
                RunToFluids = list[3] as List<RunToFluid>;
                RunToPhysicals = list[4] as List<RunToPhysical>;
            }
        }

    }
}