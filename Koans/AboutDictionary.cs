using System.Collections.Generic;
using System.Linq;
using DotNetKoans.Engine;
using Xunit;

namespace DotNetKoans.Koans;

public class AboutDictionary : Koan
{
	// A dictionary is a C# class.
	[Step(1)]
	public void DictionaryIsACSharpClass()
	{
		var dict = new Dictionary<string, string>();
		dict.Add("Key", "Value");
		var firstElement = dict.First();

		Assert.Equal("Key", firstElement.Key); // Key
		Assert.Equal("Value", firstElement.Value); // Value
	}

	//Pass keys to get their values.
	[Step(2)]
	public void UsingDictionaryKeysToGetValues()
	{
		var dict = new Dictionary<string, string>();
		dict.Add("Bruce", "Wayne");
		dict.Add("United Kingdom", "London");
		dict.Add("Poland", "Warsaw");
		dict.Add("Japan", "Tokyo");

		var key = "Japan";
		Assert.Equal("Tokyo", dict[key]); // What is the value?            
	}

	//Check if a key exists in Dictionary.
	[Step(3)]
	public void CheckIfKeyExists()
	{
		var dict = new Dictionary<string, string>();
		dict.Add("Bruce", "Wayne");
		dict.Add("United Kingdom", "London");
		dict.Add("Poland", "Warsaw");
		dict.Add("Japan", "Tokyo");

		var key = "Bruce";
		Assert.True(dict.ContainsKey(key)); // How to make this statement true?          
	}

	//Check if a value exists in Dictionary.
	[Step(4)]
	public void CheckIfValueExists()
	{
		var dict = new Dictionary<string, string>();
		dict.Add("Bruce", "Wayne");
		dict.Add("United Kingdom", "London");
		dict.Add("Poland", "Warsaw");
		dict.Add("Japan", "Tokyo");

		var val = "Wayne";
		Assert.True(dict.ContainsValue(val)); // How to make this statement true?          
	}

	//Update the value of a key in dictionary.
	[Step(5)]
	public void UpdateValueOfKey()
	{
		var dict = new Dictionary<string, string>();
		dict.Add("Bruce", "Wayne");
		dict.Add("United Kingdom", "London");
		dict.Add("Poland", "Warsaw");
		dict.Add("Japan", "Tokyo");
		dict.Add("India", "Mumbai");

		var key = "India";
		var expectedValue = "New Delhi";

		//May be you should update this
		dict[key] = expectedValue;

		Assert.Equal(expectedValue, dict[key]); // How to make this statement true?          
	}

	//Remove a key from dictionary and check its value.
	[Step(6)]
	public void RemoveKeyAndCheckIfItExists()
	{
		var dict = new Dictionary<string, string>();
		dict.Add("Bruce", "Wayne");
		dict.Add("United Kingdom", "London");
		dict.Add("Poland", "Warsaw");
		dict.Add("Japan", "Tokyo");
		dict.Add("India", "Mumbai");

		var keyToRemove = "Bruce";

		if (dict.ContainsKey(keyToRemove))
			dict.Remove(keyToRemove);
            
		Assert.False(dict.ContainsKey(keyToRemove)); // How to make this statement true?          
	}

}