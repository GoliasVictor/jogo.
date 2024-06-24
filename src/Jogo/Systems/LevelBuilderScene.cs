using Raylib_cs;

/// <summary>
/// A tool for creating and editing level in the game
/// </summary>
public class LevelBuilderScene : IScene
{
    private static class BuilderKey {
        public const KeyboardKey Player = KeyboardKey.Zero;
        public const KeyboardKey Wall = KeyboardKey.One;
        public const KeyboardKey Block = KeyboardKey.Two;
        public const KeyboardKey Enemy = KeyboardKey.Three; 
        public const KeyboardKey Item = KeyboardKey.Four;
        public const KeyboardKey Key = KeyboardKey.Five;
        public const KeyboardKey Door = KeyboardKey.Six;
        public const KeyboardKey Goal = KeyboardKey.Nine;
        public const KeyboardKey Fire = KeyboardKey.Kp1;
        public const KeyboardKey Water = KeyboardKey.Kp2;
        public const KeyboardKey Grass = KeyboardKey.Kp3;
    }


    private const string CustomLevelPath = @"Levels/custom-levels.yaml";

    private bool _isTesting = false;
    private Map _map;
    private LevelScene _testScene;
    private string _mapBackup = "";
    private GridVec2 _selection = GridVec2.ZERO;
    private int index;

    private bool IsTesting {
        get => _isTesting;
        set {
            if(value) {
                _mapBackup = LevelLoader.ParseMap(_map);
            }else{
                _map = LevelLoader.LoadFromString(_mapBackup);
                _testScene = new LevelScene([new TickUpdateSystem(), new CollisionSystem(), new ItemCollectionSystem()], _map);
            }
            _isTesting = value;
        }
    }

    public LevelBuilderScene(int index) {
        this.index = index;
        _map = LevelLoader.LoadFromYaml(CustomLevelPath, index);
        _mapBackup = LevelLoader.ParseMap(_map);
        _testScene = new LevelScene([new TickUpdateSystem(), new CollisionSystem(), new ItemCollectionSystem()], _map);
    }

    public void Update()
    {
        if(Raylib.IsKeyPressed(KeyboardKey.Enter)) IsTesting = !IsTesting;

        if(IsTesting) {
            _testScene.Update();
            if (_testScene.Player.PlayerKilled){
                _testScene = new LevelScene([new TickUpdateSystem(), new CollisionSystem(), new ItemCollectionSystem()], _map);
                IsTesting = false;
            }
        }else {
            _selection.i += (int) Raylib.IsKeyPressed(KeyboardKey.Down) - (int) Raylib.IsKeyPressed(KeyboardKey.Up);
            _selection.j += (int)(Raylib.IsKeyPressed(KeyboardKey.Right) - Raylib.IsKeyPressed(KeyboardKey.Left));

            if(_selection.i < 0) _selection.i = 9;
            if(_selection.j < 0) _selection.j = 9;
            if(_selection.i >= 10) _selection.i = 0;
            if(_selection.j >= 10) _selection.j = 0;
        }
    }

    public void Render()
    {
        _testScene.Render();
        if(IsTesting) {
            Raylib.DrawText("Testing...", 0, 0, 20, Color.RayWhite);
        }else {
            Raylib.BeginMode2D(_testScene._camera);

                Raylib.DrawRectangleLines(_selection.j * GameSystem.TileSize, _selection.i * GameSystem.TileSize, GameSystem.TileSize, GameSystem.TileSize, Color.RayWhite);

            Raylib.EndMode2D();
        }
        Raylib.DrawFPS(0,120);
    }

    public void ViewSizeChanged() {
        _testScene.ViewSizeChanged();
    }

}