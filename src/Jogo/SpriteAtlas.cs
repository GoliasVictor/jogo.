using Raylib_cs;

/// <summary>
/// Responsible for storing all of the Sprites after they have been imported
/// </summary>
public class SpriteAtlas {
    private static SpriteAtlas instance = new();

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
        for(int i = 0; i < Math.Min(rows, instance._rows); i++){
            for(int j = 0; j < Math.Min(columns, instance._columns); j++){
                newArray[i, j] = instance._sprites[i, j];
            }
        }
        instance._sprites = newArray;
        instance._rows = rows;
        instance._columns = columns;
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
    /// <returns>In case the position is valid, returns the sprite in the desired position.</returns>
    /// <exception cref="IndexOutOfRangeException">Invalid desired position.</exception>
    public static Texture2D GetSprite(int i, int j) {
        try {
            return instance._sprites[i, j];        
        } catch(IndexOutOfRangeException e) {
            bool iValid = (i >= 0) && (i < instance._rows);
            bool jValid = (j >= 0) && (j < instance._columns);
            string exceptionDescription;
            if(!iValid && !jValid) {
                exceptionDescription = $"<Row {i}; Column {j}> not available in Atlas with {instance._rows}x{instance._columns}";
            }else if(!iValid) {
                exceptionDescription = $"Row {i} not avaliable in Atlas with {instance._rows} rows.";
            }else {
                exceptionDescription = $"Column {i} not avaliable in Atlas with {instance._columns} Columns.";
            }

            throw new IndexOutOfRangeException(exceptionDescription, e);
        }
    }
}