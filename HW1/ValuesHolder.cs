using System;
using System.Collections.Generic;
using HW1;

public class ValuesHolder
{
    public List<WeatherForecast> Values { get; set; } = new List<WeatherForecast>();
	public ValuesHolder()
	{
	}

    internal void Add(WeatherForecast input)
    {
        Values.Add(input);
    }

    internal object Get()
    {
        return Values;
    }
}
