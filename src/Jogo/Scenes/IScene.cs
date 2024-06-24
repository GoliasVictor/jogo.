
/// <summary>
/// Represents a scene in the game that can be rendered.
/// </summary>
/// <typeparam name="T">The type of context used for rendering.</typeparam>
interface IScene<T> : ISystem<T>
{
    /// <summary>
    /// Renders the scene using the specified context.
    /// </summary>
    /// <param name="context">The context used for rendering.</param>
    void Render(T context);

    /// <summary>
    /// Called when the game's window view has been resized in order to change rendering.
    /// </summary>
    void ViewSizeChanged() {}
}

/// <summary>
/// Represents a scene in the game that can be rendered.
/// </summary>
interface IScene : ISystem
{
    /// <summary>
    /// Renders the scene.
    /// </summary>
    void Render();

    /// <summary>
    /// Called when the game's window view has been resized in order to change rendering.
    /// </summary>
    void ViewSizeChanged() {}
}