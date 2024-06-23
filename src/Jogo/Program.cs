using Raylib_cs;
using Jogo.Systems;


/// <sumary>
/// Static class Responsible for starting and running the game.  
///</sumary>
static class GameSystem {
    public const int DefaultWindowWidth = 800;
    public const int DefaultWindowHeight = 600;
    private const string DefaultWindowName = "Elements";
    public const int TileSize = 30;

    private static Color ClearColor = Color.DarkGray;
    private static int targetFPS = 60;
    private static IScene currentScene;
    private static Audio audio = new();

    static GameSystem()
    {
        currentScene = MockLevel();
    }

    private static LevelScene MockLevel()
    {
        return new LevelScene([new TickUpdateSystem(), new CollisionSystem(), new ItemCollectionSystem()], new Map(10, 10, [
            new Wall(new GridVec2(0,0)),
            new Wall(new GridVec2(1,0)),
            new Wall(new GridVec2(2,0)),
            new Wall(new GridVec2(3,0)),
            new Wall(new GridVec2(4,0)),
            new Wall(new GridVec2(5,0)),
            new Wall(new GridVec2(6,0)),
            new Wall(new GridVec2(7,0)),
            new Wall(new GridVec2(8,0)),
            new Wall(new GridVec2(9,0)),
            new Wall(new GridVec2(0,9)),
            new Wall(new GridVec2(1,9)),
            new Wall(new GridVec2(2,9)),
            new Wall(new GridVec2(3,9)),
            new Wall(new GridVec2(4,9)),
            new Wall(new GridVec2(5,9)),
            new Wall(new GridVec2(6,9)),
            new Wall(new GridVec2(7,9)),
            new Wall(new GridVec2(8,9)),
            new Wall(new GridVec2(9,9)),


            new Wall(new GridVec2(0,1)),
            new Wall(new GridVec2(0,2)),
            new Wall(new GridVec2(0,3)),
            new Wall(new GridVec2(0,4)),
            new Wall(new GridVec2(0,5)),
            new Wall(new GridVec2(0,6)),
            new Wall(new GridVec2(0,7)),
            new Wall(new GridVec2(0,8)),
            new Wall(new GridVec2(9,1)),
            new Wall(new GridVec2(9,2)),
            new Wall(new GridVec2(9,3)),
            new Wall(new GridVec2(9,4)),
            new Wall(new GridVec2(9,5)),
            new Wall(new GridVec2(9,6)),
            new Wall(new GridVec2(9,7)),
            new Wall(new GridVec2(9,8)),


            new Wall(new GridVec2(1,5)),
            new Wall(new GridVec2(2,5)),
            new Wall(new GridVec2(3,5)),
            new Wall(new GridVec2(4,5)),
            new Wall(new GridVec2(6,5)),
            new Wall(new GridVec2(7,5)),

            new Wall(new GridVec2(6,6)),
            new Wall(new GridVec2(6,7)),
            new Wall(new GridVec2(6,8)),

            new DoorEntity(new GridVec2(8,5)),
            new Box(new GridVec2(7,2)),

            new Box(new GridVec2(5,5)),

            new FireEntity(new GridVec2(5,6)),
            new FireEntity(new GridVec2(5,7)),
            new FireEntity(new GridVec2(5,8)),

            new FireEntity(new GridVec2(4,7)),
            new FireEntity(new GridVec2(4,8)),

            new WaterEntity(new GridVec2(1,2)),
            new WaterEntity(new GridVec2(2,2)),
            new WaterEntity(new GridVec2(2,1)),
            new Key(new GridVec2(1,1)),

            new ElementChangerEntity(new GridVec2(5,1), Element.Leaf),
            new ElementChangerEntity(new GridVec2(5,2), Element.Fire),
            new ElementChangerEntity(new GridVec2(1,8), Element.Water),
            new Enemy(new GridVec2(3,4), Element.Leaf, false),
            new Enemy(new GridVec2(6,3), Element.Water, true),
            new Enemy(new GridVec2(2,6), Element.Fire, true),
            new Player(new GridVec2(4,3)),
        ]));
    }

    static void Main() {
        Raylib.InitWindow(DefaultWindowWidth, DefaultWindowHeight, DefaultWindowName);

        Raylib.SetTargetFPS(targetFPS);
        Raylib.InitAudioDevice();
        audio.PlayMusic(IAudio.MusicEffect.TitleScreen);

        while (!Raylib.WindowShouldClose())
        {
            Update();
            Render();
        }

        Raylib.CloseAudioDevice();
        Raylib.CloseWindow();
    }


    /// <summary>
    /// <para> Called inside the Game Loop for handling logic.</para>
    /// <para> Calls <c>GameSystem.UpdateObjects()</c> and <c>GameSystem.UpdateUI()</c>.</para>
    /// <seealso cref="M:Game.GameSystem.UpdateObjects"/>
    /// <seealso cref="M:Game.GameSystem,UpdateUI"/> 
    /// </summary>
    private static void Update() {
        UpdateObjects();
        if (currentScene is LevelScene level && level.Player.PlayerKilled){
            currentScene = MockLevel();
        }
        UpdateUI();
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
            RenderObjects();
            RenderUI();

        Raylib.EndDrawing();
    }

    /// <summary>
    /// <para> Called by <c>GameSystem.Update()</c>.</para>
    /// <para> Call <c>Object.Update()</c> for every <c>Object</c> in list.</para>
    /// <seealso cref="M:Game.GameSystem.Update"/> 
    /// <seealso cref="M:Game.GameSystem.UpdateUI"/> 
    /// <seealso cref="M:Game.GameSystem.RenderObjects"/>
    /// </summary>
    private static void UpdateObjects() {
        currentScene.Update();
        audio.Update();
    }

    /// <summary>
    /// <para> Called by <c>GameSystem.Update()</c>.</para>
    /// <para> Call <c>UI.Update()</c> for every <c>UI Element</c> in list.</para>
    /// <seealso cref="M:Game.GameSystem.Update"/> 
    /// <seealso cref="M:Game.GameSystem.UpdateObject"/> 
    /// <seealso cref="M:Game.GameSystem.RenderUI"/>
    /// </summary>
    private static void UpdateUI() {

    }

    /// <summary>
    /// <para> Called by <c>GameSystem.Render()</c>.</para>
    /// <para> Call <c>Object.Render()</c> for every <c>Object</c> in list.</para>
    /// <seealso cref="M:Game.GameSystem.Render"/> 
    /// <seealso cref="M:Game.GameSystem.RenderUI"/>
    /// <seealso cref="M:Game.GameSystem.UpdateObjects"/> 
    /// </summary>
    private static void RenderObjects() {
        currentScene.Render();
    }

    /// <summary>
    /// <para> Called by <c>GameSystem.Render()</c>.</para>
    /// <para> Call <c>UI.Render()</c> for every <c>UI Element</c> in list.</para>
    /// <seealso cref="M:Game.GameSystem.Render"/> 
    /// <seealso cref="M:Game.GameSystem.UpdateUI"/> 
    /// <seealso cref="M:Game.GameSystem.RenderObjects"/>
    /// </summary>
    private static void RenderUI() {

    }
}
