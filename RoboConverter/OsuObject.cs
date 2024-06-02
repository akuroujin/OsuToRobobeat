abstract class OsuObject{
    protected OsuObject(int offset)
    {
        this.offset = offset;
    }
    public int offset{get; private set;}    
}