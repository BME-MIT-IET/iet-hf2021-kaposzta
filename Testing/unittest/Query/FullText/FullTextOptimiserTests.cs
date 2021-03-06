/*
dotNetRDF is free and open source software licensed under the MIT License

-----------------------------------------------------------------------------

Copyright (c) 2009-2012 dotNetRDF Project (dotnetrdf-developer@lists.sf.net)

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is furnished
to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

#if !NO_FULLTEXT

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Query.Algebra;
using VDS.RDF.Query.FullText.Search;
using VDS.RDF.Query.FullText.Search.Lucene;
using VDS.RDF.Query.Optimisation;
using VDS.RDF.Writing.Formatting;

namespace VDS.RDF.Query.FullText
{
    [Trait("category", "explicit")]
    [Trait("category", "fulltext")]
    public class FullTextOptimiserTests
    {
        private SparqlQueryParser _parser = new SparqlQueryParser();
        private List<IAlgebraOptimiser> _optimisers;
        private SparqlFormatter _formatter = new SparqlFormatter();

        public FullTextOptimiserTests()
        {
            Options.AlgebraOptimisation = true;
            Options.QueryOptimisation = true;
        }

        private SparqlQuery TestOptimisation(String query)
        {
            query = "PREFIX pf: <http://jena.hpl.hp.com/ARQ/property#>\n" + query;
            SparqlQuery q = this._parser.ParseFromString(query);
            Console.WriteLine(this._formatter.Format(q));

            Console.WriteLine("Normal Algebra: " + q.ToAlgebra().ToString());

            if (this._optimisers == null)
            {
                this._optimisers = new List<IAlgebraOptimiser>()
                {
                    new StrictAlgebraOptimiser(),
                    new FullTextOptimiser(new MockSearchProvider())
                };
            }
            q.AlgebraOptimisers = this._optimisers;
            Options.AlgebraOptimisation = true;

            String algebra = q.ToAlgebra().ToString();
            Console.WriteLine("Optimised Algebra: " + algebra);
            Assert.True(algebra.Contains("FullTextQuery("), "Optimised Algebra should use FullTextQuery operator");
            Assert.True(algebra.Contains("PropertyFunction("), "Optimised Algebra should use PropertyFunction operator");

            return q;
        }
    }

    class MockSearchProvider
        : IFullTextSearchProvider
    {

        #region IFullTextSearchProvider Members

        public IEnumerable<IFullTextSearchResult> Match(string text, double scoreThreshold, int limit)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IFullTextSearchResult> Match(string text, double scoreThreshold)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IFullTextSearchResult> Match(string text, int limit)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IFullTextSearchResult> Match(string text)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IFullTextSearchResult> Match(IEnumerable<Uri> graphUris, string text, double scoreThreshold, int limit)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IFullTextSearchResult> Match(IEnumerable<Uri> graphUris, string text, double scoreThreshold)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IFullTextSearchResult> Match(IEnumerable<Uri> graphUris, string text, int limit)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IFullTextSearchResult> Match(IEnumerable<Uri> graphUris, string text)
        {
            throw new NotImplementedException();
        }

        public bool IsAutoSynced
        {
            get
            {
                return true;
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
#endif