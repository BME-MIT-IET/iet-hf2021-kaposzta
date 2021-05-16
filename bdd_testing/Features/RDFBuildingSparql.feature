Feature: RDFBuildingSparql
 We want to "build" Sparql query using dotNetRDF

	Scenario: Query filters given names based on a case-insensitive regex comparison.
	Given I want to build a query wihtout hard coding the command in a string
	When Add Namespaces "ecrm" "http://erlangen-crm.org/current/" , "rdfs" "http://www.w3.org/2000/01/rdf-schema#"
	And set predicate "ecrm:E39_Actor"
	And filter for name "giovanni"
	Then should built next 
	"""
	PREFIX ecrm: <http://erlangen-crm.org/current/>
	PREFIX rdfs: <http://www.w3.org/2000/01/rdf-schema#>

	SELECT ?givenName WHERE
	{ 
	  ?y ecrm:E39_Actor ?givenName . 
	  FILTER(REGEX(?givenName,"giovanni","i")) 
	}

	"""