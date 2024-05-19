class TimingPoint{
    public TimingPoint(int offset, double bpm)
    {
        this.offset = offset;
        this.bpm = bpm;
    }

    public int offset { get; private set; }
    public double bpm { get; private set; }
    
}