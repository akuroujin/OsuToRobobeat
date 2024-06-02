class OsuTimingPoint : OsuObject
{
    public OsuTimingPoint(int offset, double bpm) : base(offset)
    {
        this.bpm = bpm;
    }

    public double bpm{get; private set;}
}