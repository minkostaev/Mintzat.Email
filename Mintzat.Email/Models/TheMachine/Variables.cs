namespace Mintzat.Email.Models.TheMachine;

using System.Collections;

public class Variables : Dictionary<string, string?>
{
    public Variables()
    {
        try
        {
            int errors = 0;
            foreach (DictionaryEntry variable in Environment.GetEnvironmentVariables())
            {
                if (variable.Key != null && variable.Value != null)
                {
                    try { this.Add(variable.Key.ToString()!, variable.Value.ToString()); }
                    catch (Exception)
                    {
                        errors++;
                        if (this.ContainsKey("ERRORS"))
                        {
                            this["ERRORS"] = errors.ToString();
                        }
                        else
                        {
                            this.Add("ERRORS", errors.ToString());
                        }
                    }
                }
            }
        }
        catch (Exception ex) { this.Add("CRASH", ex.Message); }
    }
}