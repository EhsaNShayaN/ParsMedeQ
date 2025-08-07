namespace SRH.Persistance.Models;

public sealed record MultipleReader<T1, T2>
{
    public T1? Item1 { get; set; } = default;
    public T2? Item2 { get; set; } = default;
}

public sealed record MultipleReader<T1, T2, T3>
{
    public T1? Item1 { get; set; } = default;
    public T2? Item2 { get; set; } = default;
    public T3? Item3 { get; set; } = default;
}

public sealed record MultipleReader<T1, T2, T3, T4>
{
    public T1? Item1 { get; set; } = default;
    public T2? Item2 { get; set; } = default;
    public T3? Item3 { get; set; } = default;
    public T4? Item4 { get; set; } = default;
}
public sealed record MultipleReader<T1, T2, T3, T4, T5>
{
    public T1? Item1 { get; set; } = default;
    public T2? Item2 { get; set; } = default;
    public T3? Item3 { get; set; } = default;
    public T4? Item4 { get; set; } = default;
    public T5? Item5 { get; set; } = default;
}
public sealed record MultipleReader<T1, T2, T3, T4, T5, T6>
{
    public T1? Item1 { get; set; } = default;
    public T2? Item2 { get; set; } = default;
    public T3? Item3 { get; set; } = default;
    public T4? Item4 { get; set; } = default;
    public T5? Item5 { get; set; } = default;
    public T6? Item6 { get; set; } = default;
}