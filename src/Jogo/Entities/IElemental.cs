/// <summary>
/// Represents an elemental entity in the game.
/// </summary>
interface IElemental : IEntity {
	/// <summary>
	/// Gets the element of the entity.
	/// </summary>
	Element Element { get; }

	/// <summary>
	/// Determines if the current entity wins against the specified entity.
	/// </summary>
	/// <param name="entity">The entity to compare against.</param>
	/// <returns>True if the current entity wins, otherwise false.</returns>
	public bool WinAgainst(IElemental entity){
		return this.Element.WinAgainst(entity.Element);
	}
}