using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Raylib_cs;

/// <summary>
/// Represents a box that can be pushed by the player across the map.
/// </summary>
class BlockEntity : IEntity
{
	/// <summary>
	/// Gets or sets the position of the block entity.
	/// </summary>
	public GridVec2 Position { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="BlockEntity"/> class.
	/// </summary>
	/// <param name="position">The position of the block entity.</param>
	public BlockEntity(GridVec2 position)
	{
		Position = position;
	}

	public bool CanOverlapWith(LevelScene level, IEntity entity)
	{
		return false;
	}

	public void Render(LevelScene level, int x, int y)
	{
		Raylib.DrawRectangle(x, y, GameSystem.TileSize, GameSystem.TileSize, Color.Violet);
	}

	public bool GetPushed(LevelScene level, IEntity entity, GridVec2 direction) {
		if(direction.Equals(GridVec2.ZERO)) return false;
		GridVec2 newPos = Position + direction;
		if (newPos.i < 0 || newPos.i >= level.Map.Rows) return false;
		if (newPos.j < 0 || newPos.j >= level.Map.Collumns) return false;
		if (level.Map[newPos].Any(e => !e.CanOverlapWith(level, this)))
			return false;
		Position = newPos;
		foreach(IEntity e in level.Map[newPos]) {
			e.Collide(level, this);
		}
		return true;
	}
}