using Raylib_cs;

class PlayerMovingSystem : ISystem<LevelScene>
{  
	public void Update(LevelScene level)
    {
		TileVec2 delta;
		if (Raylib.IsKeyPressed(KeyboardKey.Left))
			delta = TileVec2.LEFT;
		else if (Raylib.IsKeyPressed(KeyboardKey.Right))
			delta = TileVec2.RIGHT;
		else if (Raylib.IsKeyPressed(KeyboardKey.Up))
			delta = TileVec2.UP;
		else if (Raylib.IsKeyPressed(KeyboardKey.Down))
			delta = TileVec2.DOWN;
		else
			return;
		var oldPos = level.PlayerPosition;
		var newPos = level.PlayerPosition + delta;
		if (newPos.i < 0 || newPos.i >= level.map.Rows)
			return;
		if (newPos.j < 0 || newPos.j >= level.map.Collumns)
			return;
		if (level.map[newPos] is not EmptyEntity)
			return;
		level.map[newPos] = level.map[oldPos];
		level.map[oldPos] = null;
    }
}