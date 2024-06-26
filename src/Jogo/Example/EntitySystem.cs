interface IEntity
{ 
	GridVec2 Position { get; set; } 
	
	Layer Layer { get; } 
	
	bool CanOverlapWith(LevelScene level, IEntity entity);
 	
    void Collide(LevelScene level, IEntity entity) { }
	
    void Colliding(LevelScene level, IEntity entity) { }
	
	void Render(LevelScene level, int x, int y);

	void TickUpdate(LevelScene level) { }
}


interface IItem : IEntity
{
	void Utilize(LevelScene level, Player player);
}

interface IElemental : IEntity {

	Element Element { get; }

	public bool WinsAgainst(IElemental entity){ .. }
}

class Enemy : IEntity, IElemental
{
    public GridVec2 Position { get; set; }
    public Element Element { get; set; }
    public Layer Layer => Layer.Character;

    public bool CanOverlapWith(LevelScene level, IEntity entity) {
        return entity.Layer > this.Layer;
    }
    public void Render(LevelScene level, int x, int y)
    {
        // Calcula o sprite baseado na elemento
        SpriteAtlas.DrawSprite(sprite, x, y);
    }

    public void TickUpdate(LevelScene level) {
		// Move o inimigo, se não conseguir muda de direção
    }
}


 
class CollisionSystem : ISystem<LevelScene>
{
	public void Update(LevelScene context)
	{
		for (int i = 0; i < context.Map.Rows; i++)
			for (int j = 0; j < context.Map.Columns; j++)
				Collide(level, context.Map[i, j].ToList());
	}
    private void Collide(LevelScene level, List<IEntity> entities)
    {
        foreach (var (entity,entity2) in <pares entidades>)
        {
            if (entity == entity2)
                continue;
            entity.Colliding(context, entity2);
            if (entity is IElemental A && entity2 is IElemental B)
                ElementalConflitSolver.Solve(context, A, B);
        }
    }
}