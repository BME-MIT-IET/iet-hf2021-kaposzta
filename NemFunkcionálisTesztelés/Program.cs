using System;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Query.Datasets;
using VDS.RDF.Writing;
using VDS.RDF.Writing.Formatting;

namespace NemFunkcionálisTesztelés
{
    class Program
    {
        static void Main(string[] args)
        {
            //Testing the RDF library bigger and bigger numbers of RDF statements
            for (int i = 1; i < 20; i++)
            {
                int dbSize = 1;
                for (int y = 0; y < i; y++)
                {
                    dbSize *= 2;
                }
                PerfomanceTester tester = new PerfomanceTester();
                tester.loadData(dbSize);
                tester.readWriteTest();
                tester.QuerryTest();
                Console.WriteLine($"Test with {dbSize} Entities (time in millisec):\n" +
                    $"Saving Perfomance: \n" +
                    $"\tNT save: {tester.saveTimeNTsec}\n" +
                    $"\tRDFXML save: {tester.saveTimeRDFsec}\n" +
                    $"Loading Performance:\n" +
                    $"\tNT load: {tester.loadTimeNTsec}\n" +
                    $"\tRDF load: {tester.loadTimeRDFsec}\n" +
                    $"Querry Time: \n" +
                    $"\t time: {tester.querryTimesec}\n");
            }
        }
    }
}
