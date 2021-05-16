using TechTalk.SpecFlow;
using FluentAssertions;
using VDS.RDF;
using System;
using VDS.RDF.Writing;
using System.IO;
using VDS.RDF.Parsing;
using System.Collections.Generic;
using VDS.RDF.Query.Builder;
using VDS.RDF.Query;

namespace bdd_testing.Steps
{
    [Binding]
    class RDFBuildingSparqlStepDefinitions
    {
        private NamespaceMapper prefixes;
        private string predicateUri;
        private string name;

        [Given(@"I want to build a query wihtout hard coding the command in a string")]
        public void GivenIWantToBuildAQueryWihtoutHardCodingTheCommandInAString()
        {
            prefixes = new NamespaceMapper(true);
        }

        [When(@"Add Namespaces ""(.*)"" ""(.*)"" , ""(.*)"" ""(.*)""")]
        public void WhenAddNamespaces(string p0, string p1, string p2, string p3)
        {
            prefixes.AddNamespace(p0, new Uri(p1));
            prefixes.AddNamespace(p2, new Uri(p3));
        }


        [When(@"set predicate ""(.*)""")]
        public void WhenSetPredicate(string p0)
        {
            predicateUri = p0;
        }
        [When(@"filter for name ""(.*)""")]
        public void WhenFilterForName(string p0)
        {
            name = p0;
        }

        [Then(@"should built next")]
        public void ThenShouldBuiltNext(string multilineText)
        {
            var givenName = new SparqlVariable("givenName");
            var queryBuilder =
                QueryBuilder
                .Select(new SparqlVariable[] { givenName })
                .Where(
                    (triplePatternBuilder) =>
                    {
                        triplePatternBuilder
                            .Subject("y")
                            .PredicateUri(predicateUri)
                            .Object(givenName);
                    })
                .Filter((builder) => builder.Regex(builder.Variable("givenName"), name, "i"));
            queryBuilder.Prefixes = prefixes;

            queryBuilder.BuildQuery().ToString().Should().Contain(multilineText);
        }




    }
}
