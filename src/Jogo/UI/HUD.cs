using System.Numerics;
using Raylib_cs;

class HUD : UIScene {
    public Button RestartButton = ButtonText("Reiniciar");
    public Button MenuButton = ButtonText("Menu");

    public HUD() : base((Container)null!) {
        
        Vector2 ContainerSize = new Vector2(GameSystem.DefaultWindowWidth, GameSystem.DefaultWindowHeight);
        Vector2 size = new Vector2(80, 40);
        Vector2 bsize =  new (100, 30);
        var comandbar = new FlexboxContainer(
                size: new(300, 200),
                axis: Axis.Horizontal,
                justifyMain: JustifyMode.End,
                gap: 10,
                components: [
                    RestartButton,
                    MenuButton
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
    }
}