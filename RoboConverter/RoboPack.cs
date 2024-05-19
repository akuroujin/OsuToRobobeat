using System.IO.Compression;

class RoboPack{   

    public static void Pack(string path, RoboCasette casette){
        if(Directory.Exists("./roboPack")) Directory.Delete("./roboPack", true);
        Directory.CreateDirectory("./roboPack");
        casette.WriteToFile($"./roboPack/[{casette.title}].cassette");

        if(!File.Exists("./osuExport/audio.mp3")){
            File.Copy( SongSelector(), $"./roboPack/{casette.title}.mp3");
        }
        else
            File.Copy("./osuExport/audio.mp3", $"./roboPack/{casette.title}.mp3");
        
        if(File.Exists(path + casette.title + ".robobeat")) File.Delete(path + casette.title + ".robobeat");

        ZipFile.CreateFromDirectory("./roboPack", path + casette.title + ".robobeat");

        Directory.Delete("./roboPack", true);
    }
    

    private static string SongSelector(){

        string[] difficulty = Directory.GetFiles("./osuExport", "*.mp3");
        if (difficulty.Length == 0)
        {
            difficulty = Directory.GetFiles("./osuExport", "*.ogg");
        }
        Console.WriteLine("Select Song to Convert");
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

