
using System.Numerics;
using Raylib_cs;

class LevelScene: IScene {
	ISystem<LevelScene>[] systems;
	public Map map { get; private init; }
	public TileVec2 PlayerPosition { get {
			for( int i =0; i < map.Rows; i++){
				for (int j = 0; j < map.Collumns; j++) {
					if(map[i, j] is PlayerEntity){
						return new TileVec2(i, j);
					}
				}
			}
			return new TileVec2(-1,-1);
		}
	}

	public void Update(){
		foreach(var system in systems){
			system.Update(this);
		}
	}
	public LevelScene(ISystem<LevelScene>[] systems, Map map ){
		this.systems = systems;
		this.map = map;
	}

	public void Render(){
		int start_x = GameSystem.DefaultWindowWidth / 2 - map.Collumns*GameSystem.TileSize/2; 
		int start_y = GameSystem.DefaultWindowHeight / 2 - map.Rows*GameSystem.TileSize/2; 
		for( int i =0; i < map.Rows; i++){
			for (int j = 0; j < map.Collumns; j++) {
				map[i, j]?.Render(this, start_x + j * GameSystem.TileSize, start_y + i * GameSystem.TileSize);
			}
		}
	}

}