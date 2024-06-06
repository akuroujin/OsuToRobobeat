static class TimingConverter{
    private static List<double> beats = new List<double>();

    public static List<double> ConvertOffset(OsuMap map){
        for (int i = 0; i < map.GetObjects().Count; i++)
        {
            double offset = map.GetObjects()[i].offset/1000.0;
            beats.Add(offset);
        }
        return beats;
    }
    public static List<double> CovnertTiming(OsuMap map){
        // const double delay = 0.073; //Weird delay that gets added after importing
        double startOffset;
        for (int i = 0; i < map.GetTimingPoints().Count; i++)
        {
            
            startOffset = map.GetTimingPoints()[i].offset/1000.0;
            if(i+1 < map.GetTimingPoints().Count){
                FillBeats(
                    startOffset, map.GetTimingPoints()[i+1].offset/1000.0, map.GetTimingPoints()[i].bpm/1000.0
                    );
            }
            else{
                FillBeats(
                    startOffset, map.lastObjectTime/1000.0, map.GetTimingPoints()[i].bpm/1000.0
                    );
            }
        }
        beats.Add(map.lastObjectTime/1000.0);
        return beats;
    }
    private static void FillBeats(double startOffset, double endOffset, double bpm){
        double currentOffset = startOffset;
        while(currentOffset < endOffset){
            currentOffset += bpm;
            beats.Add(currentOffset);
        }
    }
}