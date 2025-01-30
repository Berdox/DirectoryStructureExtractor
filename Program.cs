
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class Program {
    private static Dictionary<string, object> GetDirectoryStructure(string path) {
        var structure = new Dictionary<string, object>();

        foreach (var dir in Directory.GetDirectories(path)) {
            structure[Path.GetFileName(dir)] = GetDirectoryStructure(dir);
        }

        var files = new List<string>();
        foreach (var file in Directory.GetFiles(path)) {
            files.Add(Path.GetFileName(file));
        }

        if (files.Count > 0)
            structure["files"] = files;

        return structure;
    }

    public static void Main(String[] args) {

        if(args.Length < 0) {
            Console.WriteLine("Need to pass a directory path as an argument");
            return;
        } 
        string rootDirectory = args[0];
        var structure = GetDirectoryStructure(rootDirectory);

        string json = JsonConvert.SerializeObject(structure, Formatting.Indented);
        File.WriteAllText("directory_structure.json", json);

        Console.WriteLine("Directory structure saved to directory_structure.json");
    }
}
