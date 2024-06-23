
/// <summary>
/// Represents a system responsible for collecting items in the game.
/// </summary>
class ItemCollectionSystem : ISystem<LevelScene>
{
    /// <summary>
    /// Updates the level scene by collecting items that the player is currently on.
    /// </summary>
    /// <param name="context">The level scene context.</param>
    public void Update(LevelScene context)
    {
        var player = context.Player;
        var entities = context.Map[player.Position];

        foreach (var item in entities.OfType<IItem>().ToList())
        {
            player.RecipeItem(context, item);
            context.DestroyEntity(item);
        }
    }
}