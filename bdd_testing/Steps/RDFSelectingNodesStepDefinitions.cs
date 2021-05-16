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
    class RDFSelectingNodesStepDefinitions
    {
        private Graph g;
        private IEnumerable<Triple> ts;
        [Given(@"graph g is loaded")]
        public void GivenGraphGIsLoaded()
        {
            g = new Graph();
            IUriNode dotNetRDF = g.CreateUriNode(UriFactory.Create("http://www.dotnetrdf.org"));
            IUriNode says = g.CreateUriNode(UriFactory.Create("http://example.org/says"));
            ILiteralNode helloWorld = g.CreateLiteralNode("Hello World");
            ILiteralNode bdd_testing = g.CreateLiteralNode("bdd_cool");

            g.Assert(new Triple(dotNetRDF, says, helloWorld));
            g.Assert(new Triple(dotNetRDF, says, bdd_testing));
        }

        [When(@"node ""(.*)"" is picked")]
        public void WhenNodeIsPicked(string p0)
        {
            ILiteralNode select = g.CreateLiteralNode(p0);
            ts = g.GetTriples(select);
        }

        [Then(@"triples of ""(.*)"" nodes shown")]
        public void ThenTriplesOfNodesShown(string p0)
        {
            NTriplesWriter nTriplesWriter = new NTriplesWriter(NTriplesSyntax.Original);
            String data = VDS.RDF.Writing.StringWriter.Write(g, nTriplesWriter);
            data.Should().Contain(p0);
        }
    }
}
