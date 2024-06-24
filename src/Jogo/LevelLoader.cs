using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

/// <summary>
/// Contains predefined token chars for level building.
/// </summary>
static class LevelToken {
    const char Player = 'p';
    const char Wall = 'w';
    const char Goal = 'g';
    const char Enemy = 'e';
    const char Water = '1';
    const char Grass = '2';
    const char Fire = '3';
    const char Horizontal = 'h';
    const char Vertical = 'v';
    const char Item = 'i';
    const char Key = 'k';
    const char Door = 'd';
    const char None = '_';

}

/// <summary>
/// Responsible for loading and storing levels from yaml files.
/// </summary>
static class LevelLoader
{
    private const string SampleLevel = """
        ---
        map: >
            w w w w w w w w w w
            w p _ _ _ _ _ _ _ w
            w _ _ _ _ _ _ _ _ w
            w _ _ _ _ _ _ _ _ w
            w _ _ _ _ _ _ _ _ w
            w _ _ _ _ _ _ _ _ w
            w _ _ _ _ _ _ _ _ w
            w _ _ _ _ _ _ _ g w
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
        if(string.IsNullOrWhiteSpace(yamlMap)) yamlMap = SampleLevel;
        // Deserialize
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .Build();

        var MapDefs = deserializer.Deserialize<Dictionary<string, string>>(yamlMap);

        string [] map_rows = MapDefs["map"].Split("\n");

        int rows = map_rows.Length;
        int columns = 0;

        string [][] token_map = new string[rows][];
        
        foreach (string line in map_rows) {
            string [] current_row = line.Split(' ');
            int length = current_row.Length;

            if (length > columns) {
                columns = length;
            }
            token_map[Array.IndexOf(map_rows,line)] = current_row;
        }

        List<IEntity> map_entities = [];

        for (int i = 0; i < rows; i++) {
            for (int j = 0; j < token_map[i].Length; j++) {
                map_entities.Add(token_map[i][j] switch {
                    "b" => new Box(new GridVec2(i,j)),
                    
                    "d" => new DoorEntity(new GridVec2(i,j)),

                    "f" => new FireEntity(new GridVec2(i,j)),

                    "o" => new Wall(new GridVec2(i,j)),

                    "w" => new WaterEntity(new GridVec2(i,j)),
                });
            }
        }
        return new Map(rows, columns, map_entities);
    }
}
