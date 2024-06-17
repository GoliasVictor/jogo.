using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Raylib_cs;

class BlockEntity(GridVec2 position) : IEntity
{
    public GridVec2 Position { get; set; } = position;

    public bool CanOverlapedBy(LevelScene level, IEntity entity)
    {
		return false;
    }

    public void Render(LevelScene level, int x, int y)
    {
		  Raylib.DrawRectangle(x, y, GameSystem.TileSize, GameSystem.TileSize, Color.Violet );
    }

	public void Collide(LevelScene level, IEntity entity) {
		if (entity is PlayerEntity) {
			var idiff = Position.i - entity.Position.i;
			var jdiff = Position.j - entity.Position.j;
			GridVec2 delta = GridVec2.ZERO;
			if (idiff != 0) {
				delta = new GridVec2(Math.Sign(idiff), 0);
			}
			if (jdiff != 0) {
				delta = new GridVec2(0, Math.Sign(jdiff));
			}
			var newPos = Position + delta;
			if (newPos.i < 0 || newPos.i >= level.Map.Rows)
				return;
			if (newPos.j < 0 || newPos.j >= level.Map.Collumns)
				return;
			foreach (var another in level.Map[newPos]) {
				another.Collide(level, this);
			}
			if (level.Map[newPos].Any(e => !e.CanOverlapedBy(level, this)))
				return;
			Position = newPos;
		}
	}

}