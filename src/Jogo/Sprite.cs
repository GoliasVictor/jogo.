/// <summary>
/// Coordinates and size of a slice of the sprite atlas.
/// </summary>
/// <param name="i">Row of the slice.</param>
/// <param name="j">Column of the slice.</param>
public struct SpriteSlice(uint i, uint j) {
    public uint i = i;
    public uint j = j;
    public uint w = 1;
    public uint h = 1;
}

/// <summary>
/// List of all the slices to be imported into the atlas.
/// </summary>
public static class Sprite {
    public readonly static SpriteSlice Wall = new(0, 0);
    public readonly static SpriteSlice Flag = new(0, 1);
    public readonly static SpriteSlice Door = new(0, 2);
    public readonly static SpriteSlice Key = new(0, 3);
    public readonly static SpriteSlice FirePlayer = new(1, 0);
    public readonly static SpriteSlice WaterPlayer = new(1, 1);
    public readonly static SpriteSlice GrassPlayer = new(1, 2);
    public readonly static SpriteSlice NeutralPlayer = new(1, 3);
    public readonly static SpriteSlice FireTile = new(2, 0);
    public readonly static SpriteSlice WaterTile = new(2, 1);
    public readonly static SpriteSlice GrassTile = new(2, 2);
    public readonly static SpriteSlice FireItem = new(3, 0);
    public readonly static SpriteSlice WaterItem = new(3, 1);
    public readonly static SpriteSlice GrassItem = new(3, 2);
    public readonly static SpriteSlice FireEnemy = new(4, 0);
    public readonly static SpriteSlice WaterEnemy = new(4, 1);
    public readonly static SpriteSlice GrassEnemy = new(4, 2);
}
