using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Raylib_cs;

class EmptyEntity : IEntity
{

    public void Render(LevelScene level, int x, int y)
    {
		  Raylib.DrawRectangle(x, y, GameSystem.TileSize, GameSystem.TileSize, Color.DarkGray);
		  Raylib.DrawRectangle(x+4, y+4, GameSystem.TileSize-8, GameSystem.TileSize-8, Color.Gray);
    }
}