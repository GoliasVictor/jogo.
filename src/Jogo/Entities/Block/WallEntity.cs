using Raylib_cs;

/// <summary>
/// Represents a wall entity, no object can overlap it..
/// </summary>
/// <param name="position"></param>
class Wall(GridVec2 position) : IEntity
{
    public GridVec2 Position { get; set; } = position;

    public bool CanOverlapWith(LevelScene level, IEntity entity)
    {
		  return false;
    }
    public void Render(LevelScene level, int x, int y)
    {
        Raylib.DrawRectangle(x, y, GameSystem.TileSize, GameSystem.TileSize, Color.DarkBrown);
    }
}