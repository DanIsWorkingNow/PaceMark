using System;
using System.IO;
using Microsoft.Maui.Controls;

namespace PaceMark
{
    public partial class Record : ContentPage
{
    // Same filename as MainPage!
    readonly string fileName = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RunRecords.txt");

    public Record()
    {
        InitializeComponent();
    }

    // Every time this page comes into view, reload the latest file
    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadRecords();
    }

    void LoadRecords()
    {
        if (File.Exists(fileName) && new FileInfo(fileName).Length > 0)
            displayRecord.Text = File.ReadAllText(fileName);
        else
            displayRecord.Text = "No records available.";
    }

    private void OnClearRecordClicked(object sender, EventArgs e)
    {
        // Erase the file
        File.WriteAllText(fileName, string.Empty);

        // And immediately refresh the display
        LoadRecords();
       }
   }
}