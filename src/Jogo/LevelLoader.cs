using System.Diagnostics;
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
        - >
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
        if(string.IsNullOrEmpty(yamlMap)) yamlMap = SampleLevel;
        Console.WriteLine(yamlMap);
        // Deserialize
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .Build();

        var MapDefs = deserializer.Deserialize<List<string>>(yamlMap);

        string [] map_rows = MapDefs[0].Split(", ");

        List<string[]> tokenMap = [];
        
        foreach (string line in map_rows) {
            string [] current_row = line.Split();
            tokenMap.Add(current_row);
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

    public static string ParseMap(Map map) {
        string fullParse = "";
        for(int i = 0; i < 10; i++) {
            string line = "";
            for(int j = 0; j < 10; j++) {
                IEntity? entity = (map[i, j].Count() != 0)? map[i, j].First() : null;
                if(entity != null) {
                    line += entity.GetType().Name switch {
                        "Player" => LevelToken.Player,
                        "Enemy" =>
                            LevelToken.Enemy +
                            ((Enemy) entity).Element switch {
                                Element.Water => LevelToken.Water,
                                Element.Leaf => LevelToken.Grass,
                                Element.Fire => LevelToken.Fire,
                                _ => LevelToken.None
                            } +
                            ((((Enemy) entity).direction.j != 0)? LevelToken.Horizontal : LevelToken.Vertical),
                        "Box" => LevelToken.Grass,
                        "FireEntity" => LevelToken.Fire,
                        "WaterEntity" => LevelToken.Water,
                        "DoorEntity" => LevelToken.Door,
                        "Wall" => LevelToken.Wall,
                        "Key" => LevelToken.Key,
                        "ElementChangerEntity1" => LevelToken.Item +
                            ((ElementChangerEntity) entity).Element switch {
                                Element.Water => LevelToken.Water,
                                Element.Leaf => LevelToken.Grass,
                                Element.Fire => LevelToken.Fire,
                                _ => LevelToken.None
                            },
                        _ => "_"
                    };
                }else {
                    line += '_';
                }
                if(j < 9)
                    line += ' ';
            }
            if(i < 9) line += ", ";
            fullParse += line;
        }
        List<string> m = [fullParse];
        return new SerializerBuilder().Build().Serialize(m);
    }
}
