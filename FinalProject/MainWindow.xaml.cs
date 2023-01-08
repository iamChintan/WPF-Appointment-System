using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;
using VehicleDataLibrary;

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Object of AppointmentList class to store appointments
        AppointmentList appointments = new AppointmentList();
        //Declaration of ObservableCollection type of BookedAppointments class
        private ObservableCollection<BookedAppointments> vehicleAppointments = null;
        //Property for set and get data
        public ObservableCollection<BookedAppointments> VehicleAppointments { get => vehicleAppointments; set => vehicleAppointments = value; }
        //ObservableCollection of type string for binding appointment time list
        private ObservableCollection<string> TimeList { get; set; } = null;

        BookedAppointments securedAppointments = new BookedAppointments();
        public BookedAppointments SecuredAppointments { get => securedAppointments; set => securedAppointments = value; }


        public MainWindow()
        {
            InitializeComponent();
            VehicleAppointments = new ObservableCollection<BookedAppointments>();
            TimeList = new ObservableCollection<string>();
            DataContext = this;

            //Check two wheeler vehicle's radio button by default
            radioTwoWheeler.IsChecked = true;
            //Generate Available time dynamically and shows into datagrid
            GenerateAvailableSlots();


        }

 

        void GenerateAvailableSlots()
        {
            //Add dynamically available time slote into timelist
            for (int i = 9; i < 20; i++)
            {
                TimeList.Add(GetAllAvailableTime(i));
            }
            comboBoxTime.ItemsSource = TimeList;
            //Select bydefault zero indexe's time
            comboBoxTime.SelectedIndex = 0;
        }

        string GetAllAvailableTime(int hours)
        {
            var amPm = "AM";
            if (hours == 0)
            {
                hours = 12;
            }
            else if (hours == 12)
            {
                amPm = "PM";
            }
            else if (hours > 12)
            {
                hours -= 12;
                amPm = "PM";
            }
            var times = String.Format("{0}:{1:00} {2}", hours, 00, amPm);
            return times;
        }

        private void OnSubmitAppointment(object sender, RoutedEventArgs e)
        {
            //Checking if all datas are valid or not
            if (IsAllValid())
            {
              
                string appointmentTime = comboBoxTime.SelectedValue.ToString();
                string ownerName = txtOwnerName.Text;
                decimal contactNumber = decimal.Parse(txtContactNumber.Text);
                double vehicleAge = double.Parse(txtVehicleAge.Text);


                Vehicle vehicle = null;
                Appointment currentAppointment = new Appointment();
                currentAppointment.AppointmentTime = appointmentTime;

                List<string> serviceSelection = new List<string>();

                //Checking which radio button is selected
                if (radioTwoWheeler.IsChecked.Value)
                {
                    serviceSelection = GetSelectedServices();
                    TwoWheelerVehicle.TwoWheelerVehicleServices[] twoWheelerVehicleServices = new TwoWheelerVehicle.TwoWheelerVehicleServices[serviceSelection.Count];
                    foreach (var item in serviceSelection)
                    {
                        twoWheelerVehicleServices[serviceSelection.IndexOf(item)] = (TwoWheelerVehicle.TwoWheelerVehicleServices)Enum.Parse(typeof(TwoWheelerVehicle.TwoWheelerVehicleServices), item.ToString());
                    }
                    vehicle = new TwoWheelerVehicle(ownerName, contactNumber.ToString(), vehicleAge, twoWheelerVehicleServices);
                }
                else if (radioThreeWheeler.IsChecked.Value)
                {
                    serviceSelection = GetSelectedServices();
                    ThreeWheelerVehicle.ThreeWheelerVehicleServices[] threeWheelerVehicleServices = new ThreeWheelerVehicle.ThreeWheelerVehicleServices[serviceSelection.Count];
                    foreach (var item in serviceSelection)
                    {
                        threeWheelerVehicleServices[serviceSelection.IndexOf(item)] = (ThreeWheelerVehicle.ThreeWheelerVehicleServices)Enum.Parse(typeof(ThreeWheelerVehicle.ThreeWheelerVehicleServices), item.ToString());
                    }
                    vehicle = new ThreeWheelerVehicle(ownerName, contactNumber.ToString(), vehicleAge, threeWheelerVehicleServices);
                }
                else if (radioFourWheeler.IsChecked.Value)
                {
                    serviceSelection = GetSelectedServices();
                    FourWheelerVehicle.FourWheelerVehicleServices[] fourWheelerVehicleServices = new FourWheelerVehicle.FourWheelerVehicleServices[serviceSelection.Count];
                    foreach (var item in serviceSelection)
                    {
                        fourWheelerVehicleServices[serviceSelection.IndexOf(item)] = (FourWheelerVehicle.FourWheelerVehicleServices)Enum.Parse(typeof(FourWheelerVehicle.FourWheelerVehicleServices), item.ToString());
                    }
                    vehicle = new FourWheelerVehicle(ownerName, contactNumber.ToString(), vehicleAge, fourWheelerVehicleServices);
                }
                else if (radioHeavyDuty.IsChecked.Value)
                {
                    serviceSelection = GetSelectedServices();
                    HeavyDutyVehicle.HeavyDutyVehicleServices[] heavyDutyVehicleServices = new HeavyDutyVehicle.HeavyDutyVehicleServices[serviceSelection.Count];
                    foreach (var item in serviceSelection)
                    {
                        heavyDutyVehicleServices[serviceSelection.IndexOf(item)] = (HeavyDutyVehicle.HeavyDutyVehicleServices)Enum.Parse(typeof(HeavyDutyVehicle.HeavyDutyVehicleServices), item.ToString());
                    }
                    vehicle = new HeavyDutyVehicle(ownerName, contactNumber.ToString(), vehicleAge, heavyDutyVehicleServices);
                }

                currentAppointment.Vehicle = vehicle;
                //Add data into AppointmentList
                appointments.Add(currentAppointment);

                //Show data into DataGrid
                ShowCurrentDataIntoGrid(currentAppointment);

                //Remove selected timeslot after successfully saved appointment
                TimeList.Remove(currentAppointment.AppointmentTime);
                //If timeslot is not available then show dialog box
                if (comboBoxTime.Items.Count == 0)
                {
                    btnBookAppointment.IsEnabled = false;
                    MessageBox.Show("There no slots available!");
                }
                else
                {
                    comboBoxTime.SelectedIndex = 0;
                }

                ClearFields();
            }
        }

        //Clear all the fields after successfully saved appointment
        private void ClearFields()
        {

            // ClearTextFields
            txtOwnerName.Clear();
            txtVehicleAge.Clear();
            txtContactNumber.Clear();

            //Uncheck all the Check Boxes after appointment booked
            oilChange.IsChecked = false;
            radiatorFlush.IsChecked = false;
            tyresChange.IsChecked = false;
            brakesAndClutch.IsChecked = false;
            sparkPlug.IsChecked = false;
            carburetor.IsChecked = false;
            fuelPipes.IsChecked = false;
            exhaustSystems.IsChecked = false;
            gasKitRepair.IsChecked = false;
            grease.IsChecked = false;
            engineDiagnostics.IsChecked = false;
            beltsAndHoses.IsChecked = false;
            airAndHydraulic.IsChecked = false;
            hydraulicFilter.IsChecked = false;
            rotaryShaft.IsChecked = false;
            excavatorPump.IsChecked = false;
           
        }




        private void VehicleCheck(object sender, RoutedEventArgs e)
        {
            //Visible services according to which type of vehicle is selected
            RadioButton rb = (RadioButton)sender;
            switch (rb.Name)
            {
                case "radioTwoWheeler":
                    twoWheelerServices.Visibility = Visibility.Visible;
                    threeWheelerServices.Visibility = Visibility.Collapsed;
                    fourWheelerServices.Visibility = Visibility.Collapsed;
                    heavyDutyVehicle.Visibility = Visibility.Collapsed;
                    break;
                case "radioThreeWheeler":
                    twoWheelerServices.Visibility = Visibility.Collapsed;
                    threeWheelerServices.Visibility = Visibility.Visible;
                    fourWheelerServices.Visibility = Visibility.Collapsed;
                    heavyDutyVehicle.Visibility = Visibility.Collapsed;
                    break;
                case "radioFourWheeler":
                    twoWheelerServices.Visibility = Visibility.Collapsed;
                    threeWheelerServices.Visibility = Visibility.Collapsed;
                    fourWheelerServices.Visibility = Visibility.Visible;
                    heavyDutyVehicle.Visibility = Visibility.Collapsed;
                    break;
                case "radioHeavyDuty":
                    twoWheelerServices.Visibility = Visibility.Collapsed;
                    threeWheelerServices.Visibility = Visibility.Collapsed;
                    fourWheelerServices.Visibility = Visibility.Collapsed;
                    heavyDutyVehicle.Visibility = Visibility.Visible;
                    break;
                default:
                    twoWheelerServices.Visibility = Visibility.Visible;
                    break;

            }
        }


        private void ShowCurrentDataIntoGrid(Appointment appointment)
        {
            //Store data into BookedAppointments class and shows into DataGrid
            BookedAppointments bookedAppointments = new BookedAppointments();
            bookedAppointments.AppointmentTime = appointment.AppointmentTime;
            bookedAppointments.VehicleType = appointment.Vehicle.GetType().Name;
            bookedAppointments.OwnerName = appointment.Vehicle.OwnerName;
            bookedAppointments.ContactNumber = appointment.Vehicle.MaskContactNumber();
            bookedAppointments.VehicleAge = appointment.Vehicle.VehicleAge;
            bookedAppointments.SelectedServices = appointment.Vehicle.GetSelectedServices();

            VehicleAppointments.Add(bookedAppointments);
        }

        
        List<string> GetSelectedServices()
        {
            //Get all selected services by the user from the checkboxes
            List<string> currentServices = new List<string>();

            if (oilChange.IsChecked.Value)
            {
                currentServices.Add("OilChange");
            }
            if (radiatorFlush.IsChecked.Value)
            {
                currentServices.Add("RadiatorFlush");
            }
            if (tyresChange.IsChecked.Value)
            {
                currentServices.Add("TyresChange");
            }
            if (brakesAndClutch.IsChecked.Value)
            {
                currentServices.Add("BrakesAndClutch");
            }

            if (radioTwoWheeler.IsChecked.Value)
            {
                if (sparkPlug.IsChecked.Value)
                {
                    currentServices.Add("SparkPlug");
                }
                if (carburetor.IsChecked.Value)
                {
                    currentServices.Add("Carburetor");
                }
                if (fuelPipes.IsChecked.Value)
                {
                    currentServices.Add("FuelPipes");
                }
            }
            else if (radioThreeWheeler.IsChecked.Value)
            {
                if (exhaustSystems.IsChecked.Value)
                {
                    currentServices.Add("ExhaustSystems");
                }
                if (gasKitRepair.IsChecked.Value)
                {
                    currentServices.Add("GasKitRepair");
                }
                if (grease.IsChecked.Value)
                {
                    currentServices.Add("Grease");
                }
            }
            else if (radioFourWheeler.IsChecked.Value)
            {
                if (engineDiagnostics.IsChecked.Value)
                {
                    currentServices.Add("EngineDiagnostics");
                }
                if (beltsAndHoses.IsChecked.Value)
                {
                    currentServices.Add("BeltsAndHoses");
                }
                if (airAndHydraulic.IsChecked.Value)
                {
                    currentServices.Add("AirAndHydraulic");
                }
            }
            else if (radioHeavyDuty.IsChecked.Value)
            {
                if (hydraulicFilter.IsChecked.Value)
                {
                    currentServices.Add("HydraulicFilter");
                }
                if (rotaryShaft.IsChecked.Value)
                {
                    currentServices.Add("RotaryShaft");
                }
                if (excavatorPump.IsChecked.Value)
                {
                    currentServices.Add("ExcavatorPump");
                }
            }
            return currentServices;
        }


        private bool IsAllValid()
        {
            //flag for checking validate data
            bool IsAllValidData = true;

            //For Owner name validation 
            if (txtOwnerName.Text == "") 
            {
                IsAllValidData = false;
                txtOwnerName.ToolTip = "Enter Valid Name";
                txtOwnerName.BorderBrush = Brushes.Red;

            }else if ((txtOwnerName.Text.Length < 2))
            {
                IsAllValidData = false;
                txtOwnerName.ToolTip = "Name's length should be at least 2 characters";
                txtOwnerName.BorderBrush = Brushes.Red;

            }  else if ((!IsValidName(txtOwnerName.Text))){
                IsAllValidData = false;
                txtOwnerName.ToolTip = "Special characters not allowed!";
                txtOwnerName.BorderBrush = Brushes.Red;
            }
            

            ///For Contact Number validation 
            if ((txtContactNumber.Text == "") || (!IsValidMobileNumber(txtContactNumber.Text)))
            {
                IsAllValidData = false;
                txtContactNumber.ToolTip = "Enter 10 Digit Mobile Number Without Country Code";
                txtContactNumber.BorderBrush = Brushes.Red;
            }

            ///For Vehicle age validation
            if ((txtVehicleAge.Text == "") || (txtVehicleAge.Text == "0"))
            {
                IsAllValidData = false;
                txtVehicleAge.ToolTip = "Enter Vehicle's Age between 0.2 - 150 years";
                txtVehicleAge.BorderBrush = Brushes.Red;
            }
            else if ((!double.TryParse(txtVehicleAge.Text, out double age) || (age < 0.1 || age > 150)))
            {
                IsAllValidData = false;     
            }

            //For Chckboxes of services
            if (ServicesCheckBoxes())
            {
                serviceError.Visibility = Visibility.Visible;
                IsAllValidData = false;
            }
            else
            {
                serviceError.Visibility = Visibility.Collapsed;
            }

            return IsAllValidData;
        }


        // checking for any checkbox(service) is selected or not
        private bool ServicesCheckBoxes()
        {
            if ((oilChange.IsChecked.Value == false) && (radiatorFlush.IsChecked.Value == false)
                 && (tyresChange.IsChecked.Value == false) && (brakesAndClutch.IsChecked.Value == false))
            {

                if(radioTwoWheeler.IsChecked.Value == true)
                {
                    if ((sparkPlug.IsChecked.Value == false) && (carburetor.IsChecked.Value == false) && (fuelPipes.IsChecked.Value == false))
                    {
                        return true;
                    }
                }else if (radioThreeWheeler.IsChecked.Value == true)
                {
                    if ((exhaustSystems.IsChecked.Value == false) && (gasKitRepair.IsChecked.Value == false) && (grease.IsChecked.Value == false))
                    {
                        return true;
                    }
                }
                else if (radioFourWheeler.IsChecked.Value == true)
                {
                    if ((engineDiagnostics.IsChecked.Value == false) && (beltsAndHoses.IsChecked.Value == false) && (airAndHydraulic.IsChecked.Value == false))
                    {
                        return true;
                    }
                }
                else
                {
                    if ((hydraulicFilter.IsChecked.Value == false) && (rotaryShaft.IsChecked.Value == false) && (excavatorPump.IsChecked.Value == false))
                    {
                        return true;
                    }
                }

            }

            return false;
        }

        private bool IsValidName(string text)
        {
            // RegularExpressions for validation of vehicle owner name
            Regex rgx = new Regex("[^A-Za-z]");
            bool containsSpecialCharacter = rgx.IsMatch(text.ToString());
            if (containsSpecialCharacter)
            {
                return false;
            }
            return true;
        }

        bool IsValidMobileNumber(string number)
        {
            //Checking if mobile number is valid or not
            StringBuilder numberStringBuilder = new StringBuilder(number);
            if (number.Length != 10)
            {
                return false;
            }
            bool isValid = true;
            for (int i = 0; i < numberStringBuilder.Length; i++)
            {
                if (!char.IsDigit(numberStringBuilder[i]))
                {
                    isValid = false;
                }
            }
            return isValid;

        }

        //To store data into XML file
        private void StoreDataIntoXML(object sender, RoutedEventArgs e)
        {
            XmlSerializer xmlSerializer = null;
            TextWriter textWriter = null;
            try
            {
                xmlSerializer = new XmlSerializer(typeof(AppointmentList));
                textWriter = new StreamWriter("VehicleAppointments.xml");
                xmlSerializer.Serialize(textWriter, appointments);
                textWriter.Close();
                MessageBox.Show("Save Successful");
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                if (textWriter != null)
                {
                    textWriter.Close();
                }
            }
            finally
            {
                if (textWriter != null)
                {
                    textWriter.Close();
                }
            }
        }

        //To read data from XML file
        private void GetDataFromXML(object sender, RoutedEventArgs e)
        {
            XmlSerializer xmlSerializer = null;
            TextReader textReader = null;
            try
            {
                xmlSerializer = new XmlSerializer(typeof(AppointmentList));
                textReader = new StreamReader("VehicleAppointments.xml");
                appointments = (AppointmentList)xmlSerializer.Deserialize(textReader);
                textReader.Close();

                if (appointments.Count == 0)
                {
                    MessageBox.Show("There are no appointments!");
                    return;
                }

                vehicleAppointments.Clear();

                for (int i = 0; i < appointments.Count; i++)
                {
                    Vehicle vehicle = appointments[i].Vehicle;
                    BookedAppointments appointment = new BookedAppointments();
                    appointment.OwnerName = vehicle.OwnerName;
                    appointment.VehicleAge = vehicle.VehicleAge;
                    appointment.ContactNumber = vehicle.MaskContactNumber();
                    appointment.VehicleType = vehicle.GetType().Name;
                    appointment.SelectedServices = vehicle.GetSelectedServices();
                    appointment.AppointmentTime = appointments[i].AppointmentTime;
                   
                    //remove time slots which are available into xml
                    TimeList.Remove(appointments[i].AppointmentTime);
                    vehicleAppointments.Add(appointment);

                    if (comboBoxTime.Items.Count == 0)
                    {
                        btnBookAppointment.IsEnabled = false;
                    }
                    else
                    {
                        comboBoxTime.SelectedIndex = 0;
                    }
                }

                dataDisplayGrid.ItemsSource = VehicleAppointments;
                txtSearchName.Text = "";
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message);
                if (textReader != null)
                {
                    textReader.Close();
                }
            }
            finally
            {
                if (textReader != null)
                {
                    textReader.Close();
                }
            }

        }

        //Filter data according to search value and showed into DataGrid
        //LINQ-Querie : filter data based on vehicle owner's name
        private void FliterDataBySearch(object sender, RoutedEventArgs e)
        {
            if (txtSearchName.Text.Length == 0)
            {
                dataDisplayGrid.ItemsSource = VehicleAppointments;
            }
            else
            {
                var query = from appointment in VehicleAppointments
                            where appointment.OwnerName.ToLower().Contains(txtSearchName.Text.ToLower().Trim())
                            select appointment;


                dataDisplayGrid.ItemsSource = query;
            }
        }


        //Called when text change in the all the textfields
        //make Brushes color black
        private void onTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Foreground = Brushes.Black;
            textBox.BorderBrush = Brushes.LightGray;
        }

        //Called when check box is checked
        //remove error label
        private void OnCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            if (checkBox.IsEnabled)
            {
                serviceError.Visibility = Visibility.Collapsed;
            }
           
        }
    }
}
