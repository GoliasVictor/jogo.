using System.Numerics;
using Raylib_cs;

/// <summary>
/// Game's main menu.
/// </summary>
class MainMenuScene : UIScene
{
    /// <summary>
    /// Generates new UI for the Main menu.
    /// </summary>
    public MainMenuScene () : base(null!) {
        Vector2 ContainerSize = new Vector2(GameSystem.DefaultWindowWidth, GameSystem.DefaultWindowHeight);
        Button newGameButton = MenuButtonText("Novo Jogo");
        Button levelBuilderButton = MenuButtonText("Construtor de níveis");
        Button exitButton = MenuButtonText("Sair");

        newGameButton.ButtonPressed += OnPlayButtonPressed;
        levelBuilderButton.ButtonPressed += OnLevelBuilderButtonPressed;
        exitButton.ButtonPressed += OnExitButtonPressed;

        var menuButtons = new FlexboxContainer(
                size: new(220, 120),
                axis: Axis.Vertical,
                justifyMain: JustifyMode.Start,
                gap: 10,
                components: [
                    newGameButton,
                    levelBuilderButton,
                    exitButton,
                ]
            );
        this.root = new FixedPositionContainer(
            position: Vector2.Zero,
            component: new AlignedPositionContainer(
                size: ContainerSize,
                horizontalAlign: HorizontalAlign.Center,
                verticalAlign: VerticalAlign.Center,
                component: menuButtons
            )
        );
    }

    private void OnPlayButtonPressed(object? sender, EventArgs e)
    {
        GameSystem.currentScene = new LevelManagerScene(0);
    }

    private void OnLevelBuilderButtonPressed(object? sender, EventArgs e)
    {
        GameSystem.currentScene = new LevelBuilderScene(0);
    }

    private void OnExitButtonPressed(object? sender, EventArgs e)
    {
        GameSystem.ShouldExit = true;
    }

    /// <summary>
    /// Generates a large button with text.
    /// </summary>
    /// <param name="Text"></param>
    /// <returns></returns>
    private Button MenuButtonText(string Text){
        Vector2 bsize =  new (220, 30);
        return new Button(
            color: Color.Red,
            hoverColor: Color.Brown,
            component: new AlignedPositionContainer(
                size: bsize,
                horizontalAlign: HorizontalAlign.Center,
                verticalAlign: VerticalAlign.Center,
                component: new UIText(Text, Color.White, 20)
            )
        );
    }
}