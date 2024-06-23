
interface IItem : IEntity{
	/// <summary>
	/// Utilizes the item.
	/// 
	/// This method is called when the player picks up the item.
	/// </summary>
	/// <param name="level">The level scene.</param>
	/// <param name="player">The player entity.</param>
	void Utilize(LevelScene level, Player player);
}