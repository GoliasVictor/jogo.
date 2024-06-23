using Raylib_cs;


/// <summary>
/// Represents an element changer entity, which changes the player's element when picked up.
/// </summary>
/// <param name="position">The position of the element changer.</param>
/// <param name="element">The element that the player will have after picking up the element changer.</param>
class ElementChangerEntity(GridVec2 position, Element element) : IItem {
    public Element Element { get; init; } = element;

    public GridVec2 Position { get; set; } = position;

    public bool CanOverlapWith(LevelScene level, IEntity entity)
    {
		return entity is PlayerEntity;
    }

	public void Collide(LevelScene level, IEntity entity) { 

	}
	

    public void Render(LevelScene level, int x, int y)
    {
        
        Color color = this.Element switch {
            Element.Neutral => Color.White,
            Element.Fire => Color.Red,
            Element.Leaf => Color.Green,
            Element.Water => Color.Blue,
            _ => Color.White
        };
        
        Raylib.DrawRectangle(x + 8, y + 8, GameSystem.TileSize - 16, GameSystem.TileSize - 16, color);
    }

    public void Utilize(LevelScene level, PlayerEntity player)
    {
        player.Element = this.Element;
    }
}