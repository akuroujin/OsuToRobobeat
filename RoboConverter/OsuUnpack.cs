using System.IO.Compression;

public static class OsuUnpack{
    private const string exportPath = "./osuExport";
    internal static OsuMap Unpack(string path){
        
        if(path[path.Length-1] == '/' || !path.Contains('.'))
            path = FileSelector.ListFiles("osz", path);

        if(path == null){
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("No osu! files found in the folder.");
            Console.ForegroundColor = ConsoleColor.White;
            return null;  
        }
      

        if(Directory.Exists(exportPath)) Directory.Delete(exportPath, true);
        Directory.CreateDirectory(exportPath);
        ZipFile.ExtractToDirectory(path, exportPath);
        
        string difficulty = FileSelector.ListFiles("osu", exportPath);
        if(difficulty == null)
            return null;
        
        OsuMap map = new OsuMap(difficulty);


        
        return map;
    }
    public static void Clean(){
        Directory.Delete(exportPath, true);
    }
}