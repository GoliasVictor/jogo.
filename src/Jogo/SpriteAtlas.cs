using Raylib_cs;

/// <summary>
/// Responsible for storing all of the Sprite slices from the sprite-atlas file.
/// </summary>
public static class SpriteAtlas {

    private const string SourcePath = @"Assets/sprite-atlas.png";
    private const uint Columns = 4;
    private const uint Rows = 5;
    
    public const uint SpriteWidth = 16;
    public const uint SpriteHeight = 16;

    private static Texture2D[, ] _sprites = new Texture2D[Rows, Columns];

    /// <summary>
    /// Loads all sprites into atlas.
    /// </summary>
    public static void LoadAtlas() {
        Image fullImage = Raylib.LoadImage(SourcePath);
        for(uint i = 0; i < Rows; i++) {
            for(uint j = 0; j < Columns; j++) {
                LoadSprite(fullImage, i, j);
            }
        }
        Raylib.UnloadImage(fullImage);
    }

    /// <summary>
    /// Method <c>LoadSprite</c> is responsible for retrieving the desired slice and adding it to the Atlas.
    /// </summary>
    /// <param name="atlasImage">The entire atlas source.</param>
    /// <param name="i">Sprite row</param>
    /// <param name="j">Sprite column</param>
    /// <returns>Texture2D slice of the atlas in the desired position.</returns>
    private static void LoadSprite(Image atlasImage, uint i, uint j) {
        Rectangle imageBounds = new Rectangle(j * SpriteWidth, i * SpriteHeight, SpriteWidth, SpriteHeight);
        Image image = Raylib.ImageFromImage(atlasImage, imageBounds);
        Texture2D texture = Raylib.LoadTextureFromImage(image);
        Raylib.UnloadImage(image);
        SetSprite(texture, i, j);
    }

    /// <summary>
    /// Sets the sprite in the specified position of the atlas if the position is valid.
    /// </summary>
    /// <param name="sprite">The sprite to be set.</param>
    /// <param name="i">Row of the sprite.</param>
    /// <param name="j">Column of the sprite.</param>
    public static void SetSprite(Texture2D sprite, uint i, uint j) {
        if(i >= Rows || j >= Columns)
            return;
        _sprites[i, j] = sprite;
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
            return _sprites[i, j];        
        } catch(IndexOutOfRangeException e) {
            bool iValid = (i >= 0) && (i < Rows);
            bool jValid = (j >= 0) && (j < Columns);
            string exceptionDescription;
            if(!iValid && !jValid) {
                exceptionDescription = $"<Row {i}; Column {j}> not available in Atlas with {Rows}x{Columns}";
            }else if(!iValid) {
                exceptionDescription = $"Row {i} not avaliable in Atlas with {Rows} rows.";
            }else {
                exceptionDescription = $"Column {i} not avaliable in Atlas with {Columns} Columns.";
            }

            throw new IndexOutOfRangeException(exceptionDescription, e);
        }
    }

    /// <summary>
    /// Draws the desired sprite slice in the desired position.
    /// </summary>
    /// <param name="i">Row of the sprite.</param>
    /// <param name="j">Column of the sprite.</param>
    /// <param name="x">Render X coordinate.</param>
    /// <param name="y">Render Y coordinate.</param>
    public static void DrawSprite(int i, int j, int x, int y) {
        Raylib.DrawTexture(GetSprite(i, j), x, y, Color.White);
    }
}