using MHW.Entry;

namespace DumbHunter;

class Launcher
{
  [STAThread]
  static void Main(string[] args)
  {
    // TODO: Use args to determine which MH game support to launch

    MhwEntryPoint.Main(args);
  }
}