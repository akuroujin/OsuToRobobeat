class RoboCasette {
    public RoboCasette(OsuMap map){
        timingpoints = TimingConverter.CovnertTiming(map);
        title = map.title;
        artist = map.artist;
        id = map.id;
        estimatedEnd = (map.lastObjectTime/1000.0) + 10;
        lastObjectTime = map.lastObjectTime/1000.0;
        firstObjectTime = (map.firstObjectTime/1000.0) - 1;
    }
    public List<double> timingpoints;
    public List<double> GetTimingPoints(){
        return timingpoints;
    }
    public string title{get;private set;}
    public string artist{get;private set;}
    public string id { get; private set; }
    public double estimatedEnd { get; private set; }
    private double firstObjectTime;
    private double lastObjectTime;

    public void WriteToFile(string path){
        StreamWriter sw = new StreamWriter(path);
        sw.WriteLine("{\n\"Main\": 5,\n\"Secondary\": 8\n}{");
        sw.WriteLine("\"File\": {");
        sw.WriteLine($"\"InternalName\": \"{id}\",");
        sw.WriteLine("\"Info\": {");
        sw.WriteLine("\"PathToAudioClip\": \"imported/from/osu\",");
        sw.WriteLine("\"InStorage\": true,");
        sw.WriteLine($"\"FileName\": \"{id}.mp3\",");
        sw.WriteLine($"\"LengthOfClip\": {estimatedEnd},");
        sw.WriteLine($"\"PublicName\": \"{title}\",");
        sw.WriteLine($"\"ArtistName\": \"{artist}\",");
        sw.WriteLine("\"BPM\": 0");
        sw.WriteLine("},");
        sw.WriteLine("\"Beat\": {");
        sw.WriteLine($"\"StartTime\": {firstObjectTime},");
        sw.WriteLine($"\"EndTime\": {lastObjectTime},");
        sw.WriteLine($"\"NumberOfBeats\": {timingpoints.Count},");
        sw.WriteLine("\"Beats\": [");
        for(int i = 0; i < timingpoints.Count; i++){
            sw.Write("{0}", timingpoints[i]);
            if(i != timingpoints.Count - 1) sw.WriteLine(","); 
            else sw.WriteLine("");
        }
        sw.WriteLine("]");
        sw.WriteLine("},");
        sw.WriteLine("\"Visuals\": {");
        sw.WriteLine("\"CassetteTextureInternalName\": \"PASSAGE1\",");
        sw.WriteLine("\"CassetteColor\": {");
        sw.WriteLine("\"r\": 0.8,");
        sw.WriteLine("\"g\": 0.4,");
        sw.WriteLine("\"b\": 0.4,");
        sw.WriteLine("\"a\": 1.0");
        sw.WriteLine("},");
        sw.WriteLine("\"CasetteStrength\": 1.25,");
        sw.WriteLine("\"CasetteStrength\": false");
        sw.WriteLine("},");
        sw.WriteLine("\"IsMixTape\": false,");
        sw.WriteLine("},");
        sw.WriteLine($"\"InternalName\": \"{id}\"");
        sw.WriteLine("}");
        sw.Close();
    }
}