using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Raylib_cs;

class PlayerEntity : IEntity
{
    public GridVec2 Position { get; set; }
    
    public PlayerEntity(GridVec2 position)
    {
        Position = position;
    }

    public void Render(LevelScene level, int x, int y)
    {
		  Raylib.DrawRectangle(x, y, GameSystem.TileSize, GameSystem.TileSize, Color.Red );
    }

    public bool CanOverlapedBy(LevelScene level, IEntity entity)
    {
        return false;
    }
}