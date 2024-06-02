internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("osu! to RoboCasette Converter V0.2 (prepare for issues!)");
        Console.WriteLine("Made by: @Akuroujin");
        

        string choice;
        do{
            System.Console.WriteLine("Convert bpm(1) or map(2)?");
            choice = Console.ReadLine();
            
        } while(choice != "1" && choice != "2");

        bool convertMap = choice == "2";
        string osuPath;

        OsuMap map = null;

        do {
            
            Console.WriteLine("Input folder or path to .osz file:");
            osuPath = Console.ReadLine();
            if(!Directory.Exists(osuPath) && File.Exists(osuPath)){
                continue;            
            }
            map = OsuUnpack.Unpack(osuPath, convertMap);
        }    while(!File.Exists(osuPath) && !Directory.Exists(osuPath) && map == null);


        string roboPath= "";
        while(!Directory.Exists(roboPath)){

            Console.WriteLine("Input export folder: ");
            roboPath = Console.ReadLine();
            if(osuPath[osuPath.Length-1] != '/' || osuPath[osuPath.Length-1] != '\\') roboPath += '/';
            Console.WriteLine("Export folder: " + roboPath);
        }
        
        RoboPack.Pack(roboPath, map);

        OsuUnpack.Clean();
        RoboPack.Clean();

        //Temporary solution for no maps in folder
        Console.WriteLine("Complete!");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}