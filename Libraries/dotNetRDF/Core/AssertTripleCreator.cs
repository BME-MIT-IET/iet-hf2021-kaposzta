// dotNetRDF is free and open source software licensed under the MIT License
//
// -----------------------------------------------------------------------------
//
// Copyright (c) [InvalidReference] dotNetRDF Project
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is furnished
// to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using VDS.RDF.Parsing;
using VDS.RDF.Writing.Serialization;

namespace VDS.RDF
{
    //create triples for assert
    public class AssertTripleCreator
    {
        public Triple CreateTriplesForAssert(Triple t, bool keepOriginalGraphUri, Dictionary<INode, IBlankNode> mapping, IGraph _g)
        {
            INode s, p, o;
            if (t.Subject.NodeType == NodeType.Blank)
            {
                if (!mapping.ContainsKey(t.Subject))
                {
                    IBlankNode temp = _g.CreateBlankNode();
                    if (keepOriginalGraphUri) temp.GraphUri = t.Subject.GraphUri;
                    mapping.Add(t.Subject, temp);
                }
                s = mapping[t.Subject];
            }
            else
            {
                s = Tools.CopyNode(t.Subject, _g, keepOriginalGraphUri);
            }

            if (t.Predicate.NodeType == NodeType.Blank)
            {
                if (!mapping.ContainsKey(t.Predicate))
                {
                    IBlankNode temp = _g.CreateBlankNode();
                    if (keepOriginalGraphUri) temp.GraphUri = t.Predicate.GraphUri;
                    mapping.Add(t.Predicate, temp);
                }
                p = mapping[t.Predicate];
            }
            else
            {
                p = Tools.CopyNode(t.Predicate, _g, keepOriginalGraphUri);
            }

            if (t.Object.NodeType == NodeType.Blank)
            {
                if (!mapping.ContainsKey(t.Object))
                {
                    IBlankNode temp = _g.CreateBlankNode();
                    if (keepOriginalGraphUri) temp.GraphUri = t.Object.GraphUri;
                    mapping.Add(t.Object, temp);
                }
                o = mapping[t.Object];
            }
            else
            {
                o = Tools.CopyNode(t.Object, _g, keepOriginalGraphUri);
            }

            return new Triple(s, p, o);
        }
    }


}




