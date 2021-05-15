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
    class RDFreadingStepDefinitions
    {
        private Graph g;

        [Given(@"graph g is created")]
        public void GivenGraphGIsCreated()
        {
            g = new Graph();
        }

        [When(@"we read file ""(.*)""")]
        public void WhenWeReadFile(string p0)
        {
            FileLoader.Load(g, p0);
        }

        [Then(@"graph g shouldn't be empty")]
        public void ThenGraphGShouldnTBeEmpty()
        {
            g.IsEmpty.Should().BeFalse();
        }
    }
}
