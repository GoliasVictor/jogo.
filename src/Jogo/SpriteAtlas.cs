using Raylib_cs;

/// <summary>
/// Responsible for storing all of the Sprites after they have been imported
/// </summary>
public class SpriteAtlas {
    private static SpriteAtlas instance = new(0, 0);

    private Texture2D[, ] _sprites;
    private readonly uint _rows;
    private readonly uint _columns;

    /// <summary>
    /// Creates new SpriteAtlas and sets instance to it.
    /// </summary>
    /// <param name="rows">Number of rows of the Atlas.</param>
    /// <param name="columns">Number of columns of the Atlas.</param>
    public SpriteAtlas(uint rows, uint columns) {
        _rows = rows;
        _columns = columns;
        _sprites = new Texture2D[rows, columns];
        instance = this;
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