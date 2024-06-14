
using System.Numerics;
using Raylib_cs;

class LevelScene: IScene {
	ISystem<LevelScene>[] Systems;
	Map map;
	public void Update(){
		foreach(var system in Systems){
			system.Update(this);
		}
	}

	public void Render(){
		Raylib.DrawCircle(800 / 2, 480 / 2, 30, Color.Purple);
	}

}