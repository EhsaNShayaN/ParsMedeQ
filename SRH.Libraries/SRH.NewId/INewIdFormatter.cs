namespace SRH.NewId;

public interface INewIdFormatter
{
    string Format(in byte[] bytes);
}
