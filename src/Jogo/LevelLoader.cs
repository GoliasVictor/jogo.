using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

/// <summary>
/// Contains predefined token chars for level building.
/// </summary>
static class LevelToken {
    public const char Player = 'p';
    public const char Wall = 'w';
    public const char Goal = 'g';
    public const char Enemy = 'e';
    public const char Water = '1';
    public const char Grass = '2';
    public const char Fire = '3';
    public const char Horizontal = 'h';
    public const char Vertical = 'v';
    public const char Item = 'i';
    public const char Key = 'k';
    public const char Door = 'd';
    public const char None = '_';

}

/// <summary>
/// Responsible for loading and storing levels from yaml files.
/// </summary>
static class LevelLoader
{
    private const string SampleLevel = """
        ---
        map: >
            w w w w w w w w w w,
            w p _ _ _ _ _ _ _ w,
            w _ _ _ _ _ _ _ _ w,
            w _ _ _ _ _ _ _ _ w,
            w _ _ _ _ _ _ _ _ w,
            w _ _ _ _ _ _ _ _ w,
            w _ _ _ _ _ _ _ _ w,
            w _ _ _ _ _ _ _ _ w,
            w _ _ _ _ _ _ _ g w,
            w w w w w w w w w w
        ...
        """;

    /// <summary>
    /// Loads a YAML string into a Map.
    /// </summary>
    /// <param name="yamlMap">Content of the YAML file.</param>
    /// <returns>The full loaded map.</returns>
    public static Map Load(string yamlMap)
    {
        yamlMap = SampleLevel;
        // Deserialize
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .Build();

        var MapDefs = deserializer.Deserialize<Dictionary<string, string>>(yamlMap);

        string [] map_rows = MapDefs["map"].Split(", ");

        List<string[]> tokenMap = [];
        
        foreach (string line in map_rows) {
            string [] current_row = line.Split();
            tokenMap.Add(current_row);
            foreach(string token in current_row) Console.WriteLine(token);
        }

        List<IEntity> map_entities = [];

        for (int i = 0; i < 10; i++) {
            for (int j = 0; j < 10; j++) {
                GridVec2 position = new GridVec2(i, j);
                IEntity? entity = tokenMap[i][j][0] switch {
                    LevelToken.Player => new Player(position),
                    LevelToken.Door => new DoorEntity(position),
                    LevelToken.Wall => new Wall(position),
                    LevelToken.Enemy => tokenMap[i][j][1] switch {
                        LevelToken.Water => new Enemy(position, Element.Water, tokenMap[i][j][2] == LevelToken.Horizontal),
                        LevelToken.Grass => new Enemy(position, Element.Leaf, tokenMap[i][j][2] == LevelToken.Horizontal),
                        LevelToken.Fire => new Enemy(position, Element.Fire, tokenMap[i][j][2] == LevelToken.Horizontal),
                        _ => null
                    },
                    LevelToken.Item => tokenMap[i][j][1] switch {
                        LevelToken.Water => new ElementChangerEntity(position, Element.Water),
                        LevelToken.Grass => new ElementChangerEntity(position, Element.Leaf),
                        LevelToken.Fire => new ElementChangerEntity(position, Element.Fire),
                        _ => null
                    },
                    LevelToken.Water => new WaterEntity(position),
                    LevelToken.Grass => new Box(position),
                    LevelToken.Fire => new FireEntity(position),
                    _ => null
                };
                if(entity != null) map_entities.Add(entity);
            }
        }
        return new Map(10, 10, map_entities);
    }
}
