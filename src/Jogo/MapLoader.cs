using System.Reflection.Metadata;
using System.Linq;
using Raylib_cs;

using System;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
//using YamlDotNet.Samples.Helpers;


public class LoadMap
{
    static Map load(string yaml_map)
    {
        // Deserialize
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)
            .Build();

        var MapDefs = deserializer.Deserialize<Dictionary<string, string>>(yaml_map);

        //foreach (tile in MapDefs.tiles) {
        //    //TODO initialize tile types
        //}

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

    private const string Document = """
        ---
            tiles:
                t: &terra 
                    arquivo: "terra.png"
                a: &agua 
                    arquivo: "agua.png"
                    colide : True
            
            map: >
                _ _ _ _ a _
                _ _ _ a a a _ 
                _ _ a a t a a _
                _ a a t t t a a _
                a a t t a t t a a
                _ a a t t t a a _
                _ _ a a t a a _
                _ _ _ a a a _
                _ _ _ _ a _ 
        ...
        """;
}
