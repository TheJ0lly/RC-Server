namespace Server.Controller;

public class MainWindowController : ViewModelBase
{ 
    private bool _paneOpen;
    public bool IsPaneOpen
    {
        get => _paneOpen;
        set
        {
            _paneOpen = value;
            OnPropertyChanged(nameof(IsPaneOpen));
        }
    }
    public bool SwitchPane()
    {
        IsPaneOpen = !IsPaneOpen;
        return _paneOpen;
    }
}