
using Raylib_cs;

/// <summary>
/// Responsible for importing the sprites into the game via the sprite atlas.
/// </summary>
public static class SpriteImporter {
    private const string _AtlasPath = "Assets/sprite-atlas.png";
    private const uint _AtlasColumns = 4;
    private const uint _AtlasRows = 5;
    private const uint _SpriteWidth = 16;
    private const uint _SpriteHeight = 16;

    /// <summary>
    /// Creates new SpriteAtlas and loads all sprites into it.
    /// </summary>
    public static void ImportAtlas() {
        SpriteAtlas _ = new SpriteAtlas(_AtlasRows, _AtlasColumns);
        
        Image atlasImage = Raylib.LoadImage(_AtlasPath);
        for(uint i = 0; i < _AtlasRows; i++) {
            for(uint j = 0; j < _AtlasColumns; j++) {
                LoadSprite(atlasImage, i, j);
            }
        }
        Raylib.UnloadImage(atlasImage);
    }

    /// <summary>
    /// Method <c>LoadSprite</c> is responsible for retrieving the desired slice and adding it to the Atlas.
    /// </summary>
    /// <param name="atlasImage">The entire atlas source.</param>
    /// <param name="i">Sprite row</param>
    /// <param name="j">Sprite column</param>
    /// <returns>Texture2D slice of the atlas in the desired position.</returns>
    private static void LoadSprite(Image atlasImage, uint i, uint j) {
        Rectangle imageBounds = new Rectangle(j * _SpriteWidth, i * _SpriteHeight, _SpriteWidth, _SpriteHeight);
        Image image = Raylib.ImageFromImage(atlasImage, imageBounds);
        Texture2D texture = Raylib.LoadTextureFromImage(image);
        Raylib.UnloadImage(image);
        SpriteAtlas.SetSprite(texture, i, j);
    }
}