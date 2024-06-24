using System.Numerics;

abstract class UIComponent(Container? parent)
{
    public abstract Vector2 Size { get; } 
    public Container? Parent { get; set; } = parent;
    public virtual void Update(){

    }
    public abstract void Render();
    
}