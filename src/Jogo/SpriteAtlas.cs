using Raylib_cs;

/// <summary>
/// Responsible for storing all of the Sprites after they have been imported
/// </summary>
public class SpriteAtlas {
    private static SpriteAtlas? _instance = null;
    private static SpriteAtlas instance {
        get{
            if(_instance == null) {
                _instance = new();
            }
            return _instance;
        }
    }

    private Texture2D[, ] _sprites = new Texture2D[0, 0];
    private uint _rows = 0;
    private uint _columns = 0;

    /// <summary>
    /// Sets current instances sprite array size, mantains old values if possible.
    /// </summary>
    /// <param name="rows">Number of rows of the atlas.</param>
    /// <param name="columns">Number of columns of the atlas.</param>
    public static void SetAtlasSize(uint rows, uint columns) {
        Texture2D[, ] newArray = new Texture2D[rows, columns];
        /*for(int i = 0; i < Math.Min(rows, instance._rows); i++){
            for(int j = 0; j < Math.Min(columns, instance._columns); j++){
                newArray[i, j] = instance._sprites[i, j];
            }
        }*/
        instance._sprites = newArray;
        instance._rows = rows;
        instance._columns = columns;
        Console.WriteLine($"{rows}, {columns}");
    }

    /// <summary>
    /// Sets the sprite in the specified position of the atlas if the position is valid.
    /// </summary>
    /// <param name="sprite">The sprite to be set.</param>
    /// <param name="i">Row of the sprite.</param>
    /// <param name="j">Column of the sprite.</param>
    public static void SetSprite(Texture2D sprite, uint i, uint j) {
        if(i >= instance._rows || j >= instance._columns)
            return;
        instance._sprites[i, j] = sprite;
    }

    /// <summary>
    /// Responsible for retrieving the sprite in the desired position.
    /// </summary>
    /// <param name="i">Row of the desired sprite.</param>
    /// <param name="j">Column of the desired sprite.</param>
    /// <returns>In case the position is valid, returns the sprite in the desired position, otherwise returns the sprite in the nearest valid position.</returns>
    public static Texture2D GetSprite(int i, int j) {
        if(instance._rows == 0 || instance._columns == 0) return new();
        i = Math.Clamp(i, 0, (int)instance._rows - 1);
        j = Math.Clamp(j, 0, (int)instance._columns - 1);
        return instance._sprites[i, j];        
    }
}