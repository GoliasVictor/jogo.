class LevelScene: IScene
{
    // Campos de detalhes de execução
    public Player Player => { };
    public Map Map { get; private init; };
    ISystem<LevelScene>[] systems;
    public Camera2D _camera { get; private set;};

    public void Update()
    {
        foreach (var system in systems)
            system.Update(this);
    }
    public void Render()
    {
        //  Prepara renderização
        foreach (var entity in <Entidades por layer>)
        {
            //  Calcula posição renderiza o chão 
            entity.Render(this, x, y);
        }
        //  Finaliza renderização
    }  
}   