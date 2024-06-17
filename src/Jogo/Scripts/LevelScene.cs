
using System.Numerics;
using Raylib_cs;

/// <summary>
/// Represents a level scene in the game.
/// </summary>
class LevelScene : IScene {
	ISystem<LevelScene>[] systems;

	/// <summary>
	/// Gets the map associated with the level scene.
	/// </summary>
	public Map Map { get; private init; }

	/// <summary>
	/// Gets the player entity in the level scene.
	/// </summary>
	public IEntity Player {
		get {
			return Map.MovableEntities.First(e => e is PlayerEntity);
		}
	}

	/// <summary>
	/// Updates the level scene.
	/// </summary>
	public void Update() {
		foreach (var system in systems) {
			system.Update(this);
		}
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="LevelScene"/> class.
	/// </summary>
	/// <param name="systems">The systems associated with the level scene.</param>
	/// <param name="map">The map associated with the level scene.</param>
	public LevelScene(ISystem<LevelScene>[] systems, Map map) {
		this.systems = systems;
		this.Map = map;
	}

	/// <summary>
	/// Renders the level scene.
	/// </summary>
	public void Render() {
		int start_x = GameSystem.DefaultWindowWidth / 2 - Map.Collumns * GameSystem.TileSize / 2;
		int start_y = GameSystem.DefaultWindowHeight / 2 - Map.Rows * GameSystem.TileSize / 2;
		foreach (var entity in Map.AllEntities) {
			var x = start_x + entity.Position.j * GameSystem.TileSize;
			var y = start_y + entity.Position.i * GameSystem.TileSize;
			entity.Render(this, x, y);
		}
	}
}