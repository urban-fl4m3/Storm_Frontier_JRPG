using SF.Common.Factories;

namespace SF.UI.Windows
{
    public interface IWindow : IFactoryInstance
    {
        void Show();
        void Hide();
        void Close();
    }
}