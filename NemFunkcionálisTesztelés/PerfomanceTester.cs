using System;
using System.Collections.Generic;
using System.Text;
using System;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Query.Datasets;
using VDS.RDF.Writing;
using VDS.RDF.Writing.Formatting;
using System.Diagnostics;

namespace NemFunkcionálisTesztelés
{
    /// <summary>
    /// This is the class responsible for perfomance testing the dotNetRDF library
    /// </summary>
    class PerfomanceTester
    {
        // These attributes store data about the tests
        private IGraph graph;
        public long saveTimeRDFMilli
        { get; private set; }
        public long saveTimeNTMilli
        { get; private set; }
        public long loadTimeRDFMilli
        { get; private set; }
        public long loadTimeNTMilli
        { get; private set; }
        public long querryTimeMilli
        { get; private set; }

        public double saveTimeRDFsec
        { get { return (double)saveTimeRDFMilli / 1000000.0; } }
        public double saveTimeNTsec
        { get { return (double)saveTimeNTMilli / 1000000.0; } }
        public double loadTimeRDFsec
        { get { return (double)loadTimeRDFMilli / 1000000.0; } }
        public double loadTimeNTsec
        { get{ return (double)loadTimeNTMilli / 1000000.0; } }
        public double querryTimesec
        { get { return (double)querryTimeMilli / 1000000.0; } }
        public PerfomanceTester()
        {
            graph = new Graph();
        }
        
        /// <summary>
        /// Loading given number of entites with 2 rdf statements for each
        /// </summary>
        /// <param name="dataNum"></param>
        public void loadData(int dataNum)
        {
            IUriNode says = graph.CreateUriNode(UriFactory.Create("http://example.org/says"));
            IUriNode hasFriend = graph.CreateUriNode(UriFactory.Create("http://example.org/hasFriend"));
            IUriNode firstPerson = graph.CreateUriNode(UriFactory.Create("http://www.dotnetrdf.org/person" +  1));
            graph.Assert(new Triple(firstPerson, says, graph.CreateLiteralNode("Likes number: " +  1)));
            IUriNode prevPerson = firstPerson;
            for (int i = 1; i < dataNum; i++)
            {
                IUriNode person = graph.CreateUriNode(UriFactory.Create("http://www.dotnetrdf.org/person"+(i+1)));
                ILiteralNode likesNumber = graph.CreateLiteralNode("Likes number: " + (i+1));
                graph.Assert(new Triple(person, says, likesNumber));
                graph.Assert(new Triple(person, hasFriend, prevPerson));
                prevPerson = person;
            }
            graph.Assert(new Triple(firstPerson, hasFriend, prevPerson));
        }

        /// <summary>
        /// Testing how fast is the reading and writing from file
        /// </summary>
        public void readWriteTest()
        {

            Stopwatch s = Stopwatch.StartNew();

            NTriplesWriter ntwriter = new NTriplesWriter();

            s.Restart();
            ntwriter.Save(graph, "fileNT.nt");
            saveTimeNTMilli = s.ElapsedMilliseconds;
            


            RdfXmlWriter rdfxmlwriter = new RdfXmlWriter();

            s.Restart();
            rdfxmlwriter.Save(graph, "fileRDF.rdf");
            saveTimeRDFMilli = s.ElapsedMilliseconds;
            


            IGraph loadRDF = new Graph();
            IGraph loadNT = new Graph();
            try
            {
                s.Restart();
                FileLoader.Load(loadRDF, "fileRDF.rdf");
                loadTimeRDFMilli = s.ElapsedMilliseconds;
                

                NTriplesParser ntparser = new NTriplesParser();
                //Load using Filename
                s.Restart();
                ntparser.Load(loadNT, "fileNT.nt");
                loadTimeNTMilli = s.ElapsedMilliseconds;
            }
            catch (RdfParseException parseEx)
            {
                //This indicates a parser error e.g unexpected character, premature end of input, invalid syntax etc.
                Console.WriteLine("Parser Error");
                Console.WriteLine(parseEx.Message);
            }
            catch (RdfException rdfEx)
            {
                //This represents a RDF error e.g. illegal triple for the given syntax, undefined namespace
                Console.WriteLine("RDF Error");
                Console.WriteLine(rdfEx.Message);
            }
        }

        /// <summary>
        /// Testing how fast is the querrying
        /// </summary>
        public void QuerryTest()
        {
            Stopwatch s = Stopwatch.StartNew();

            TripleStore store = new TripleStore();
            store.Add(graph);
            SparqlQueryParser sparqlparser = new SparqlQueryParser();
            SparqlQuery query = sparqlparser.ParseFromString("CONSTRUCT { ?s ?p ?o } WHERE { { ?s ?p ?o } UNION { GRAPH ?g { ?s ?p ?o } } }");
            Object results = store.ExecuteQuery(query);
            if (results is IGraph)
            {
                //Print out the Results
                IGraph g5 = (IGraph)results;
                /*foreach (Triple t in g5.Triples)
                {
                    Console.WriteLine(t.ToString());
                }*/
                //Console.WriteLine("Query took " + query.QueryExecutionTime.ToString());
            }
            //Console.WriteLine(query.QueryExecutionTime.Value.TotalMilliseconds);
            querryTimeMilli = s.ElapsedMilliseconds;
        }


        public static void badTest()
        {
            IGraph g = new Graph();

            IUriNode dotNetRDF = g.CreateUriNode(UriFactory.Create("http://www.dotnetrdf.org"));
            IUriNode says = g.CreateUriNode(UriFactory.Create("http://example.org/says"));
            ILiteralNode helloWorld = g.CreateLiteralNode("Hello World");
            ILiteralNode bonjourMonde = g.CreateLiteralNode("Bonjour tout le Monde", "fr");

            for (int i = 0; i < 100000; i++)
            {
                g.Assert(new Triple(dotNetRDF, says, helloWorld));
                g.Assert(new Triple(dotNetRDF, says, bonjourMonde));
            }


            foreach (Triple t in g.Triples)
            {
                Console.WriteLine(t.ToString());
            }

            IGraph g2 = new Graph();
            IGraph g3 = new Graph();
            try
            {
                NTriplesWriter ntwriter = new NTriplesWriter();
                ntwriter.Save(g, "HelloWorld.nt");

                RdfXmlWriter rdfxmlwriter = new RdfXmlWriter();
                rdfxmlwriter.Save(g, "somefile.rdf");


                FileLoader.Load(g2, "somefile.rdf");


                NTriplesParser ntparser = new NTriplesParser();

                //Load using Filename
                ntparser.Load(g3, "HelloWorld.nt");
            }
            catch (RdfParseException parseEx)
            {
                //This indicates a parser error e.g unexpected character, premature end of input, invalid syntax etc.
                Console.WriteLine("Parser Error");
                Console.WriteLine(parseEx.Message);
            }
            catch (RdfException rdfEx)
            {
                //This represents a RDF error e.g. illegal triple for the given syntax, undefined namespace
                Console.WriteLine("RDF Error");
                Console.WriteLine(rdfEx.Message);
            }

            TripleStore store = new TripleStore();
            store.LoadFromFile("somefile.rdf");
            Object results = store.ExecuteQuery("SELECT * WHERE { { ?s ?p ?o } UNION { GRAPH ?g { ?s ?p ?o } } }");
            if (results is SparqlResultSet)
            {
                //Print out the Results
                SparqlResultSet rset = (SparqlResultSet)results;
                foreach (SparqlResult result in rset)
                {
                    Console.WriteLine(result.ToString());
                }
            }


            SparqlQueryParser sparqlparser = new SparqlQueryParser();
            SparqlQuery query = sparqlparser.ParseFromString("CONSTRUCT { ?s ?p ?o } WHERE { { ?s ?p ?o } UNION { GRAPH ?g { ?s ?p ?o } } }");
            results = store.ExecuteQuery(query);
            if (results is IGraph)
            {
                //Print out the Results
                IGraph g5 = (IGraph)results;
                foreach (Triple t in g5.Triples)
                {
                    Console.WriteLine(t.ToString());
                }
                Console.WriteLine("Query took " + query.QueryExecutionTime.ToString());
            }



            /*
            InMemoryDataset ds = new InMemoryDataset(store, new Uri("http://mydefaultgraph.org"));

            //Get the Query processor
            ISparqlQueryProcessor processor = new LeviathanQueryProcessor(ds);

            //Use the SparqlQueryParser to give us a SparqlQuery object
            //Should get a Graph back from a CONSTRUCT query
            SparqlQueryParser sparqlparser = new SparqlQueryParser();
            SparqlQuery query = sparqlparser.ParseFromString("CONSTRUCT { ?s ?p ?o } WHERE {?s ?p ?o}");
            results = processor.ProcessQuery(query);
            if (results is IGraph)
            {
                //Print out the Results
                IGraph g66 = (IGraph)results;
                NTriplesFormatter formatter = new NTriplesFormatter();
                foreach (Triple t in g66.Triples)
                {
                    Console.WriteLine(t.ToString(formatter));
                }
                Console.WriteLine("Query took " + query.QueryExecutionTime + " milliseconds");
            }*/
        }
    }
}
