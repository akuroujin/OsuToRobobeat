static class TimingConverter{
    private static List<double> beats = new List<double>();
    public static List<double> CovnertTiming(OsuMap map){
        
        beats.Add(map.firstObjectTime/1000.0);
        double startOffset = map.firstObjectTime/1000.0;
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