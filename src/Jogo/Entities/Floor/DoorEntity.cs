using Raylib_cs;
/// <summary>
/// It represents a door, no object is stopped, and when a key is picked up it is destroyed.
/// </summary>
/// <param name="position"></param>
class DoorEntity(GridVec2 position) : IEntity
{
    public GridVec2 Position { get; set; } = position;
    public Layer Layer => Layer.Floor;

    public bool CanOverlapWith(LevelScene level, IEntity entity)
    {
        return false;
    }

    public void Render(LevelScene level, int x, int y)
    {
        Raylib.DrawRectangle(x, y, GameSystem.TileSize, GameSystem.TileSize, Color.Yellow);
    }
}