using System.IO.Compression;

class RoboPack{   

    public static void Pack(string path, RoboCasette casette){
        if(Directory.Exists("./roboPack")) Directory.Delete("./roboPack", true);
        Directory.CreateDirectory("./roboPack");
        casette.WriteToFile($"./roboPack/[{casette.title}].cassette");

        if(!File.Exists("./osuExport/audio.mp3")){
            string song = SongSelector();
            string fileFormat = song.Split('.')[1];
            File.Copy( song, $"./roboPack/{casette.title}.{fileFormat}");
        }
        else
            File.Copy("./osuExport/audio.mp3", $"./roboPack/{casette.title}.mp3");
        
        if(File.Exists(path + casette.title + ".robobeat")) File.Delete(path + casette.title + ".robobeat");

        ZipFile.CreateFromDirectory("./roboPack", path + casette.title + ".robobeat");

        Directory.Delete("./roboPack", true);
    }
    

    private static string SongSelector(){
        string[] audioList = Directory.GetFiles("./osuExport", "*.mp3");
        if (audioList.Length == 0)
        {
            audioList = Directory.GetFiles("./osuExport", "*.ogg");
        }
        if (audioList.Length == 0){
            audioList = Directory.GetFiles("./osuExport", "*.wav");
        }
        Console.WriteLine("Select Song to Convert");
        for (int i = 0; i < audioList.Length; i++)        {
            Console.WriteLine($"{i+1}. - {audioList[i]}");
        }
        
        int selection = -1;

        while(selection < 0 || selection > audioList.Length){
            Console.Write("Selection: ");     
            if(!int.TryParse(Console.ReadLine(), out selection)) continue; 
        }

        return audioList[selection];
    }
}

