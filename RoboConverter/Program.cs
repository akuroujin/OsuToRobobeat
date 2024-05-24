internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("osu! to RoboCasette Converter V0.1 (prepare for issues!)");
        Console.WriteLine("Made by: @Akuroujin");
        


        string osuPath = "";
        while(!File.Exists(osuPath) ){
            Console.WriteLine("Input path to .osz file:");
            osuPath = Console.ReadLine();
            if(osuPath.Contains('.') && osuPath.Split('.')[1] != "osz"){
                continue;
            }
        }    

        string roboPath= "";
        while(!Directory.Exists(roboPath)){
            Console.WriteLine("Input export folder: ");
            roboPath = Console.ReadLine();
            if(osuPath[osuPath.Length-1] != '\\') roboPath += '\\';
        }
        
        
        OsuUnpack.Unpack(osuPath, roboPath);   

        Console.WriteLine("Complete!");
        Console.WriteLine("Press any key to exit...");
        Console.Read();
    }
}