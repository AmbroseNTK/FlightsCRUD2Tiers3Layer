using FlightsCRUD2Tiers3Layer.BLL;
using FlightsCRUD2Tiers3Layer.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FlightsCRUD2Tiers3Layer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private FlightBUS flightBL;
        public MainPage()
        {
            this.InitializeComponent();
            flightBL = new FlightBUS();
        }

        private void LoadData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadData();
            }
            catch(Exception ex)
            {
                ShowMessage(ex.Message, ex.Source);
            }
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Flight flight = this.dataGrid.SelectedItem as Flight;
                if (flight != null)
                {
                    tbID.Text = flight.ID.ToString();
                    tbCode.Text = flight.Code;
                    tbArrAir.Text = flight.ArrivalAirport;
                    tbArrGate.Text = flight.ArrivalGate;
                    tbDeptAir.Text = flight.DepatureAirport;
                    tbDeptGate.Text = flight.DepatureGate;
                    pickerDate.SelectedDate = new DateTimeOffset(DateTime.Parse(flight.Date));
                    string[] timeParts = flight.CheckInTime.Split(":");
                    pickerTime.SelectedTime = new TimeSpan(int.Parse(timeParts[0]), int.Parse(timeParts[1]), 0);
                }
            }
            catch(Exception ex)
            {
                ShowMessage(ex.Message, ex.Source);
            }
        }

        private void InsertData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                flightBL.Add(GetFlightFromFields());
                LoadData();
            }
            catch(Exception ex)
            {
                ShowMessage(ex.Message, ex.Source);
            }
        }

        private void LoadData()
        {
            this.dataGrid.ItemsSource = flightBL.GetAll();
        }

        private void UpdateData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                flightBL.Update(GetFlightFromFields());
                LoadData();
            }
            catch(Exception ex)
            {
                ShowMessage(ex.Message, ex.Source);
            }
        }

        private async void DeleteData_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new MessageDialog("Are you sure?","Warning");
            dialog.Commands.Add(new UICommand { Label = "Ok", Id = 0 });
            dialog.Commands.Add(new UICommand { Label = "Cancel", Id = 1 });
            var result = await dialog.ShowAsync();
            if ((int)result.Id == 0)
            {
                try
                {
                    flightBL.Remove(int.Parse(tbID.Text));
                    LoadData();
                }
                catch(Exception ex)
                {
                    ShowMessage(ex.Message, ex.Source);
                }
            }
 
        }

        private Flight GetFlightFromFields()
        {
            string date = "";
            if (pickerDate.SelectedDate != null)
            {
                string month = pickerDate.SelectedDate.Value.Month.ToString();
                string day = pickerDate.SelectedDate.Value.Day.ToString();
                month = month.Length == 1 ? "0" + month : month;
                day = day.Length == 1 ? "0" + day : day;
                date = month + "/" + day + "/" + pickerDate.SelectedDate.Value.Year;

            }
            string time = "";
            if (pickerTime.SelectedTime != null)
            {
                string hours = pickerTime.SelectedTime.Value.Hours.ToString();
                string minutes = pickerTime.SelectedTime.Value.Minutes.ToString();
                hours = hours.Length == 1 ? "0" + hours : hours;
                minutes = minutes.Length == 1 ? "0" + minutes : minutes;
                time = hours + ":" + minutes;
            }

            return new Flight()
            {
                ID = int.Parse(tbID.Text),
                Code = tbCode.Text,
                ArrivalAirport = tbArrAir.Text,
                ArrivalGate = tbArrGate.Text,
                DepatureAirport = tbDeptAir.Text,
                DepatureGate = tbDeptGate.Text,
                Date = pickerDate.SelectedDate == null ? DateTime.Now.ToShortDateString() : date,
                CheckInTime = pickerTime.SelectedTime == null ? "00:00" : time
            };
        }
        private async void ShowMessage(string caption, string title)
        {
            var dialog = new MessageDialog(caption,title);
            await dialog.ShowAsync();
        }

        private void AddKeywordToExpression(object sender, RoutedEventArgs e)
        {
            Button input = sender as Button;
            tbKeywords.Text = tbKeywords.Text.Trim();
            if (tbKeywords.Text.Length!=0 && tbKeywords.Text[tbKeywords.Text.Length - 1] != ';')
            {
                tbKeywords.Text += ";";
            }
            tbKeywords.Text += input.Tag.ToString();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dataGrid.ItemsSource = flightBL.Search(tbKeywords.Text);
            }
            catch(Exception ex)
            {
                ShowMessage(ex.Message, ex.Source);
            }
        }
    }
}
