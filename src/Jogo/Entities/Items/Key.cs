using Raylib_cs;

/// <summary>
/// Represents a key item in the game.
/// The key can be utilized by the player to remove all door entities from the level map.
/// </summary>
class Key(GridVec2 position) : IItem
{
	public Layer Layer => Layer.Item;

	/// <summary>
	/// Gets or sets the position of the key.
	/// </summary>
	public GridVec2 Position { get; set; } = position;

	public bool CanOverlapWith(LevelScene level, IEntity entity)
	{
		return entity is Player;
	}


	public void Render(LevelScene level, int x, int y)
	{
		SpriteAtlas.DrawSprite(Sprite.Key, x, y);
	}

	/// <summary>
	/// Utilizes the key by removing all door entities from the all entities list in the level map.
	/// </summary>
	/// <param name="level">The level scene.</param>
	/// <param name="player">The player entity.</param>
	public void Utilize(LevelScene level, Player player)
	{
		level.Map.Entities.RemoveAll(e => e is DoorEntity);
	}
}