
using System.Numerics;
using Raylib_cs;

class LevelScene: IScene {
	ISystem<LevelScene>[] systems;
	public Map Map { get; private init; }
	public IEntity Player { get {
			return Map.MovableEntities.First(e => e is PlayerEntity);
		}
	}

	public void Update(){
		foreach(var system in systems){
			system.Update(this);
		}
	}
	public LevelScene(ISystem<LevelScene>[] systems, Map map ){
		this.systems = systems;
		this.Map = map;
	}
	
	public void Render(){
		int start_x = GameSystem.DefaultWindowWidth / 2 - Map.Collumns*GameSystem.TileSize/2; 
		int start_y = GameSystem.DefaultWindowHeight / 2 - Map.Rows*GameSystem.TileSize/2; 
		foreach (var entity in Map.AllEntities ){
			var x = start_x + entity.Position.j * GameSystem.TileSize;
			var y = start_y + entity.Position.i * GameSystem.TileSize;
			entity.Render(this, x, y);
		}
	}

}