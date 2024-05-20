using System.Reflection;

class OsuMap{

    private StreamReader sr;
    public OsuMap(string path)
    {
        ReadFromFile(path);
    }
    public string id { get; private set; }    
    public string title { get; private set; }
    public string artist { get; private set; }
    public int firstObjectTime { get; private set; }
    public int lastObjectTime { get; private set; }


    private List<TimingPoint> timingpoints = new List<TimingPoint>();
    public List<TimingPoint> GetTimingPoints(){
        return timingpoints;
    }

    private void ReadFromFile(string path){
        sr = new StreamReader(path);
        string line = "";
        while(line != "[Metadata]" && !sr.EndOfStream){
            line = sr.ReadLine();
        }
        
        while(line != "[TimingPoints]" && !sr.EndOfStream){
            line = sr.ReadLine();
            if(line == "") continue;
            string[] data = line.Split(':');
            if(data[0] == "BeatMapID") id = data[1]; 
            if(data[0] == "Title") title = data[1]; 
            if(data[0] == "Artist") artist = data[1];             
        }

        while(line != "[HitObjects]" && !sr.EndOfStream){
            line = sr.ReadLine();
            string[] data = line.Split(',');
            if(data.Length < 6) continue;
            if(data[6] == "0") continue;  
            string offset = data[0].Split('.')[0];          
            timingpoints.Add(new TimingPoint(int.Parse(offset), double.Parse(data[1])));
        }

        line = sr.ReadLine();
        firstObjectTime = int.Parse(line.Split(',')[2]);
        while(!sr.EndOfStream) {
            line = sr.ReadLine();
        }
        lastObjectTime = int.Parse(line.Split(',')[2]);
        sr.Close();
    }        
}