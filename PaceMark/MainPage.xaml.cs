using System;
using System.IO;
using Microsoft.Maui.Controls;


namespace PaceMark
{
    public partial class MainPage : ContentPage
    {
        // Path to store run records
        string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "RunRecords.txt");


        /*FirebaseHelper firebaseHelper = new FirebaseHelper();*/

        public MainPage()
        {
            InitializeComponent();
        }

        void onDatePickerSelected(object sender, DateChangedEventArgs e)
        {
            var selectedDate = e.NewDate.ToString();
        }

        // Calculate pace (min/km) and speed (m/s, km/h)
        void OnCalculatePace(object sender, EventArgs e)
        {
            // parse inputs
            bool okTime = double.TryParse(inputTimeSeconds.Text, out double timeSec);
            bool okDistance = double.TryParse(inputDistanceMeters.Text, out double distanceM);

            if (!okTime || !okDistance || timeSec <= 0 || distanceM <= 0)
            {
                outputResult.Text = "Enter valid time & distance.";
                return;
            }

            // pace in seconds per km
            double secPerKm = timeSec / distanceM * 1000;
            int paceMin = (int)(secPerKm / 60);
            int paceSec = (int)Math.Round(secPerKm % 60);

            // speed
            double speedMs = distanceM / timeSec;
            double speedKmh = speedMs * 3.6;

            // show both
            outputResult.Text =
                $"Pace: {paceMin}:{paceSec:00} min/km\n" +
                $"Speed: {speedMs:0.00} m/s ({speedKmh:0.00} km/h)";
        }






        // Save the current run record to file
        void OnSaveRecord(object sender, EventArgs e)
        {
            // parse again
            bool okTime = double.TryParse(inputTimeSeconds.Text, out double timeSec);
            bool okDistance = double.TryParse(inputDistanceMeters.Text, out double distanceM);

            if (!okTime || !okDistance || timeSec <= 0 || distanceM <= 0)
            {
                DisplayAlert("Invalid Data", "Cannot save. Please enter valid time and distance.", "OK");
                return;
            }

            // recalc
            double secPerKm = timeSec / distanceM * 1000;
            int paceMin = (int)(secPerKm / 60);
            int paceSec = (int)Math.Round(secPerKm % 60);
            double speedMs = distanceM / timeSec;
            double speedKmh = speedMs * 3.6;
            string dateString = selectDate.Date.ToString("dd/MM/yyyy");

            // build record
            string record =
                $"Date: {dateString}\n" +
                $"Time: {timeSec:0.##} s\n" +
                $"Distance: {distanceM:0.##} m\n" +
                $"Pace: {paceMin}:{paceSec:00} min/km\n" +
                $"Speed: {speedMs:0.00} m/s ({speedKmh:0.00} km/h)\n\n";

            File.AppendAllText(fileName, record);
            DisplayAlert("Record Saved", "Your run record has been saved.", "OK");
        }

        // Reset inputs and output
        void OnReset(object sender, EventArgs e)
        {
            inputTimeSeconds.Text = string.Empty;
            inputDistanceMeters.Text = string.Empty;
            outputResult.Text = "Your pace will appear here";
        }
    }
}
