Feature: Using dotNetRDF to create "Hello World application"

Scenario: Writing RDF graph to file
Given Graph g is created
When User add "Hello World" string to the graph
And Graph g is saved to "hello_world.rdf" file
Then "hello_world.rdf" file should be created

Scenario: Checking RDF graph datas
	Given "hello_world.rdf" files datas are loaded into graph g
	When writing datas of graph g
	Then triples related to graph g are shown