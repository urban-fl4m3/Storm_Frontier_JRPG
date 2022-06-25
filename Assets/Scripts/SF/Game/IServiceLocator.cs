using SF.Common.Factories;
using SF.Common.Logger;
using SF.Common.Ticks;
using SF.UI.Controller;

namespace SF.Game
{
    public interface IServiceLocator
    {
        IDebugLogger Logger { get; }
        ITickProcessor TickProcessor { get; }
        IFactoryHolder FactoryHolder { get; }
        IWindowController WindowController { get; }
    }
}