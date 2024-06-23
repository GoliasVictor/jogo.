using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Raylib_cs;

/// <summary>
/// Represents a player entity in the game.
/// </summary>
class PlayerEntity : IEntity, IElemental
{
    /// <summary>
    /// Gets or sets the position of the player entity on the grid.
    /// </summary>
    public GridVec2 Position { get; set; }
    public Element Element{ get; set; } = Element.Neutral;
    public bool PlayerKilled = false;
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerEntity"/> class with the specified position.
    /// </summary>
    /// <param name="position">The initial position of the player entity.</param>
    public PlayerEntity(GridVec2 position)
    {
        Position = position;
    }

    /// <summary>
    /// Renders the player entity on the screen.
    /// </summary>
    /// <param name="level">The level scene where the player entity is rendered.</param>
    /// <param name="x">The x-coordinate of the top-left corner of the player entity's rendering area.</param>
    /// <param name="y">The y-coordinate of the top-left corner of the player entity's rendering area.</param>
    public void Render(LevelScene level, int x, int y)
    {
        Color color = this.Element switch {
            Element.Neutral => Color.White,
            Element.Fire => Color.Red,
            Element.Leaf => Color.Green,
            Element.Water => Color.Blue,
            _ => Color.White
        };
        
		Raylib.DrawCircle(x+ GameSystem.TileSize/2, y+ GameSystem.TileSize/2, (GameSystem.TileSize - 6)/2f, color);
    }

    /// <summary>
    /// Determines whether the player entity can be overlapped by the specified entity.
    /// </summary>
    /// <param name="level">The level scene where the player entity exists.</param>
    /// <param name="entity">The entity to check for overlap.</param>
    /// <returns><c>true</c> if the player entity can be overlapped by the specified entity; otherwise, <c>false</c>.</returns>
    public bool CanOverlapWith(LevelScene level, IEntity entity)
    {
        return entity is FireEntity;
    }

    public void RecipeItem(LevelScene level, IItem item){
        item.Utilize(level, this);
    }
    public void KillPlayer(){
        this.PlayerKilled = true;
	}
}