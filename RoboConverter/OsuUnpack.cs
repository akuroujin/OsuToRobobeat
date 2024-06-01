using System.IO.Compression;

public static class OsuUnpack{
    private const string exportPath = "./osuExport";
    public static void Unpack(string path, string to){
        
        if(path[path.Length-1] == '/' || !path.Contains('.'))
            path = FileSelector.ListFiles("osz", path);

        if(path == null){
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("No osu! files found in the folder.");
            Console.ForegroundColor = ConsoleColor.White;
            return;  
        }
      

        if(Directory.Exists(exportPath)) Directory.Delete(exportPath, true);
        Directory.CreateDirectory(exportPath);
        ZipFile.ExtractToDirectory(path, exportPath);
        
        string difficulty = FileSelector.ListFiles("osu", exportPath);
        if(difficulty == null)
            return;
        
        OsuMap map = new OsuMap(difficulty);
        RoboCasette casette = new RoboCasette(map);

        RoboPack.Pack(to, casette);
        Directory.Delete(exportPath, true);
    }
}