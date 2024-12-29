using System;
using System.IO;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using Server.Controller;
using System.Linq;

namespace Server.Views;

public partial class ShareView : UserControl
{
    private readonly ShareViewController _controller;
    public ShareView()
    {
        InitializeComponent();
        _controller = new ShareViewController();
        DataContext = _controller;
    }

    private async void BrowseButton_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            var topLevel = TopLevel.GetTopLevel(this);

            if (topLevel is null) 
                throw new Exception("Unable to locate top level!");
            
            // We start the dialog.
            var folderPicker = await topLevel.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
            {
                Title = "Select a folder",
                AllowMultiple = false,
            });

            // If we did not select any folders, we simply add a log.
            if (folderPicker.Count == 0)
            {
                _controller.AddLog("No folder selected!");
                return;
            }

            var folderPath = folderPicker[0].Path.LocalPath;
                
            // We display the current path
            FolderPathBox.Text = folderPath;
            
            _controller.AddLog($"Reading folder: {folderPath}");
            
            var files = ParseFolder(folderPath);

            if (files.Length == 0)
            {
                _controller.AddLog("Cannot find any files!");
                return;
            }
            
            _controller.AddLog($"Found {files.Length} files");
            
            AddFilesToList(files);
        }
        catch (Exception ex)
        {
            _controller.AddLog(ex.Message);
        }
    }

    private static string?[] ParseFolder(string folderPath)
    {
        return Directory.GetFiles(folderPath).Select(Path.GetFileName).OrderBy(x => x).ToArray();
    }

    private void AddFilesToList(string?[] files)
    {
        foreach (var file in files)
        {
            if (file is null) continue;
            
            FilesListBox.Items.Add(file);
        }
    }
}