# dotNetRDF

A projekt egy API-t biztosít RDF adatok tárolására és lekérdezésére .NET környezetben. Az adatok tárolásához három osztályt ad a projekt: `Triples`, `Graphs` and `Triple Stores`, viszont nincs közvetlen támogatása OWL-hez. A tárolás mellett biztosít adat lekérdezést SPARQL segítségével.

## Triples

Ezen keresztül lehet megadni a Subject, Predicate, Object hármast.

## Graphs

Egy RDF gráfot reprezentál. A csomópontok a következőkből állhatnak: `IBlankNode`, `ILiteralNod`, `IUriNode`, `IGraphLiteralNode`, `IVariableNode`.

## Triple store

Gráfok kollekciójának tárolására használható.