using System.Numerics;
using Raylib_cs;

/// <summary>
/// Represents an enemy of a element that moves horizontally or vertically
/// </summary>
class Enemy : IEntity, IElemental
{
    public GridVec2 Position { get; set; }
    public Layer Layer => Layer.Character;

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
        return entity.Layer > this.Layer;
    }
    public void Render(LevelScene level, int x, int y)
    {
        SpriteSlice sprite = Element switch {
            Element.Fire => Sprite.FireEnemy,
            Element.Leaf => Sprite.GrassEnemy,
            Element.Water => Sprite.WaterEnemy,
            _ => new(0, 0)
        };
        SpriteAtlas.DrawSprite(sprite, x, y);
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