using Raylib_cs;
using Jogo.Systems;


/// <sumary>
/// Static class Responsible for starting and running the game.  
///</sumary>
static class GameSystem {
    public const int DefaultWindowWidth = 800;
    public const int DefaultWindowHeight = 600;
    private const string DefaultWindowName = "Elements";
    public const int TileSize = 16;

    private static Color ClearColor = Color.Black;
    private static int targetFPS = 60;
    private static Audio audio = new();
    public static IScene currentScene;
    public static bool ShouldExit = false;

    static GameSystem()
    {
        currentScene = new MainMenuScene();
    }

    static void Main() {
        try
        {


            Raylib.InitWindow(DefaultWindowWidth, DefaultWindowHeight, DefaultWindowName);
            Raylib.SetExitKey(KeyboardKey.Null);

            currentScene.ViewSizeChanged();

            Raylib.SetTargetFPS(targetFPS);
            Raylib.InitAudioDevice();

            audio.PlayMusic(IAudio.MusicEffect.TitleScreen);


            try
            {
                SpriteAtlas.LoadAtlas();
            }
            catch(ResourceLoadException _)
            {
                currentScene = new ErrorScene("!! ERRO !!\n\nVerifique se os arquivos do jogo\n\nestão presentes no local certo.");
                Console.WriteLine("Falha ao carregar os sprites, verifique se os arquivos do jogo estão presentes no local certo.");
            }
            
            


            while (!ShouldExit)
            {
                try {
                    Update();
                    Render();
                } 
                catch {
                    currentScene = new ErrorScene("!! Aconteceu algum erro no jogo !!\n\nse possivel reporte para os mantenedores.");
                }
                if (Raylib.WindowShouldClose())
                    ShouldExit = true;
            }

            Raylib.CloseAudioDevice();
            Raylib.CloseWindow();
        } catch {
            Console.WriteLine("Aconteceu algum erro no jogo, se possivel reporte para os mantenedores.");
        }
    }


    /// <summary>
    /// <para> Called inside the Game Loop for handling logic.</para>
    /// <para> Calls <c>GameSystem.UpdateObjects()</c> and <c>GameSystem.UpdateUI()</c>.</para>
    /// <seealso cref="M:Game.GameSystem.UpdateObjects"/>
    /// <seealso cref="M:Game.GameSystem,UpdateUI"/> 
    /// </summary>
    private static void Update() {
        currentScene.Update();
        audio.Update();
    }

    /// <summary>
    /// <para> Called inside the Game Loop for rendering items on screen.</para>
    /// <para> Renders Objects first, then UI.</para>
    /// <seealso cref="M:Game.GameSystem.RenderObjects"/>
    /// <seealso cref="M:Game.GameSystem.RenderUI"/> 
    /// </summary>
    private static void Render() {
        Raylib.BeginDrawing();
            Raylib.ClearBackground(ClearColor);
            currentScene.Render();


        Raylib.EndDrawing();
    }


}
