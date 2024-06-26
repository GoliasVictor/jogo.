class LevelBuilderUI : UIScene
{
    private Button saveButton = ButtonText("Salvar");
 
    public LevelBuilderUI(LevelBuilderScene levelBuilder)
    {

        var comandbar = new FlexboxContainer(
            // Parametro flexbox
            components: [
                nextButton, previousButton,
                testButton, saveButton, menuButton,
            ]
        );
        this.root = new FixedPositionContarootiner(
            position: Vector2.Zero,
            component: new AlignedPositionContainer(
                /// Parametros Alinhamento
                component: new MarginContainer(
                    margin: 10,
                    component: comandbar
                )
            )
        );

        saveButton.ButtonPressed += OnSaveButtonPressed;
        // Inicializa o resto das coisas nescessarias
    }
 
    private void OnSaveButtonPressed(object? sender, EventArgs e) {
        levelBuilder.SaveMap();
    }  
}