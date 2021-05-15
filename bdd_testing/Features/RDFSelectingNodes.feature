Feature: RDFSelectingNodes
	When we have a huge Graph we want to choose only the nodes we need

	Scenario: Geting one of the nodes of our RDF graph
		Given graph g is loaded
		When node "bdd_cool" is picked
		Then triples of "bdd_cool" nodes shown