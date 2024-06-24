using Raylib_cs;


/// <summary>
/// Represents an element changer entity, which changes the player's element when picked up.
/// </summary>
/// <param name="position">The position of the element changer.</param>
/// <param name="element">The element that the player will have after picking up the element changer.</param>
class ElementChangerEntity(GridVec2 position, Element element) : IItem
{
    public Element Element { get; init; } = element;
    public GridVec2 Position { get; set; } = position;
    public Layer Layer => Layer.Item;

    public bool CanOverlapWith(LevelScene level, IEntity entity)
    {
        return entity is Player;
    }

    public void Render(LevelScene level, int x, int y)
    {

        SpriteSlice sprite = this.Element switch
        {
            Element.Fire => Sprite.FireItem,
            Element.Leaf => Sprite.GrassItem,
            Element.Water => Sprite.WaterItem,
            _ => new(0, 0)
        };

        SpriteAtlas.DrawSprite(sprite, x, y);
    }

    public void Utilize(LevelScene level, Player player)
    {
        player.Element = this.Element;
    }
}