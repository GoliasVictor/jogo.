using System.Numerics;
using Raylib_cs;

class ErrorScene : UIScene {
    public ErrorScene(string errorMesage) : base(null!){

        Vector2 ContainerSize = new Vector2(GameSystem.DefaultWindowWidth, GameSystem.DefaultWindowHeight);

        this.root = new FixedPositionContainer(
            position: Vector2.Zero,
            component: new AlignedPositionContainer(
                size: ContainerSize,
                horizontalAlign: HorizontalAlign.Center,
                verticalAlign: VerticalAlign.Center,
                component: new UIText(errorMesage,Color.Red, 30,2)
            )
        );
    }
}