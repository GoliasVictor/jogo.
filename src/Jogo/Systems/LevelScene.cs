
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
    private const int blockMargin = 1;

    ISystem<LevelScene>[] systems = systems;

    private Camera2D _camera = GenerateCamera(map);

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
        Raylib.BeginMode2D(_camera);
            foreach (var entity in Map.Entities)
            {
                var x = entity.Position.j * GameSystem.TileSize;
                var y = entity.Position.i * GameSystem.TileSize;
                Raylib.DrawRectangle(x, y, GameSystem.TileSize, GameSystem.TileSize, Color.Black);                
                entity.Render(this, x, y);
            }
        Raylib.EndMode2D();
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

    public void ViewSizeChanged() {
        _camera = GenerateCamera(Map);
    }

    /// <summary>
    /// Creates a new Camera for the scene.
    /// </summary>
    private static Camera2D GenerateCamera(Map map) {
        float scaleFactor = (float)Raylib.GetScreenHeight() / ((map.Rows + blockMargin * 2) * GameSystem.TileSize);
        Vector2 position = new();
        position.X = Raylib.GetScreenWidth() / (scaleFactor) - map.Rows * GameSystem.TileSize;
        position.X *= -0.5f;
        position.Y = -blockMargin * GameSystem.TileSize;
        
        return new() {
            Target = position,
            Zoom = scaleFactor,
            Rotation = 0f,
            Offset = Vector2.Zero
        };
    }

}