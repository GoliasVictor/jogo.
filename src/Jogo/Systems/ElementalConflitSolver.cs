static class ElementalConflitSolver
{
		/// <summary>
	/// Solves a collision conflict between two elemental entities.
	/// </summary>
	/// <param name="level">The level scene.</param>
	/// <param name="A">The first elemental entity.</param>
	/// <param name="B">The second elemental entity.</param>
	public static void Solve(LevelScene level, IElemental A, IElemental B)
	{

		Player? player;
		if (A is Player p)
			player = p;
		else if (B is Player p2)
			player = p2;
		else
			player = null;
		if (player?.Element == Element.Neutral)
			level.DestroyEntity(player);

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
}