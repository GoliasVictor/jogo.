
using System.Numerics;
using Raylib_cs;

/// <summary>
/// Represents a level scene in the game.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="LevelScene"/> class.
/// </remarks>
/// <param name="systems">The systems associated with the level scene.</param>
/// <param name="map">The map associated with the level scene.</param>
class LevelScene(ISystem<LevelScene>[] systems, Map map) : IScene
{
    ISystem<LevelScene>[] systems = systems;

    /// <summary>
    /// Gets the map associated with the level scene.
    /// </summary>
    public Map Map { get; private init; } = map;

    /// <summary>
    /// Gets the player entity in the level scene.
    /// </summary>
    public Player Player
    {
        get => Map.Entities.OfType<Player>().First();
    }

    /// <summary>
    /// Updates the level scene.
    /// </summary>
    public void Update()
    {
        foreach (var system in systems)
            system.Update(this);
    }

    /// <summary>
    /// Renders the floor of the level scene.
    /// </summary>
    /// <param name="start_x">The starting x-coordinate of the floor.</param>
    /// <param name="start_y">The starting y-coordinate of the floor.</param>
    void RenderFloor(int start_x, int start_y)
    {
        for (int i = 0; i < Map.Rows; i++)
        {
            for (int j = 0; j < Map.Columns; j++)
            {
                var x = start_x + j * GameSystem.TileSize;
                var y = start_y + i * GameSystem.TileSize;
                Raylib.DrawRectangle(x, y, GameSystem.TileSize, GameSystem.TileSize, Color.DarkGray);
                var Border = 3;
                Raylib.DrawRectangle(x + Border, y + Border, GameSystem.TileSize - 2 * Border, GameSystem.TileSize - 2 * Border, Color.Gray);
            }
        }
    }
    /// <summary>
    /// Renders the level scene.
    /// </summary>
    public void Render()
    {
        int start_x = GameSystem.DefaultWindowWidth / 2 - Map.Columns * GameSystem.TileSize / 2;
        int start_y = GameSystem.DefaultWindowHeight / 2 - Map.Rows * GameSystem.TileSize / 2;
        RenderFloor(start_x, start_y);
        foreach (var entity in Map.Entities)
        {
            var x = start_x + entity.Position.j * GameSystem.TileSize;
            var y = start_y + entity.Position.i * GameSystem.TileSize;
            entity.Render(this, x, y);
        }
    }
    /// <summary>
    /// Destroys the specified entity. If the entity is a player, it calls the KillPlayer method on the player entity. 
    /// Otherwise, it removes the entity from the map's list of entities.
    /// </summary>
    /// <param name="Entity">The entity to destroy.</param>
    public void DestroyEntity(IEntity Entity)
    {
        if (Entity is Player player)
        {
            player.KillPlayer();
            return;
        }
        this.Map.Entities.Remove(Entity);
    }

}