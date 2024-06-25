using System.Numerics;

/// <summary>
/// User Interface for <seealso cref="LevelBuilderScene"/>.
/// </summary>
class LevelBuilderUI : UIScene
{
    private LevelBuilderScene levelBuilder;

    private Button menuButton = ButtonText("Menu");
    private Button saveButton = ButtonText("Salvar");
    private Button testButton = ButtonText("Testar");
    private Button nextButton = ButtonText(">");
    private Button previousButton = ButtonText("<");

    private UIText testButtonText;

    /// <summary>
    /// Generates new UI elements for the Level Builder.
    /// </summary>
    /// <param name="levelBuilder">The LevelBuilder to be assigned.</param>
    public LevelBuilderUI(LevelBuilderScene levelBuilder) : base(null!)
    {
        this.levelBuilder = levelBuilder;
        Vector2 ContainerSize = new Vector2(GameSystem.DefaultWindowWidth, GameSystem.DefaultWindowHeight);
        Vector2 size = new Vector2(80, 40);
        Vector2 bsize =  new (100, 30);
        var comandbar = new FlexboxContainer(
                size: new(300, 200),
                axis: Axis.Horizontal,
                justifyMain: JustifyMode.End,
                gap: 10,
                components: [
                    nextButton,
                    previousButton,
                    testButton,
                    saveButton,
                    menuButton,
                ]
            );
        this.root = new FixedPositionContainer(
            position: Vector2.Zero,
            component: new AlignedPositionContainer(
                size: ContainerSize,
                horizontalAlign: HorizontalAlign.Right,
                verticalAlign: VerticalAlign.Top,
                component: new MarginContainer(
                    margin: 10,
                    component: comandbar
                )
            )
        );

        menuButton.ButtonPressed += OnMenuButtonPressed;
        saveButton.ButtonPressed += OnSaveButtonPressed;
        testButton.ButtonPressed += OnTestButtonPressed;
        previousButton.ButtonPressed += OnPreviousButtonPressed;
        nextButton.ButtonPressed += OnNextButtonPressed;

        
        testButtonText = (UIText) ((AlignedPositionContainer) testButton.Component).Component;

    }

    public new void Update() {
        base.Update();
        testButtonText.Text = (levelBuilder.IsTesting)? "Parar" : "Testar";
    }

    private void OnNextButtonPressed(object? sender, EventArgs e)
    {
        levelBuilder.index++;
    }

    private void OnTestButtonPressed(object? sender, EventArgs e)
    {
        levelBuilder.IsTesting = !levelBuilder.IsTesting;
    }

    private void OnSaveButtonPressed(object? sender, EventArgs e)
    {
        levelBuilder.SaveMap();
    }

    private void OnMenuButtonPressed(object? sender, EventArgs e)
    {
        GameSystem.currentScene = new MainMenuScene();
    }

    private void OnPreviousButtonPressed(object? sender, EventArgs e)
    {
        levelBuilder.index--;
    }
}