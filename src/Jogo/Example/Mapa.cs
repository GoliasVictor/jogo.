
interface IEntity
{
	GridVec2 Position { get; set; }

	Layer Layer { get; }

	public bool CanOverlapWith(LevelScene level, IEntity entity);

	void Collide(LevelScene level, IEntity entity) { }

	void Colliding(LevelScene level, IEntity entity) { }

	void Render(LevelScene level, int x, int y);

	void TickUpdate(LevelScene level) { }
} 

interface IItem : IEntity{
	void Utilize(LevelScene level, Player player);
}

interface IElemental : IEntity {

	Element Element { get; }

	public bool WinsAgainst(IElemental entity){
		return this.Element.WinsAgainst(entity.Element);
	}
}