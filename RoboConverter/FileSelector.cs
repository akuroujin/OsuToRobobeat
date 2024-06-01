static class FileSelector{
    public static string ListFiles(string type, string path){
        string[] files = Directory.GetFiles(path, "*." + type);

        if(files.Length == 0){
            System.Console.WriteLine("No files were found with the type: ." + type);
            return null;
        }

        if(files.Length == 1)
            return files[0];

        Console.WriteLine("Select file: ");
        for (int i = 0; i < files.Length; i++)        {
            Console.WriteLine($"{i+1}. - {files[i]}");
        }
        Console.Write("Selection: ");        
        int selection = -1;
        
        while(selection < 0 || selection > files.Length){
            selection = int.Parse(Console.ReadLine())-1;
        }

        return files[selection];
    }
}