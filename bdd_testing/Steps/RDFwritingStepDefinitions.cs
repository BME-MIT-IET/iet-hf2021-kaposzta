using TechTalk.SpecFlow;
using FluentAssertions;
using VDS.RDF;
using System;
using VDS.RDF.Writing;
using System.IO;

namespace bdd_testing.Steps
{
    [Binding]
    public sealed class RDFwritingStepDefinitions
    {
        private Graph g;


        [Given(@"Graph g is created")]
        public void GivenGraphGIsCreated()
        {
             g = new Graph();
        }

      

        [When(@"User add ""(.*)"" string to the graph")]
        public void WhenUserAddStringToTheGraph(string p0)
        {
            IUriNode dotNetRDF = g.CreateUriNode(UriFactory.Create("http://www.dotnetrdf.org"));
            IUriNode says = g.CreateUriNode(UriFactory.Create("http://example.org/says"));
            ILiteralNode helloWorld = g.CreateLiteralNode(p0);
            g.Assert(new Triple(dotNetRDF, says, helloWorld));
        }

        [When(@"Graph g is saved to ""(.*)"" file")]
        public void WhenGraphGIsSavedToFile(string p0)
        {
            RdfXmlWriter rdfxmlwriter = new RdfXmlWriter();
            rdfxmlwriter.Save(g, p0);
        }

        [Then(@"""(.*)"" file should be created")]
        public void ThenFileShouldBeCreated(string p0)
        {
            bool exists = File.Exists(p0);
            exists.Should().BeTrue();
        }

    }
}
