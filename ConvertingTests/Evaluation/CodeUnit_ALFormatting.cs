[NavCodeunitOptions()]
public sealed class Codeunit93000 : NavCodeunit
{
    public Codeunit93000(ITreeObject parent): base (parent, 93000)
    {
    }

    public override string ObjectName => @"example";
    public static Codeunit93000 __Construct(ITreeObject parent) => new Codeunit93000(parent);
}