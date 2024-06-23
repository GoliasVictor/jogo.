using Raylib_cs;

/// <summary>
/// Represents a system responsible for updating the movement of the player in the game.
/// </summary>
class TickUpdateSystem : ISystem<LevelScene>
{  
	/// <summary>
	/// Updates the movement of the player based on the user input.
	/// </summary>
	/// <param name="level">The current level scene.</param>
	public void Update(LevelScene level)
    {
        GridVec2 direction =  GridVec2.ZERO;

        if (Raylib.IsKeyPressed(KeyboardKey.Left))
            direction += GridVec2.LEFT;
        if (Raylib.IsKeyPressed(KeyboardKey.Right))
            direction += GridVec2.RIGHT;
        if (Raylib.IsKeyPressed(KeyboardKey.Up))
            direction += GridVec2.UP;
        if (Raylib.IsKeyPressed(KeyboardKey.Down))
            direction += GridVec2.DOWN;
        if (direction == GridVec2.ZERO)
            return;
        
        level.Player.Move(level, direction);
        var entities = level.Map.Entities.ToList();
        entities.Sort((a, b) => a.Layer.CompareTo(b.Layer));
        foreach (var entity in entities)
        {
            entity.TickUpdate(level);
        }
    }

    
}
