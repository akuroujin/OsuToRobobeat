using System.IO.Compression;

public static class OsuUnpack{
    
    public static void Unpack(string from, string to){
        if(Directory.Exists("./osuExport")) Directory.Delete("./osuExport", true);
        Directory.CreateDirectory("./osuExport");
        ZipFile.ExtractToDirectory(from, "./osuExport");
        
        string difficulty = DiffiCultySelection();
        
        OsuMap map = new OsuMap(difficulty);
        RoboCasette casette = new RoboCasette(map);


        RoboPack.Pack(to, casette);
        Directory.Delete("./osuExport", true);
    }
    private static string DiffiCultySelection(){
        string[] difficulty = Directory.GetFiles("./osuExport", "*.osu");
        Console.WriteLine("Select Difficulty to Convert");
        for (int i = 0; i < difficulty.Length; i++)        {
            Console.WriteLine($"{i+1}. - {difficulty[i]}");
        }
        Console.Write("Selection: ");        
        int selection = -1;
        
        while(selection < 0 || selection > difficulty.Length){
            selection = int.Parse(Console.ReadLine())-1;
        }

        return difficulty[selection];
    }
}