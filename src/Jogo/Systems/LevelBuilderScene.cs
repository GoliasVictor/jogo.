using Raylib_cs;

public class LevelBuilderScene : IScene
{
    private bool _isTesting = false;
    private Map _map;
    private LevelScene _testScene;
    private string _mapBackup = "";
    private GridVec2 _selection = GridVec2.ZERO;

    private bool IsTesting {
        get => _isTesting;
        set {
            if(value) {
                _mapBackup = LevelLoader.ParseMap(_map);
            }else{
                _map = LevelLoader.Load(_mapBackup);
                _testScene = new LevelScene([new TickUpdateSystem(), new CollisionSystem(), new ItemCollectionSystem()], _map);
            }
            _isTesting = value;
        }
    }

    public LevelBuilderScene() {
        _map = LevelLoader.Load("");
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
    }

    public void ViewSizeChanged() {
        _testScene.ViewSizeChanged();
    }

}