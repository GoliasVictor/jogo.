using System.Numerics;
using Raylib_cs;

/// <summary>
/// It represents a fire that spreads
/// </summary>
class FireEntity(GridVec2 position) : IEntity, IElemental
{
    public GridVec2 Position { get; set; } = position;
    public Layer Layer => Layer.Block;
    public Element Element => Element.Fire;

    public bool CanOverlapWith(LevelScene level, IEntity entity)
    {
        return entity.Layer >= Layer && entity is not FireEntity;
    }

    public void Render(LevelScene level, int x, int y)
    {
        SpriteAtlas.DrawSprite(Sprite.FireTile, x, y);
    }

    public void TickUpdate(LevelScene level)
    {
        GridVec2[] deltas = [new(-1, 0), new(1, 0), new(0, -1), new(0, 1)];
        foreach (var delta in deltas)
        {
            var newpos = delta + this.Position;
            var newEntity = new FireEntity(newpos);
            var entities = level.Map[newpos].ToList();
            var added = false;
            foreach (var entity in entities.OfType<IElemental>())
            {
                if (((IElemental)this).WinsAgainst(entity))
                {
                    level.Map.Entities.Add(newEntity);
                    added = true;
                }
            }
            if (!added && level.Map[newpos].All(e => e.CanOverlapWith(level, newEntity)))
            {
                level.Map.Entities.Insert(0, newEntity);
            }
        }
    }

}