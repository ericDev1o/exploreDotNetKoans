using Xunit;
using System;
using System.Globalization;
using DotNetKoans.Engine;

namespace DotNetKoans.Koans;

/// <summary>
/// Note: This is one of the longest katas and, perhaps, one
/// of the most important. String behavior in .NET is not
/// always what you expect it to be, especially when it comes
/// to concatenation and newlines, and is one of the biggest
/// causes of memory leaks in .NET applications.
/// </summary>
public class AboutStrings : Koan
{
	[Step(1)]
	public void DoubleQuotedStringsAreStrings()
	{
		var str = "Hello, World";
		Assert.Equal(typeof(string), str.GetType());
	}

	[Step(2)]
	public void SingleQuotedStringsAreNotStrings()
	{
		var str = 'H';
		Assert.NotEqual(typeof(string), str.GetType());
	}

	[Step(3)]
	public void CreateAStringWhichContainsDoubleQuotes()
	{
		var str = "Hello, \"World\"";
		Assert.Equal(14, str.Length);
	}

	/// <summary>
	/// The @ symbol creates a 'verbatim string literal'.
	/// </summary>
	[Step(4)]
	public void AnotherWayToCreateAStringWhichContainsDoubleQuotes()
	{
		var str = @"Hello, ""World""";
		Assert.Equal(14, str.Length);
	}

	[Step(5)]
	public void VerbatimStringsCanHandleFlexibleQuoting()
	{
		var strA = @"Verbatim Strings can handle both ' and "" characters (when escaped)";
		var strB = "Verbatim Strings can handle both ' and \" characters (when escaped)";
		Assert.Equal(strA, strB);
	}

	/// <summary>
	/// Tip: What you create for the literal string will have to 
	/// escape the newline characters. For Windows, that would be
	///  \r\n. If you are on non-Windows, that would just be \n.
	/// We'll show a different way next.
    /// Make sure to use a literal string.
	/// Escaped characters in verbatim strings are covered later.
	/// For verbatim strings, the newline character used will depend on
	/// whether the source file uses a \r\n or a \n ending and they have
	/// to match the ones on the literal string.
	/// If you are using Visual Studio Code, you can see which line ending is
	/// in use at the bottom right of the screen.
	/// </summary>
	[Step(6)]
	public void VerbatimStringsCanHandleMultipleLinesToo()
	{
		var verbatimString = @"I
am a
broken line";
		
		//var literalString = string.Concat("I", "\r\n", "am a", "\r\n", "broken line");//Windows
		var literalString = string.Concat("I", "\n", "am a", "\n", "broken line");//Ubuntu
		Assert.Equal(literalString.Length, verbatimString.Length);
		Assert.Equal(literalString, verbatimString);
	}

	/// <summary>
    /// Since line endings are different on different platforms
	/// (\r\n for Windows, \n for Linux) you shouldn't just type in
	/// the hardcoded escape sequence. A much better way
	/// (We'll handle concatenation and better ways of that in a bit).
	/// </summary>
	[Step(7)]
	public void ACrossPlatformWayToHandleLineEndings()
	{
		var literalString = "I" + Environment.NewLine + "am a" + Environment.NewLine + "broken line";
		var verbatimString = @"I
am a
broken line";
		Assert.Equal(literalString, verbatimString);
	}

	[Step(8)]
	public void PlusWillConcatenateTwoStrings()
	{
		var str = "Hello, " + "World";
		Assert.Equal("Hello, World", str);
	}

	[Step(9)]
	public void PlusConcatenationWillNotModifyOriginalStrings()
	{
		var strA = "Hello, ";
		var strB = "World";
		var fullString = strA + strB;
		Assert.Equal("Hello, ", strA);
		Assert.Equal("World", strB);
	}

	[Step(10)]
	public void PlusEqualsWillModifyTheTargetString()
	{
		var strA = "Hello, ";
		var strB = "World";
		strA += strB;
		Assert.Equal("Hello, World", strA);
		Assert.Equal("World", strB);
	}

	/// <summary>
	/// So here's the thing. Concatenating strings is cool
	/// and all. But if you think you are modifying the original
	/// string, you'd be wrong. 
	/// What just happened? Well, the string concatenation actually
	/// takes strA and strB and creates a *new* string in memory
	/// that has the new value. It does *not* modify the original
	/// string. This is a very important point - if you do this kind
	/// of string concatenation in a tight loop, you'll use a lot of memory
	/// because the original string will hang around in memory until the
	/// garbage collector picks it up. Let's look at a better way
	/// when dealing with lots of concatenation.
	/// </summary>
	[Step(11)]
	public void StringsAreReallyImmutable()
	{
		var strA = "Hello, ";
		var originalString = strA;
		var strB = "World";
		strA += strB;
		
		Assert.Equal("Hello, ", originalString);
		Assert.False(Object.ReferenceEquals(strA, originalString));
	}

	/// <summary>
	/// Concatenating lots of strings is a Bad Idea(tm). If you need to do that, then consider StringBuilder.
	/// When doing lots and lots of concatenation in a loop, StringBuilder will be more efficient than concatenation using the +-operator.
	/// However, even in the above example simple concatenation would actually be more efficient.
	/// </summary>
	[Step(12)]
	public void ABetterWayToConcatenateLotsOfStrings()
	{
		var strBuilder = new System.Text.StringBuilder();
		strBuilder.Append("The ");
		strBuilder.Append("quick ");
		strBuilder.Append("brown ");
		strBuilder.Append("fox ");
		strBuilder.Append("jumped ");
		strBuilder.Append("over ");
		strBuilder.Append("the ");
		strBuilder.Append("lazy ");
		strBuilder.Append("dog.");
		var str = strBuilder.ToString();
		Assert.Equal("The quick brown fox jumped over the lazy dog.", str);
	}
	
	/// <summary>
	/// Note that string concatenation and interpolation is more efficient than string.Format.
	/// </summary>
	[Step(13)]
	public void YouCouldAlsoUseStringFormatToConcatenate()
	{
		var world = "World";
		var str = String.Format("Hello, {0}", world);
		Assert.Equal("Hello, World", str);
	}

	[Step(14)]
	public void AnyExpressionCanBeUsedInFormatString()
	{
		var str = String.Format("The square root of 9 is {0}", Math.Sqrt(9));
		Assert.Equal("The square root of 9 is 3", str);
	}

	/// <summary>
	/// You can modify the value inserted into the result.
	/// </summary>
	[Step(15)]
	public void StringsCanBePaddedToTheLeft()
	{
		// Arrange Act
		var str = string.Format("{0,3}", "x");
		var x = "x".PadLeft(3, ' ');
		// Assert
		Assert.Equal(x, str);
	}

	[Step(16)]
	public void StringsCanBePaddedToTheRight()
	{
		// Arrange Act
		var str = string.Format("{0,-3}", "x"); // Note the '-' sign to indicate left alignment
		var x = "x".PadRight(3, ' ');
		Assert.Equal(x, str);
	}

	[Step(17)]
	public void SeparatorsCanBeAdded()
	{
		var str = string.Format(CultureInfo.InvariantCulture, "{0:0,0.00}", 123456);
		/*Console.WriteLine(str);
		Console.ReadLine();*/
		//var strTrimmed = str.Trim();
		Assert.Equal("123,456.00", str);//Ubuntu24.04 default
		/*var str = string.Format("{0:n}", 123456)
		Assert.Equal("123,456.00", str);//Windows10 default*/
	}

	[Step(18)]
	public void CurrencyDesignatorsCanBeAdded()
	{
		string culture = CultureInfo.InvariantCulture.Name;
		/*Console.WriteLine($"The current culture is {CultureInfo.CurrentCulture.Name}");
		Console.ReadLine();*/
		CultureInfo.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
		/*Console.WriteLine($"The current culture is {CultureInfo.CurrentCulture.Name}");*/
		Assert.Equal("en-US", CultureInfo.CurrentCulture.Name);
		int i = 123456;
		var strC2 = i.ToString("C2");
		/*Console.WriteLine(strC2);
		Console.ReadLine();*/
		Assert.Equal("$123,456.00", strC2);

		//Windows10
		/*var str = string.Format("{0:c}", 123456);
		Assert.Equal("123,456.00 XDR", str);*/
	}

	[Step(19)]
	public void NumberOfDisplayedDecimalsCanBeControlled()
	{
		var str = string.Format("{0:.##}", 12.3456);
		Assert.Equal("12.35", str);
	}

	[Step(20)]
	public void MinimumNumberOfDisplayedDecimalsCanBeControlled()
	{
		var str = string.Format("{0:.00}", 12.3);
		Assert.Equal("12.30", str);
	}

	[Step(21)]
	public void BuiltInDateFormatters()
	{
		var str = string.Format("{0:t}", DateTime.Parse("12/16/2011 2:35:02 PM", CultureInfo.InvariantCulture));
		/*Console.WriteLine(str);
		Console.ReadLine();*/
		//Assert.Equal("14:35", str);//Windows10
		Assert.Equal("2:35 PM", str);
	}

	/// <summary>
	/// These are just a few of the formatters available. Dig some and you may find what you need.
	/// </summary>
	[Step(22)]
	public void CustomDateFormatters()
	{
		var str = string.Format("{0:HH:mm dd MMMM}", DateTime.Parse("12/16/2011 2:35:02 PM", CultureInfo.InvariantCulture));
		Assert.Equal("14:35 16 December", str);
	}

	[Step(23)]
	public void StringBuilderCanUseFormatAsWell()
	{
		var strBuilder = new System.Text.StringBuilder();
		strBuilder.AppendFormat("{0} {1} {2} {3} {4}", "The", "quick", "brown", "fox", "");
		strBuilder.AppendFormat("{0} {1} {2} {3}", "jumped", "over", "the", "");
		strBuilder.AppendFormat("{0} {1}.", "lazy", "dog");
		var str = strBuilder.ToString();
		Assert.Equal("The quick brown fox jumped over the lazy dog.", str);
	}

	[Step(24)]
	public void LiteralStringsInterpretsEscapeCharacters()
	{
		var str = "\n";
		Assert.Equal(1, str.Length);
	}

	[Step(25)]
	public void VerbatimStringsDoNotInterpretEscapeCharacters()
	{
		var str = @"\n";
		Assert.Equal(2, str.Length);
	}

	[Step(26)]
	public void VerbatimStringsStillDoNotInterpretEscapeCharacters()
	{
		var str = @"\\\";
		Assert.Equal(3, str.Length);
	}

	[Step(27)]
	public void YouCanGetASubstringFromAString()
	{
		var str = "Bacon, lettuce and tomato";
		Assert.Equal("tomato", str.Substring(19));
		Assert.Equal("let", str.Substring(7, 3));
	}

	[Step(28)]
	public void YouCanGetASingleCharacterFromAString()
	{
		var str = "Bacon, lettuce and tomato";
		Assert.Equal('B', str[0]);
	}

	[Step(29)]
	public void SingleCharactersAreRepresentedByIntegers()
	{
		Assert.Equal(97, 'a');
		Assert.Equal(98, 'b');
		Assert.True('b' == ('a' + 1));
	}

	[Step(30)]
	public void StringsCanBeSplit()
	{
		var str = "Sausage Egg Cheese";
		string[] words = str.Split();
		Assert.Equal(new[] { "Sausage", "Egg", "Cheese" }, words);
	}

	[Step(31)]
	public void StringsCanBeSplitUsingCharacters()
	{
		var str = "the:rain:in:spain";
		string[] words = str.Split(':');
		Assert.Equal(new[] { "the", "rain", "in", "spain" }, words);
	}

	/// <summary>
	/// A full treatment of regular expressions is beyond the scope
	/// of this tutorial. The book "Mastering Regular Expressions"
	/// is highly recommended to be on your bookshelf<
	/// </summary>
	[Step(32)]
	public void StringsCanBeSplitUsingRegularExpressions()
	{
		var str = "the:rain:in:spain";
		var regex = new System.Text.RegularExpressions.Regex(":");
		string[] words = regex.Split(str);
		Assert.Equal(new[] { "the", "rain", "in", "spain" }, words);
	}

	[Step(33)]
	public void YouCanInterpolateVariablesIntoAString()
	{
		var name = "John Doe";
		var age = 33;
		var str = $"Mr. {name} is {age} years old";
		Assert.Equal("Mr. John Doe is 33 years old", str);
	}
	
	[Step(34)]
	public void InterpolationSupportsFormatAsWell()
	{//to do
		var str = $"{DateTime.Parse("12/16/2011 2:35:02 PM", CultureInfo.CreateSpecificCulture("en-US")):G}";
		Assert.Equal("16/12/2011 14:35:02", str.ToString(CultureInfo.CreateSpecificCulture("fr-FR")));
	}
}