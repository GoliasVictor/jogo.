using System.Numerics;
using Raylib_cs;

/// <summary>
/// Represents an enemy of a element that moves horizontally or vertically
/// </summary>
class Enemy : IEntity, IElemental
{
    public GridVec2 Position { get; set; }
    public Element Element { get; set; }
    private GridVec2 direction;

    public Enemy(GridVec2 position, Element element, bool horizontal)
    {
        Position = position;
        Element = element;
        if(horizontal){
            direction = new GridVec2(0, 1);
        } else {
            direction = new GridVec2(1, 0);
        }
    }

    public bool CanOverlapWith(LevelScene level, IEntity entity)
    {
        return entity is PlayerEntity;
    }
    public void Render(LevelScene level, int x, int y)
    {
      Vector2 v = new Vector2(x, y);
      Color color = this.Element.GetColor();
      Raylib.DrawTriangle(v + GameSystem.TileSize * new Vector2(0.5f, 0), v + GameSystem.TileSize * new Vector2(0, 1), v + GameSystem.TileSize * new Vector2(1, 1), color);
    }

    public void TickUpdate(LevelScene level) {
       
        if (level.Map[this.Position+direction].All(e => e.CanOverlapWith(level, this))){
            this.Position += direction;
        } else {
            direction = -direction;
            var newpos2 = direction + this.Position;
            if (level.Map[this.Position+direction].All(e => e.CanOverlapWith(level, this))){
				this.Position = newpos2;
            }
        }
    }

}