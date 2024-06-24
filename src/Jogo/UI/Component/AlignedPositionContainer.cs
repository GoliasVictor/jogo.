using System.Diagnostics;
using System.Numerics;
using System.Reflection.Emit;
enum VerticalAlign {
    Top = 1, 
    Bottom = 2,
    Center = 3
}
enum HorizontalAlign {
    Left = 1,
    Right = 2,
    Center = 3,

}



class AlignedPositionContainer : UnitaryContainer

{
    Vector2 Position;
    VerticalAlign verticalAlign;
    HorizontalAlign horizontalAlign;
    public override Vector2 Size {get; }
    public AlignedPositionContainer(Vector2 size , HorizontalAlign horizontalAlign,VerticalAlign verticalAlign , UIComponent component, Container? parent = null) : base(component, parent)
    {
        Size = size;
        this.verticalAlign = verticalAlign;
        this.horizontalAlign = horizontalAlign;
    }

    public override Vector2 GetPosition(UIComponent component)
    {
        var origin = Parent.GetPosition(this);
        var psize = this.Size;
        var csize = Component.Size;
        var x = horizontalAlign switch
        {
            HorizontalAlign.Left => 0,
            HorizontalAlign.Center => psize.X / 2 - csize.X / 2,
            HorizontalAlign.Right => psize.X - csize.X,
            _ => throw new UnreachableException(),
        };
        var y = verticalAlign switch
        {
            VerticalAlign.Top => 0,
            VerticalAlign.Center => psize.Y / 2 - csize.Y / 2,
            VerticalAlign.Bottom => psize.Y - csize.Y,
            _ => throw new UnreachableException(),
        };

        return origin + new Vector2(x,y);
    }
}