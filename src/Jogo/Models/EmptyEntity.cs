using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Raylib_cs;

/// <summary>
/// Represents an empty entity in the game.
/// </summary>
class EmptyEntity : IEntity
{
    public GridVec2 Position { get; set; }

    public bool CanOverlapedBy(LevelScene level, IEntity entity)
    {
        return true;
    }
    public void Render(LevelScene level, int x, int y)
    {
        Raylib.DrawRectangle(x, y, GameSystem.TileSize, GameSystem.TileSize, Color.DarkGray);
        Raylib.DrawRectangle(x + 4, y + 4, GameSystem.TileSize - 8, GameSystem.TileSize - 8, Color.Gray);
    }
}