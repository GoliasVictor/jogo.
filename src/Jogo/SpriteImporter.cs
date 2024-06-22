
using Raylib_cs;

/// <summary>
/// Responsible for importing the sprites into the game via the sprite atlas.
/// </summary>
public class SpriteImporter {
    private const string _AtlasPath = "Assets/sprite-atlas.png";
    private const int _SpriteWidth = 16;
    private const int _SpriteHeight = 16;

    private static SpriteImporter instance = new SpriteImporter();

    private Image _atlas;

    /// <summary>
    /// The instance of the <see cref="SpriteImporter"/> class and loads necessary sprites.
    /// </summary>
    /// <param name="position">The initial position of the player entity.</param>
    private SpriteImporter() {
        _atlas = Raylib.LoadImage(_AtlasPath);
    }

    /// <summary>
    /// Method <c>GetSprite</c> is responsible for restrieving the desired slice of the atlas.
    /// </summary>
    /// <param name="i">Sprite row</param>
    /// <param name="j">Sprite column</param>
    /// <returns>Texture2D slice of the atlas in the desired position.</returns>
    public static Texture2D GetSprite(int i, int j) {
        Rectangle imageBounds = new Rectangle(j * _SpriteWidth, i * _SpriteHeight, _SpriteWidth, _SpriteHeight);
        Image image = Raylib.ImageFromImage(instance._atlas, imageBounds);
        Texture2D texture = Raylib.LoadTextureFromImage(image);
        Raylib.UnloadImage(image);
        return texture;
    }
    
}