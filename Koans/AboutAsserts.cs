using Xunit;
using DotNetKoans.Engine;

namespace DotNetKoans.Koans;

/// <summary>
/// Be accurate in Assert.True() and Assert.Equal() usage.
/// </summary>
public class AboutAsserts : Koan
{
	/// <summary>
	/// We shall contemplate truth by testing reality, via asserts.
	/// </summary>
	[Step(1)]
	public void AssertTruth()
	{
		Assert.True(1 == 1);
	}

	/// <summary>
	/// Enlightenment may be more easily achieved with appropriate messages.
	/// </summary>
	[Step(2)]
	public void AssertTruthWithMessage()
	{
		Assert.True("a" == "a", "This is be true");
	}

	/// <summary>
	/// To understand reality, we must compare our expectations against reality.
	/// </summary>
	[Step(3)]
	public void AssertEquality()
	{
		var expectedValue = 3;
		var actualValue = 1 + 1 + 1;
		Assert.True(expectedValue == actualValue);
	}

	/// <summary>
	/// Some ways of asserting equality are better than others.
	/// </summary>
	[Step(4)]
	public void ABetterWayOfAssertingEquality()
	{
		var expectedValue = 3;
		var actualValue = 1 + 1;
		Assert.Equal(expectedValue, actualValue + 1);
	}

	/// <summary>
	/// Sometimes we will ask you to fill in the values.
	/// </summary>
	[Step(5)]
	public void FillInValues()
	{
		Assert.Equal(2, 1 + 1);
	}
}