internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("osu! to RoboCasette Converter V0.2 (prepare for issues!)");
        Console.WriteLine("Made by: @Akuroujin");
        


        string osuPath = "";
        while(!File.Exists(osuPath) && !Directory.Exists(osuPath)){
            Console.WriteLine("Input folder or path to .osz file:");
            osuPath = Console.ReadLine();
            if(!Directory.Exists(osuPath) && File.Exists(osuPath)){
                continue;            
            }
        }    

        string roboPath= "";
        while(!Directory.Exists(roboPath)){
            Console.WriteLine("Input export folder: ");
            roboPath = Console.ReadLine();
            if(osuPath[osuPath.Length-1] != '/' || osuPath[osuPath.Length-1] != '\\') roboPath += '/';
            Console.WriteLine("Export folder: " + roboPath);
        }
        
        
        OsuUnpack.Unpack(osuPath, roboPath);   

        Console.WriteLine("Complete!");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}