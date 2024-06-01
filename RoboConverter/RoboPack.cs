using System.IO.Compression;

static class RoboPack{   
    private const string exportPath = "./roboPack";
    private const string osuExport = "./osuExport";

    public static void Pack(string path, RoboCasette casette){
        if(Directory.Exists(exportPath)) Directory.Delete("./roboPack", true);
        Directory.CreateDirectory(exportPath);
        casette.WriteToFile($"{exportPath}/[{casette.title}].cassette");

        string song;
        string fileFormat = "mp3";
        song =  FileSelector.ListFiles("mp3", osuExport);

        if(song == null){
            song =  FileSelector.ListFiles("ogg", osuExport);
            fileFormat = "ogg";
        }
            
        if(song == null){
            song =  FileSelector.ListFiles("wav", osuExport);
            fileFormat = ".wav";
        }

        if(song == null){
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine("No usable audio files found...");
            Console.ForegroundColor = ConsoleColor.White;
            return;
        }

        //fileFormat = fileFormat; - the funny line

        File.Copy( song, $"{exportPath}/{casette.id}.{fileFormat}");
        
        if(File.Exists(path + casette.title + ".robobeat")) File.Delete(path + casette.title + ".robobeat");

        ZipFile.CreateFromDirectory(exportPath, path + casette.title + ".robobeat");

        Directory.Delete(exportPath, true);
    }
}

