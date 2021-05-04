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

        public Triple CreateTriplesForAssert(Triple t, IGraph g, bool keepOriginalGraphUri, Dictionary<INode, IBlankNode> mapping, IGraph _g)
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




