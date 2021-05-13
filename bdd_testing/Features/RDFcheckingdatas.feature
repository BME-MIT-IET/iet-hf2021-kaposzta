Feature: RDFcheckingdatas
	We want to see  datas of our graph


Scenario: Checking RDF graph datas
	Given "hello_world.rdf" files datas are loaded into graph g
	When writing datas of graph g
	Then triples related to graph g are shown