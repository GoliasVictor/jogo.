using System.Diagnostics;
using System.Numerics;


enum Axis {
    Horizontal = 0,
    Vertical = 1
}  

enum JustifyMode {
    Start = 1,
    End = 2,
//    Center = 3,
//    SpaceBetween = 4,
//    SpaceAround = 5,
//    SpaceEvenly = 6
}
class FlexboxContainer : Container
{
    public List<UIComponent> Components { get; private init; }
    public override Vector2 Size { get; } 
    public Axis Axis;
    private int maxis => (int)Axis;
    private int saxis =>(int)(Axis switch
    {
        Axis.Horizontal => Axis.Vertical,
        Axis.Vertical => Axis.Horizontal,
        _ => throw new UnreachableException(),
    });
    public JustifyMode JustifyMain;
    public JustifyMode JustifySecondary;

    public float Gap;

    public FlexboxContainer(Vector2 size,Axis axis,float gap = 0, JustifyMode justifyMain = default, JustifyMode justifySecondary = default, IEnumerable<UIComponent>? components = null, Container? parent = null) : base(parent)
    {
        this.Parent = parent;
        this.Axis = axis;
        this.JustifyMain = justifyMain;
        this.JustifySecondary = justifySecondary;
        Gap = gap;
        this.Size = size;
        Components = components?.ToList() ?? [];
        foreach(var c in Components){
            c.Parent = this; 
        }
    }

    private float WidthRowFrom(int index)
    {
        float width = 0;
        for (int i = index; i < Components.Count; i++)
        {
            var newWidth = width + Components[i].Size.X;
            if (this.Size.X > newWidth)
                break;
            width += newWidth;
        }
        return width;
    }
    public override Vector2 GetPosition(UIComponent component)
    {
        var origin = GetPositionChild();
        var idx = Components.IndexOf(component);
        float secondarySize = 0;
        var pos = new Vector2(0, 0);
        for (int i = 0; i < idx; i++)
        {
            
            secondarySize = Math.Max(secondarySize, Components[i].Size[saxis]);
            pos[maxis] += Components[i].Size[maxis] + Gap  ;
            
            if(pos[maxis] > Size[maxis] || i+1 < Components.Count && pos[maxis] + Components[i+1].Size[maxis] > Size[maxis]){
                pos[saxis] += secondarySize + Gap;
                pos[maxis] = 0;
                secondarySize = 0;
            } 
        
        }
        if(JustifyMain == JustifyMode.End) {
            pos[maxis] = Size[maxis] - pos[maxis] - component.Size[maxis];
        }
        if(JustifySecondary == JustifyMode.End) {
            pos[saxis] = Size[saxis] - pos[saxis] - Gap - secondarySize;
        }
        return origin + pos;
    }
 

    public override List<UIComponent> GetChildren()
    {
        return [.. Components];
    }
}