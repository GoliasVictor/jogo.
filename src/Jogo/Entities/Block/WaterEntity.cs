using System.Numerics;
using Raylib_cs;

/// <summary>
///  Represents a pool of water, which is a block of the water element.
/// </summary>
/// <param name="position">Position of the water</param>
class WaterEntity(GridVec2 position) : IEntity, IElemental
{
    public GridVec2 Position { get; set; } = position;
    public Layer Layer => Layer.Block;
    public Element Element => Element.Water;


    public bool CanOverlapWith(LevelScene level, IEntity entity)
    {
        return true;
    }

    public void Render(LevelScene level, int x, int y)
    {
        SpriteAtlas.DrawSprite(Sprite.WaterTile, x, y);
    }

}