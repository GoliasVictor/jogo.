using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Raylib_cs;

class PlayerEntity : IEntity
{
    public void Render(LevelScene level, int x, int y)
    {
		  Raylib.DrawRectangle(x, y, GameSystem.TileSize, GameSystem.TileSize, Color.Red );
    }
}