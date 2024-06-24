/// <summary>
/// Represents the levels goal.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="Box"/> class.
/// </remarks>
/// <param name="position">The position of the block entity.</param>
class Goal(GridVec2 position) : IEntity
{
    public GridVec2 Position { get; set; } = position;
    public Layer Layer => Layer.Block;

    public bool CanOverlapWith(LevelScene level, IEntity entity)
    {
        return entity is Player;
    }

    public void Render(LevelScene level, int x, int y)
    {
        SpriteAtlas.DrawSprite(Sprite.Flag, x, y);
    }

    public void Collide(LevelScene level, IEntity entity)
    {
        if (entity is Player)
        {
            level.levelWon = true;
        }
    }
}