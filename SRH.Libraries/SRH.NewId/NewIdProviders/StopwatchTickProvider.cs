namespace SRH.NewId.NewIdProviders;

using SRH.NewId;
using System;
using System.Diagnostics;


public class StopwatchTickProvider :
    ITickProvider
{
    readonly DateTime _start;
    readonly Stopwatch _stopwatch;

    public StopwatchTickProvider()
    {
        _start = DateTime.Now;
        _stopwatch = Stopwatch.StartNew();
    }

    public long Ticks => _start.AddTicks(_stopwatch.Elapsed.Ticks).Ticks;
}
