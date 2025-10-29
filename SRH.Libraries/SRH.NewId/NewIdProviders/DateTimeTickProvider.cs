namespace SRH.NewId.NewIdProviders;

using SRH.NewId;
using System;


public class DateTimeTickProvider :
    ITickProvider
{
    public long Ticks => DateTime.Now.Ticks;
}
