
/// <summary>
/// Represents a system that updates game logic with a specific context.
/// </summary>
/// <typeparam name="T">The type of the context.</typeparam>
interface ISystem<T> {
	/// <summary>
	/// Updates the game logic using the specified context.
	/// </summary>
	/// <param name="context">The context used to update the game logic.</param>
	void Update(T context);
}

/// <summary>
/// Represents a system that updates game logic.
/// </summary>
interface ISystem {
	/// <summary>
	/// Updates the game logic.
	/// </summary>
	void Update();
}