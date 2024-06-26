class Button: UnitaryContainer {
    
	public event EventHandler? ButtonPressed;
    
	public Color Color = color;

    public Color HoverColor = hoverColor ?? color;
    
	public bool IsHover  {get; private set;}
    
	public override void Update(){
        if ( <Verifica mouse encima> ){ 
            IsHover = true;
            if (<Verifica mouse Apertado>){
                ButtonPressed?.Invoke(this, EventArgs.Empty);
            }
        } else {
            IsHover = false;
        }
    } 
    public override void Render()
    {
		/// Desenha botão
        Raylib.DrawRectangle(..); 

		/// Desenha os filhos
        base.Render();
    }

    public override Vector2 GetPositionChild(UIComponent component)
    {
        return GetPosition();
    }
}