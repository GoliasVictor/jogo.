using System.Numerics;
using Raylib_cs;

class Button(Vector2 size, Color color, UIComponent component, Color? hoverColor = null,Container? parent = null) : UnitaryContainer(component, parent) {
    public event EventHandler? ButtonPressed;
    public Color Color = color;
    public Color HoverColor = hoverColor ?? color;
    public bool IsHover  {get; private set;}
    public override void Update(){
# warning Fazer isso lançar um erro
        var pos = Parent.GetPosition(this); 
        var mousepos = Raylib.GetMousePosition();
        if (pos.X < mousepos.X && mousepos.X < pos.X + Size.X 
        &&  pos.Y < mousepos.Y && mousepos.Y < pos.Y + Size.Y){
            IsHover = true;
            if (Raylib.IsMouseButtonPressed(MouseButton.Left)){
                ButtonPressed?.Invoke(this, EventArgs.Empty);
            }
        } else {
            IsHover = false;
        }
    } 
    public override void Render()
    {
        var  position = Parent.GetPosition(this);
        Raylib.DrawRectangle((int)position.X, (int)position.Y, (int)Size.X, (int)Size.Y, IsHover ? HoverColor : Color);
        base.Render();
    }

    public override Vector2 GetPosition(UIComponent component)
    {
        return Parent.GetPosition(this);
    }
}