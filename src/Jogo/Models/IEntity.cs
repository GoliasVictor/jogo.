using System.Numerics;

interface IEntity{
	GridVec2 Position { get; set; }
	void Render(LevelScene level, int x, int y);
	bool CanOverlapedBy(LevelScene level, IEntity entity);
    void Collide(LevelScene level, IEntity player){}
}