using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Jogo.Systems;
using Raylib_cs;

/// <summary>
/// Represents a box that can be pushed by the player across the map.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="Box"/> class.
/// </remarks>
/// <param name="position">The position of the block entity.</param>
class Box(GridVec2 position) : IEntity, IElemental
{
    public GridVec2 Position { get; set; } = position;
    public Layer Layer => Layer.Block;
    public Element Element => Element.Leaf;

    public bool CanOverlapWith(LevelScene level, IEntity entity)
    {
        return entity.Layer >= Layer && entity is not Player;
    }

    public void Render(LevelScene level, int x, int y)
    {
        SpriteAtlas.DrawSprite(Sprite.GrassTile, x, y);
    }

    public void Collide(LevelScene level, IEntity entity)
    {
        if (entity is Player)
        {
            var idiff = Position.i - entity.Position.i;
            var jdiff = Position.j - entity.Position.j;
            GridVec2 delta = GridVec2.ZERO;
            if (idiff != 0)
            {
                delta = new GridVec2(Math.Sign(idiff), 0);
            }
            if (jdiff != 0)
            {
                delta = new GridVec2(0, Math.Sign(jdiff));
            }
            var newPos = Position + delta;
            if (newPos.i < 0 || newPos.i >= level.Map.Rows)
                return;
            if (newPos.j < 0 || newPos.j >= level.Map.Columns)
                return;
            foreach (var another in level.Map[newPos].ToList())
            {
                another.Collide(level, this);
                if (another is FireEntity)
                {
                    level.DestroyEntity(this);
                    return;
                }
                if (another is WaterEntity)
                {
                    level.DestroyEntity(another);
                    level.DestroyEntity(this);
                    return;
                }
            Audio.PlaySound(IAudio.SoundEffect.PushBlock, true);
            }
            if (level.Map[newPos].Any(e => !e.CanOverlapWith(level, this)))
                return;
            Position = newPos;
        }
    }
}