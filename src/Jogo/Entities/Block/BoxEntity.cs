using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Raylib_cs;

/// <summary>
/// Represents a box that can be pushed by the player across the map.
/// </summary>
class BoxEntity : IEntity, IElemental
{
	public GridVec2 Position { get; set; }
	public Element Element => Element.Leaf;
	/// <summary>
	/// Initializes a new instance of the <see cref="BoxEntity"/> class.
	/// </summary>
	/// <param name="position">The position of the block entity.</param>
	public BoxEntity(GridVec2 position)
	{
		Position = position;
	}

	public bool CanOverlapWith(LevelScene level, IEntity entity)
	{
		return entity is FireEntity;
	}

	public void Render(LevelScene level, int x, int y)
	{
		Raylib.DrawRectangle(x, y, GameSystem.TileSize, GameSystem.TileSize, Color.Violet);
	}

	public void Collide(LevelScene level, IEntity entity)
	{
		if (entity is PlayerEntity)
		{
			var idiff = Position.i - entity.Position.i;
			var jdiff = Position.j - entity.Position.j;
			GridVec2 delta = GridVec2.ZERO;
			if (idiff != 0)
			{
				delta = new GridVec2(Math.Sign(idiff), 0);
			}
			if (jdiff != 0)
			{
				delta = new GridVec2(0, Math.Sign(jdiff));
			}
			var newPos = Position + delta;
			if (newPos.i < 0 || newPos.i >= level.Map.Rows)
				return;
			if (newPos.j < 0 || newPos.j >= level.Map.Collumns)
				return;
			foreach (var another in level.Map[newPos].ToList())
			{
				another.Collide(level, this);
				if (another is FireEntity){
					level.DestroyEntity(this);
					return;
				}
				if (another is WaterEntity){
					level.DestroyEntity(another);
					level.DestroyEntity(this);
					return;
				}
			}
			if (level.Map[newPos].Any(e => !e.CanOverlapWith(level, this)))
				return;
			Position = newPos;
		}
	}
}