/*The Route Optimising SideKick was created as a 4th year final project for Gavin Gaughran x12107077 National College of Ireland 16.05.16

Code used throughout was written by Gavin Gaughran using, references and modified snippets from to the following websites:
    StackOverflow(General coding practice and troubleshooting)
    Code.MSDN(Algorithm mathematical equation)
    Damien Dennehy(Haversine information)
    Rubicite(PMX Crossover information)
    JSFiddle(JQuery and JavaScript functionality)
    W3Schools(CSS and design)
*/

using System;
using System.Web.UI;
using System.ComponentModel;
using System.Web;
using System.Reflection.Emit;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Text;

namespace RouteOp
{
    public partial class _Default : Page
    {
        private GeneticAlgorithm genAlgo;
        private BackgroundWorker backgroundWorkerThread;

        //Create arrays for Lat and Long
        private double[] lng;
        private double[] lat;
        private string[] newAddressArray;

        protected void Page_Load(object sender, EventArgs e)
        {
            returnResults.Visible = false;
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "initMap", "initMap();", true);

            if (!User.Identity.IsAuthenticated)
            {
                AddMoreFieldsButton.Visible = false;
            }
            else
            {
                AddMoreFieldsButton.Visible = true;
            }
        }

        protected void OptimiseButton_Click(object sender, EventArgs e)
        {
            //Create int variable that is dynamically set from PageStyling.js.
            //Counts textboxs submitted and sets below value (Converted from String to Int)
            int arraySize = Convert.ToInt32(numFldVariable.Value);

            //Create variable that takes in all our Lat and Long from destinations
            string variable = hdnfldVariable.Value;

            //Create variable that takes in all our addresses from destinations
            string addresses = returnDetailToDisplay.Value;

            //Create Array called words and use the comma as the split
            string[] tempAddressArray = addresses.Split('#');

            newAddressArray = new string[arraySize];

            for (int counter = 0; counter < arraySize; counter++)
            {
                var original = tempAddressArray[counter];
                //If starts with a comma, removes the comma
                if (original.StartsWith(","))
                {
                    original = original.Substring(1);
                    newAddressArray[counter] = original;
                }
                //The first location in the list doesnt have a comma in front of it so we add it here
                else
                {
                    newAddressArray[counter] = original;
                }
            }

            //Create Array called words and use the comma as the split
            string[] words = variable.Split(',');
            int i = 0;
            lng = new double[words.Length];
            lat = new double[words.Length];

            foreach (string word in words)
            {
                string[] coords = word.Split(':');
                if (coords.Length == 2)
                {
                    //Convert the String to a double
                    lat[i] = Convert.ToDouble(coords[0]);
                    lng[i] = Convert.ToDouble(coords[1]);
                    i++;
                }
            }

            //Declaring algorithm setting variables
            int generations = 100;
            int population = 50;
            int numOfDestinations = arraySize;
            double crossoverRate = 0.25;
            int seed = 1;

            genAlgo = new GeneticAlgorithm(crossoverRate, lng, lat, generations, numOfDestinations, population, seed);

            backgroundWorkerThread = new BackgroundWorker();
            backgroundWorkerThread.WorkerSupportsCancellation = true;

            backgroundWorkerThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorkerThread_RunWorkerCompleted);
            backgroundWorkerThread.DoWork += new DoWorkEventHandler(backgroundWorkerThread_DoWork);

            backgroundWorkerThread.RunWorkerAsync();
            while (!backgroundWorkerThread.IsBusy) { }
        }

        private void backgroundWorkerThread_DoWork(object sender, DoWorkEventArgs e)
        {
            genAlgo.RunGenAlgo(backgroundWorkerThread);
        }

        private void backgroundWorkerThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Get the number of inputs sent ot the algorithm
            int outputArraySize = Convert.ToInt32(numFldVariable.Value);

            //Create array containing addresses
            string[] name = new string[outputArraySize];

            //The start of array needs to start at 0
            genAlgo.Order();

            //Loop assigns the correct location to address
            for (int i = 0; i < outputArraySize; i++)
            {
                int position = genAlgo.MinimumDistanceResult[i];
                name[i] = newAddressArray[position];
            }

            //Numbers for label for optimal routes
            int labelNum = 1;
            //Loop for creating new labels, output labels
            for (int i = 0; i < outputArraySize; i++)
            {
                System.Web.UI.WebControls.Label Label = new System.Web.UI.WebControls.Label();
                resultSide.Controls.Add(Label);

                if (i == 0)
                {
                    Label.Text = "Start: &emsp;" + name[i];
                }
                else
                {
                    Label.Text = "Destination " + labelNum + ": &emsp;" + name[i];
                }

                Label.Attributes.Add("Class", "LabelClass");
                labelNum++;
            }

            string coordsConvertedToString = "";

            for (int count = 0; count < outputArraySize; count++)
            {
                coordsConvertedToString += Convert.ToString(lat[count]) + "#" + Convert.ToString(lng[count]) + ",";
            }

            Page page = HttpContext.Current.CurrentHandler as Page;
            page.ClientScript.RegisterStartupScript(typeof(Page), "Test",
                "<script type='text/javascript'>calcRoute('" + coordsConvertedToString + "');</script>");


            string allNames = "";
            StringBuilder builder = new StringBuilder();
            foreach (string value in name)
            {
                allNames = builder.Append(value + "#").ToString();
            }

            hiddenFavPlaceName.Value = allNames;
            hiddenFavLatLng.Value = coordsConvertedToString;

            returnResults.Visible = true;
        }

        protected void saveToDbButton_Click(object sender, EventArgs e)
        {
            /*string strFaveLAtLng = "";
            string strFavePlaces = "";
            string strNameOfFaveRoute = "";

            if (User.Identity.IsAuthenticated)
            {
                if (String.IsNullOrEmpty(TextBox1.Text))
                {
                    SuccessFail.Text = "Please Enter A Name For Your Route And Then Save";
                }
                else
                {
                    strFaveLAtLng = hiddenFavLatLng.Value.ToString();
                    strFavePlaces = hiddenFavPlaceName.Value.ToString();
                    strNameOfFaveRoute = TextBox1.Text.ToString();

                    SqlConnection con = new SqlConnection();
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "INSERT INTO AspNetUsers(favLatLng, FavNames, routeName)" +
                        " VALUES (@favLatLng, @FavNames, @routeName)";
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;

                    SqlParameter FavLatLng = new SqlParameter("@favLatLng", SqlDbType.NVarChar, 400);
                    FavLatLng.Value = strFaveLAtLng.ToString();
                    cmd.Parameters.Add(FavLatLng);
                    SqlParameter FavNames = new SqlParameter("@FavNames", SqlDbType.NVarChar, 400);
                    FavNames.Value = strFavePlaces.ToString();
                    cmd.Parameters.Add(FavNames);
                    SqlParameter RouteName = new SqlParameter("@routeName", SqlDbType.NVarChar, 50);
                    RouteName.Value = strNameOfFaveRoute.ToString();
                    cmd.Parameters.Add(RouteName);

                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    con.Close();

                    SuccessFail.Text = "Successfully Saved Your Favourite Route";
                }
            }

            TextBox1.Text = string.Empty;
            hiddenFavPlaceName.Value = string.Empty;
            hiddenFavLatLng.Value = string.Empty;
            */
        }
    }
}