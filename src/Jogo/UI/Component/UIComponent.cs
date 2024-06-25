using System.Numerics;

abstract class UIComponent(Container? parent)
{
    public abstract Vector2 Size { get; } 
    public Container? Parent { get; set; } = parent;

    public Vector2 GetPositionChild(){
        return Parent?.GetPosition(this) ?? throw new WithoutParentException(this);
    }
    public virtual void Update(){

    }
    public abstract void Render();
    
}