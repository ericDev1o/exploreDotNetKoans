using Xunit;
using DotNetKoans.Engine;

namespace DotNetKoans.Koans;

/// <summary>
/// The bool type represents boolean logical quantities.
/// The only possible values of bool are true and false.
/// No standard conversions exists between bool and other types.
/// bool is a simple type and is a Alias of System.Boolean, these
/// can be used interchangeably.
/// </summary>
public class AboutBooleans : Koan
{
	/// <summary>
	/// true is true
	/// </summary>
	[Step(1)]
	public void TrueIsTreatedAsTrue()
	{
		Assert.True(1 == 1);
	}

	/// <summary>
	/// false is false
	/// </summary>
	[Step(2)]
	public void FalseIsTreatedAsFalse()
	{
		// false is false
		Assert.False("a" == "b");
	}

	/// <summary>
	/// true is not false
	/// </summary>
	[Step(3)]
	public void TrueIsNotFalse()
	{
		Assert.False(!true);
	}

	/// <summary>
	/// bool is a Alias of System.Boolean
	/// </summary>
	[Step(4)]
	public void BoolIsAReservedWordOfSystemBoolean()
	{
		Assert.Equal(typeof(System.Boolean), typeof(bool));
	}

	/// <summary>
	/// no other type can cast to bool
	/// </summary>
	[Step(5)]
	public void NoOtherTypeConvertsToBool()
	{
		var otherTypes = new object[]
		{
			"not a bool",
			1, 0,
			null,
			new object[0]
		};

		foreach (var otherType in otherTypes)
		{
			Assert.False(otherType is bool);
		}
	}
}