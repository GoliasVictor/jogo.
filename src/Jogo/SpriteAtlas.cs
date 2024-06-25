using System.Reflection;
using Raylib_cs;

/// <summary>
/// Responsible for storing all of the Sprite slices from the sprite-atlas file.
/// </summary>
public static class SpriteAtlas {

    private const string SourcePath = @"Assets/sprite-atlas.png";
    private const uint Columns = 10;
    private const uint Rows = 10;
    
    public const uint SpriteWidth = 16;
    public const uint SpriteHeight = 16;

    private static Texture2D[, ] _sprites = new Texture2D[Rows, Columns];

    /// <summary>
    /// Loads all sprite slices from the static Sprite class into atlas.
    /// </summary>
    public static void LoadAtlas() {

        if(!File.Exists(SourcePath)) {
            throw new FileLoadException($"File not found: {SourcePath}");
        }
        Image fullImage = Raylib.LoadImage(SourcePath);
        
        foreach(FieldInfo fi in typeof(Sprite).GetFields(BindingFlags.Static | BindingFlags.Public)) {
            var value = fi.GetValue(null);
            if(value == null) continue;
            LoadSprite(fullImage, (SpriteSlice) value);
        }
        Raylib.UnloadImage(fullImage);
    }

    /// <summary>
    /// Method <c>LoadSprite</c> is responsible for retrieving the desired slice and adding it to the Atlas.
    /// </summary>
    /// <param name="atlasImage">The entire atlas source.</param>
    /// <param name="slice">Sprite slice to be loaded.</param>
    /// <returns>Texture2D slice of the atlas in the desired position.</returns>
    private static void LoadSprite(Image atlasImage, SpriteSlice slice) {
        Rectangle imageBounds = new Rectangle(slice.j * SpriteWidth, slice.i * SpriteHeight, slice.w * SpriteWidth, slice.h * SpriteHeight);
        Image image = Raylib.ImageFromImage(atlasImage, imageBounds);
        Texture2D texture = Raylib.LoadTextureFromImage(image);
        Raylib.UnloadImage(image);
        _sprites[slice.i, slice.j] = texture;
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
    /// <param name="slice">Desired slice.</param>
    /// <param name="x">Render X coordinate.</param>
    /// <param name="y">Render Y coordinate.</param>
    public static void DrawSprite(SpriteSlice slice, int x, int y) {
        Raylib.DrawTexture(GetSprite((int) slice.i, (int) slice.j), x, y, Color.White);
    }
}