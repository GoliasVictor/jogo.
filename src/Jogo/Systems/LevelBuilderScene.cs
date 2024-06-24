using Raylib_cs;

public class LevelBuilderScene : IScene
{
    private bool _isTesting = false;
    private Map _map;
    private LevelScene _testScene;
    private string _mapBackup = "";

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
        }
    }

    public void Render()
    {
        _testScene.Render();
        if(IsTesting) {
            Raylib.DrawText("Testing...", 0, 0, 20, Color.RayWhite);
        }
    }

    public void ViewSizeChanged() {
        _testScene.ViewSizeChanged();
    }

}