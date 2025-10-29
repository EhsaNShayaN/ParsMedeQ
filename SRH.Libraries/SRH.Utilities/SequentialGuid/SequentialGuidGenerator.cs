using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace SRH.Utilities.SequentialGuid;

public interface ISequentialGuidGenerator
{
    Guid NewSequentialGuid();
}
public class SequentialGuidGeneratorService : ISequentialGuidGenerator
{
    private static readonly RandomNumberGenerator Rng = new RNGCryptoServiceProvider();

    //volatile declarations prevent local threads from caching reads to these values
    private static volatile ushort _counter = RandomCounterInit();

    private static volatile uint _timeSig;

    public Guid NewSequentialGuid()
    {
        return NewSqlGuid();
    }

    // Returns a random ushort integer between 0 and 4095 (FFFh)
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static ushort RandomCounterInit()
    {
        var randomBytes = new byte[2];
        Rng.GetBytes(randomBytes);
        var result = BitConverter.ToUInt16(randomBytes, 0);
        return (ushort)(result & 0x0FFF);
    }

    private static Guid NewSqlGuid()
    {
        unchecked //counter overlaps and casts to ushort are expected and should not be checked against
        {
            var randomBytes = new byte[8];
            Rng.GetBytes(randomBytes);

            var ticks = DateTime.Now.Ticks;
            var timestamp = (ticks >> 12) - 0x789abcdef012L; //789abcdef012h = 10 Mar 1722
                                                             //the timestamp is decreased by the arbitrary value to give more room for future dates
            var timestampBytes = BitConverter.GetBytes(timestamp);

            //if the counter has not reached 0x7FFF yet, 
            //then do not worry about checking timestamp signature, and keep incrementing;
            //otherwise reset the counter, if the timestamp has changed
            if (_counter > 0x7FFE && _timeSig != (uint)timestamp)
                _counter = RandomCounterInit();
            else
                // ReSharper disable once NonAtomicCompoundOperator
                _counter++; //thread unsafe, but we are ok with it; use Interlocked.Increment otherwise
            _timeSig = (uint)timestamp;

            //Counter is a two byte integer, that starts from a random value between 0000 and 0FFFh,
            //and keeps incrementing; upon reaching FFFFh the counter loops back to 0,
            //at which point the correct ordering of SQL GUIDs has been breached.

            //Time signature checking logic resets the counter back to the random value
            //if timestamp has updated since the previous call.

            var counterBytes = BitConverter.GetBytes(_counter);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timestampBytes);
                Array.Reverse(counterBytes);
            }

            var guidBytes = new byte[16];
            Buffer.BlockCopy(randomBytes, 0, guidBytes, 0, 8);
            Buffer.BlockCopy(counterBytes, 0, guidBytes, 8, 2);
            Buffer.BlockCopy(timestampBytes, 2, guidBytes, 10, 6);

            return new Guid(guidBytes);
        }
    }
}