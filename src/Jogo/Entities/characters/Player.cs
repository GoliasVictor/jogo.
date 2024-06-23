using Raylib_cs;

/// <summary>
/// Represents a player entity in the game.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="Player"/> class with the specified position.
/// </remarks>
/// <param name="position">The initial position of the player entity.</param>
class Player(GridVec2 position) : IEntity, IElemental
{
    /// <summary>
    /// Gets or sets the position of the player entity on the grid.
    /// </summary>
    public GridVec2 Position { get; set; } = position;
    public Layer Layer => Layer.Character;
    public Element Element { get; set; } = Element.Neutral;
    public bool PlayerKilled = false;

    /// <summary>
    /// Renders the player entity on the screen.
    /// </summary>
    /// <param name="level">The level scene where the player entity is rendered.</param>
    /// <param name="x">The x-coordinate of the top-left corner of the player entity's rendering area.</param>
    /// <param name="y">The y-coordinate of the top-left corner of the player entity's rendering area.</param>
    public void Render(LevelScene level, int x, int y)
    {
        Color color = this.Element switch
        {
            Element.Neutral => Color.White,
            Element.Fire => Color.Red,
            Element.Leaf => Color.Green,
            Element.Water => Color.Blue,
            _ => Color.White
        };

        Raylib.DrawCircle(x + GameSystem.TileSize / 2, y + GameSystem.TileSize / 2, (GameSystem.TileSize - 6) / 2f, color);
    }

    public bool CanOverlapWith(LevelScene level, IEntity entity)
    {
        return entity.Layer != this.Layer;
    }

    public void ReceiveItem(LevelScene level, IItem item)
    {
        item.Utilize(level, this);
    }
    public void KillPlayer()
    {
        this.PlayerKilled = true;
    }

    public void Move(LevelScene level, GridVec2 direction)
    {
        var player = level.Player;
        var oldPos = player.Position;
        var newPos = oldPos + direction;

        if (newPos.i < 0 || newPos.i >= level.Map.Rows)
            return;
        if (newPos.j < 0 || newPos.j >= level.Map.Columns)
            return;
        foreach (var entity in level.Map[newPos].ToList())
        {
            entity.Collide(level, player);
            if (entity is IElemental elemental)
            {
                ElementalConflitSolver.Solve(level, player, elemental);
            }

        }
        if (level.Map[newPos].Any(e => !e.CanOverlapWith(level, player)))
            return;
        player.Position = newPos;
    }
}