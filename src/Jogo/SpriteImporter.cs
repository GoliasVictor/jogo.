
using Raylib_cs;

/// <summary>
/// Responsible for importing the sprites into the game via the sprite atlas.
/// </summary>
public class SpriteImporter : IDisposable {
    private const string _AtlasPath = "Assets/sprite-atlas.png";
    private const uint _AtlasColumns = 4;
    private const uint _AtlasRows = 5;
    private const uint _SpriteWidth = 16;
    private const uint _SpriteHeight = 16;

    private static SpriteImporter instance = new();

    private Image _atlasImage;

    /// <summary>
    /// Imports all Atlas tiles into SpriteAtlas, calledd when game starts.
    /// </summary>
    public static void ImportAtlas() {
        instance._atlasImage = Raylib.LoadImage(_AtlasPath);
        SpriteAtlas.SetAtlasSize(_AtlasRows, _AtlasColumns);
        Console.Write("nah");
        for(uint i = 0; i < _AtlasRows; i++) {
            for(uint j = 0; j < _AtlasColumns; j++) {
                LoadSprite(i, j);
            }
        }
        Raylib.UnloadImage(instance._atlasImage);
        instance.Dispose();
    }

    /// <summary>
    /// Method <c>LoadSprite</c> is responsible for retrieving the desired slice and adding it to the Atlas.
    /// </summary>
    /// <param name="i">Sprite row</param>
    /// <param name="j">Sprite column</param>
    /// <returns>Texture2D slice of the atlas in the desired position.</returns>
    private static void LoadSprite(uint i, uint j) {
        Rectangle imageBounds = new Rectangle(j * _SpriteWidth, i * _SpriteHeight, _SpriteWidth, _SpriteHeight);
        Image image = Raylib.ImageFromImage(instance._atlasImage, imageBounds);
        Texture2D texture = Raylib.LoadTextureFromImage(image);
        Raylib.UnloadImage(image);
        SpriteAtlas.SetSprite(texture, i, j);
    }

    public void Dispose() { }
}