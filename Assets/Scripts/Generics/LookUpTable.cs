using System.Collections;
using System.Collections.Generic;

public class LookUpTable<Tref,Tget>  {

	private Dictionary<Tref, Tget> table;
	public delegate Tget FactoryMethod(Tref value);
	private FactoryMethod factory;

	public LookUpTable(FactoryMethod factory) {
		table = new Dictionary<Tref, Tget>();
		this.factory = factory;
	}

	public Tget Get(Tref value) {
		if (table.ContainsKey(value))
			return table[value];
		return factory(value);
	}
	
}
