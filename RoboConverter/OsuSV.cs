class OsuSV : OsuObject
{
    public OsuSV(int offset, double SV) : base(offset)
    {
        this.SV = SV;
    }
    public double SV{get; private set;}
}