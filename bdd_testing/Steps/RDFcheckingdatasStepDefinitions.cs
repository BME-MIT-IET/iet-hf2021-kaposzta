using TechTalk.SpecFlow;
using FluentAssertions;
using VDS.RDF;
using System;
using VDS.RDF.Writing;
using System.IO;
using VDS.RDF.Parsing;
using System.Collections.Generic;

namespace bdd_testing.Steps
{
    [Binding]
    class RDFcheckingdatasStepDefinitions
    {
        private Graph g;

        [Given(@"""(.*)"" files datas are loaded into graph g")]
        public void GivenFilesDatasAreLoadedIntoGraphG(string p0)
        {
            g = new Graph();
            FileLoader.Load(g, p0);
        }

        [When(@"writing datas of graph g")]
        public void WhenWritingDatasOfGraphG()
        {
            foreach (Triple t in g.Triples)
            {
                Console.WriteLine(t.ToString());
            }
        }

        [Then(@"triples related to graph g are shown")]
        public void ThenTriplesRelatedToGraphGAreShown()
        {
            NTriplesWriter nTriplesWriter = new NTriplesWriter(NTriplesSyntax.Original);
            String data = VDS.RDF.Writing.StringWriter.Write(g, nTriplesWriter);
           data.Should()
                .StartWith("<http://www.dotnetrdf.org/>")
                .And.Contain("Hello World")
                .And.Contain("<http://example.org/says>");
        }
    }
} 
