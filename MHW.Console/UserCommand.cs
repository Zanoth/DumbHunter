namespace MHW.Console;

public class UserCommand : Attribute
{
  public UserCommand(string shortFormActivationInput, string longFormActivationInput, string description)
  {
    ShortFormActivationInput = shortFormActivationInput;
    LongFormActivationInput = longFormActivationInput;
    Description = description;
  }

  public string ShortFormActivationInput { get; }
  public string LongFormActivationInput { get; }
  public string Description { get; }
}