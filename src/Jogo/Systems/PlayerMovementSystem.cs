using Raylib_cs;

/// <summary>
/// Represents a system responsible for updating the movement of the player in the game.
/// </summary>
class PlayerMovementSystem : ISystem<LevelScene>
{  
	/// <summary>
	/// Updates the movement of the player based on the user input.
	/// </summary>
	/// <param name="level">The current level scene.</param>
	public void Update(LevelScene level)
	{
		GridVec2 delta;
		if (Raylib.IsKeyPressed(KeyboardKey.Left))
			delta = GridVec2.LEFT;
		else if (Raylib.IsKeyPressed(KeyboardKey.Right))
			delta = GridVec2.RIGHT;
		else if (Raylib.IsKeyPressed(KeyboardKey.Up))
			delta = GridVec2.UP;
		else if (Raylib.IsKeyPressed(KeyboardKey.Down))
			delta = GridVec2.DOWN;
		else
			return;
		var player = level.Player;
		var oldPos = player.Position;
		var newPos = oldPos + delta;
		if (newPos.i < 0 || newPos.i >= level.Map.Rows)
			return;
		if (newPos.j < 0 || newPos.j >= level.Map.Collumns)
			return;
		foreach(var entity in level.Map[newPos]){
			entity.Collide(level, player);
		}
		if (level.Map[newPos].Any(e => !e.CanOverlapWith(level, player)))
			return;
		player.Position = newPos;
	}
}
