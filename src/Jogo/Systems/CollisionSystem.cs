
/// <summary>
/// Represents a system responsible for handling collisions between entities in a level scene.
/// </summary>
class CollisionSystem : ISystem<LevelScene>
{


	/// <summary>
	/// Updates the collision system for the specified level scene.
	/// </summary>
	/// <param name="context">The level scene context.</param>
	public void Update(LevelScene context)
	{
		for (int i = 0; i < context.Map.Rows; i++)
		{
			for (int j = 0; j < context.Map.Columns; j++)
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
							ElementalConflitSolver.Solve(context, A, B);
						}
					}
				}
			}
		}
	}
}