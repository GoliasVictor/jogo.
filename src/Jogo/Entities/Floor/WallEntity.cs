using Jogo.Systems;
using Raylib_cs;

/// <summary>
/// Represents a wall entity, no object can overlap it..
/// </summary>
/// <param name="position"></param>
class Wall(GridVec2 position) : IEntity
{
    public GridVec2 Position { get; set; } = position;
    public Layer Layer => Layer.Floor;

    public bool CanOverlapWith(LevelScene level, IEntity entity)
    {
        return false;
    }
    public void Render(LevelScene level, int x, int y)
    {
        SpriteAtlas.DrawSprite(Sprite.Wall, x, y);
    }

    void Collide(LevelScene level, IEntity entity)
    {
        Audio.PlaySound(IAudio.SoundEffect.Wall, true)
    }
}