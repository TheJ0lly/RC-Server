using System;
using System.Collections.Generic;

namespace Server.Controller;

public class ShareViewController : ViewModelBase
{
    private readonly List<string> _logs = new();

    public string Logs => string.Join('\n', _logs);

    public void AddLog(string log)
    {
        _logs.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": " + log);
        OnPropertyChanged(nameof(Logs));
    }
}