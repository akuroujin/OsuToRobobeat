internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("osu! to RoboCasette Converter V0.1 (prepare for issues!)");
        Console.WriteLine("Made by: @Akuroujin");
        Console.WriteLine("Input path to osu beatmap: ");


        string osuPath = "";
        while(!File.Exists(osuPath)){
            osuPath = Console.ReadLine();
        }    

        Console.WriteLine("Input export folder: ");

        string roboPath = Console.ReadLine();
        
        OsuUnpack.Unpack(osuPath, roboPath);   

        Console.WriteLine("Complete!");
        Console.WriteLine("Press any key to exit...");
        Console.Read();
    }
}