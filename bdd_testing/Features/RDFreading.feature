Feature: Reading RDF from file using dotNetRDF
	We want to load RDF graph from a file


Scenario: Reading RDF graph from file
	Given graph g is created
	When we read file "hello_world.rdf"
	Then graph g shouldn't be empty 
	
