
/// <summary>
/// Represents a system responsible for handling collisions between entities in a level scene.
/// </summary>
class CollisionSystem : ISystem<LevelScene>
{
	/// <summary>
	/// Solves a collision conflict between two elemental entities.
	/// </summary>
	/// <param name="level">The level scene.</param>
	/// <param name="A">The first elemental entity.</param>
	/// <param name="B">The second elemental entity.</param>
	public static void SolveElementalConflit(LevelScene level, IElemental A, IElemental B)
	{
		if (B is PlayerEntity player && player.Element == Element.Neutral)
		{
			level.DestroyEntity(B);
		}
		if (A.Element.WinAgainst(B.Element))
		{
			level.DestroyEntity(B);
			return;
		}
		if (B.Element.WinAgainst(A.Element))
		{
			level.DestroyEntity(A);
		}
	}

	/// <summary>
	/// Updates the collision system for the specified level scene.
	/// </summary>
	/// <param name="context">The level scene context.</param>
	public void Update(LevelScene context)
	{
		for (int i = 0; i < context.Map.Rows; i++)
		{
			for (int j = 0; j < context.Map.Collumns; j++)
			{
				var entities = context.Map[i, j].ToList();
				foreach (var entity in entities)
				{
					foreach (var entity2 in entities)
					{
						if (entity == entity2)
							continue;
						entity.Colliding(context, entity2);
						if (entity is IElemental A && entity2 is IElemental B)
						{
							SolveElementalConflit(context, A, B);
						}
					}
				}
			}
		}
	}
}