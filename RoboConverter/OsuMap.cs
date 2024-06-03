class OsuMap{

    private StreamReader sr;
    public OsuMap(string path, bool fromMap)
    {
        this.fromMap = fromMap;
        if(fromMap)
            ReadFromMap(path);
        else
            ReadFromTiming(path);
    }
    public bool fromMap { get; private set; }
    public string id { get; private set; }    
    public string title { get; private set; }
    public string artist { get; private set; }
    public int lastObjectTime { get; private set; }
    private List<OsuTimingPoint> timingPoints = new List<OsuTimingPoint>();

    public List<OsuTimingPoint> GetTimingPoints(){
        return timingPoints;
    }

    private List<OsuCircle> objects = new List<OsuCircle>();
    public List<OsuCircle> GetObjects(){
        return objects;
    }

    private void ReadFromTiming(string path){
        sr = new StreamReader(path);
        string line = "";
        while(line != "[Metadata]" && !sr.EndOfStream){
            line = sr.ReadLine();
        }

        while(line != "[TimingPoints]" && !sr.EndOfStream){
            line = sr.ReadLine();
            if(line == "") continue;
            line = line.Replace('?', ' ');
            line = line.Replace("*", "");
            line = line.Replace('!', ' ');
            line = line.Trim();
            string[] data = line.Split(':');
            if(data[0] == "BeatmapID") id = data[1]; 
            if(data[0] == "Title") title = data[1]; 
            if(data[0] == "Artist") artist = data[1];             
        }

        while(line != "[HitObjects]" && !sr.EndOfStream){
            line = sr.ReadLine();
            string[] data = line.Split(',');
            if(data.Length < 6) continue;
            if(data[6] == "0") continue;  
            string offset = data[0].Split('.')[0];          
            timingPoints.Add(new OsuTimingPoint(int.Parse(offset), double.Parse(data[1])));
        }

        line = sr.ReadLine();
        while(!sr.EndOfStream) {
            line = sr.ReadLine();
        }
        lastObjectTime = int.Parse(line.Split(',')[2]);
        sr.Close();
    }        

    private void ReadFromMap(string path){
        List<OsuSV> SVs = new List<OsuSV>();
        double sliderMultiplier = 1;

        sr = new StreamReader(path);
        string line = "";
        while(line != "[Metadata]" && !sr.EndOfStream){
            line = sr.ReadLine();
        }

        while(line != "[TimingPoints]" && !sr.EndOfStream){
            line = sr.ReadLine();
            if(line == "") continue;
            line = line.Replace('?', ' ');
            line = line.Replace("*", "");
            line = line.Replace('!', ' ');
            line = line.Trim();
            string[] data = line.Split(':');
            if(data[0] == "BeatmapID") id = data[1]; 
            if(data[0] == "Title") title = data[1]; 
            if(data[0] == "Artist") artist = data[1];     
            if(data[0] == "SliderMultiplier") sliderMultiplier = double.Parse(data[1]);          
        }

        while(line != "[HitObjects]" && !sr.EndOfStream){
            line = sr.ReadLine();
            string[] data = line.Split(',');
            if(data.Length < 6) continue;
            if(data[6] == "0") 
                SVs.Add(new OsuSV(int.Parse(data[0].Split('.')[0]), double.Parse(data[1])));  
            else
                timingPoints.Add(new OsuTimingPoint(int.Parse(data[0].Split('.')[0]), double.Parse(data[1])));
        }
        while(!sr.EndOfStream) {
            line = sr.ReadLine();
            string[] data = line.Split(',');
            if(objects.Count > 0 && data[3] == objects[objects.Count-1].offset.ToString()) continue;
            if(data[3] == "1" || data[3] == "4"|| data[3] == "5")  {
                objects.Add(new OsuCircle(int.Parse(data[2])));
            }
            
            if(data[3] == "2" || data[3] == "6")  {
                double bpm = 0;
                double sv = 1;
                for (int i = 0; i < timingPoints.Count; i++)
                {
                    if(timingPoints.Count == 1){
                        bpm = timingPoints[i].bpm;
                        break;
                    }

                    if(timingPoints[i].offset > int.Parse(data[2])){
                        bpm = timingPoints[i-1].bpm;
                        break;
                    }
                }
                for (int i = 0; i < SVs.Count; i++)
                {
                    if(i == 0 && SVs[i].offset > int.Parse(data[2])){
                        bpm = timingPoints[i].bpm;
                        break;
                    }
                    if(SVs[i].offset > int.Parse(data[2])){
                        sv = SVs[i-1].SV;
                        break;
                    }
                }
                List<int> slidersPoints = GetSliderTimings(data, sliderMultiplier, sv, bpm);
                foreach (var item in slidersPoints)
                {
                    objects.Add(new OsuCircle(item));
                }
            }

            if(data[3] == "12")  {
                objects.Add(new OsuCircle(int.Parse(data[2])));
                objects.Add(new OsuCircle(int.Parse(data[5])));
            }
        }
        lastObjectTime = int.Parse(line.Split(',')[2]);
        sr.Close();

    }

    private List<int> GetSliderTimings(string[] objectData, double sliderMultiplier, double sliderVelocity, double bpm){
        /* 
            Get sliders lenght in time, add starting and endpoint to the array  
            Somehow get the repeat points into the array as well
            Profit
        */
        List<int> beats = new List<int>();
        
        int offset = int.Parse(objectData[2]);
        //System.Console.WriteLine(double.Parse(objectData[7]) / (sliderMultiplier * 100 * (-100/sliderVelocity)) * bpm);
        double sliderLength = double.Parse(objectData[7]) / (sliderMultiplier * 100 * (-100/sliderVelocity)) * bpm;

        for (int i = 0; i < int.Parse(objectData[6]); i++)
        {
            beats.Add(offset);
            offset+=(int)Math.Floor(sliderLength);
        }
        return beats;
    }
}