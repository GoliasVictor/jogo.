using Raylib_cs;

/// <summary>
/// Represents the different elements that an entity can have.
/// </summary>
enum Element
{
	Neutral,
	Water,
	Leaf,
	Fire
}

/// <summary>
/// Provides extension methods for the Element enum.
/// </summary>
static class ElementExtension
{
	/// <summary>
	/// Determines if the current element wins against the specified element.
	/// </summary>
	/// <param name="A">The current element.</param>
	/// <param name="B">The element to compare against.</param>
	/// <returns>True if the current element wins against the specified element, otherwise false.</returns>
	public static bool WinAgainst(this Element A, Element B)
	{
		return (A, B) switch
		{
			(Element.Water, Element.Fire) => true,
			(Element.Leaf, Element.Water) => true,
			(Element.Fire, Element.Leaf) => true,
			_ => false
		};
	}

	/// <summary>
	/// Gets the color associated with the current element.
	/// </summary>
	/// <param name="element">The current element.</param>
	/// <returns>The color associated with the current element.</returns>
	public static Color GetColor(this Element element)
	{
		return element switch
		{
			Element.Neutral => Color.White,
			Element.Fire => Color.Red,
			Element.Leaf => Color.Green,
			Element.Water => Color.Blue,
			_ => Color.White
		};
	}
}