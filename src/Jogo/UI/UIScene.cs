using System.Numerics;
using Raylib_cs;
class UIScene(Container root) : IScene
{
    public Container root = root;
    public static Button ButtonText(string Text){
        Vector2 bsize =  new (100, 30);
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

    public void Render()
    {
        root.Render();
    }

    public void Update()
    {
        root.Update();
    }
}