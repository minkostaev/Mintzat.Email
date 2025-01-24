namespace Mintzat.Email.Models.TheMachine;

public class Processor
{
    public Processor() { Init(); }
    public Processor(bool initialize) { Init(initialize); }
    private void Init(bool initialize = false)
    {
        if (initialize)
        {
            try
            {
                Os64 = Environment.Is64BitOperatingSystem;
                Process64 = Environment.Is64BitProcess;
                Count = Environment.ProcessorCount;
            }
            catch (Exception) { Os64 = false; }
        }
    }

    public bool Os64 { get; set; }
    public bool Process64 { get; set; }
    public int Count { get; set; }
}