using System;
using System.Collections.Generic;

public class ValuesHolder
{
    public List<string> Values { get; } = new List<string>();
	public ValuesHolder()
	{
	}

    internal void Add(string input)
    {
        Values.Add(input);
    }

    internal object Get()
    {
        return Values[0];
    }
}
