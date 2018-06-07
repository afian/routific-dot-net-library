using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Routific
{
    public class VRP
    {

        private string routingShortEndpoint = "/vrp";
        private string routingLongEndpoint = "/vrp-long";

        public VRP()
        {

            Visits = new Dictionary<string, Visits>();
            Fleet = new Dictionary<string, Vehicle>();
            Options = new Dictionary<string, dynamic>();
        }
        [JsonProperty("visits")]
        public Dictionary<string, Visits> Visits { get; set; }

        [JsonProperty("fleet")]
        public Dictionary<string, Vehicle> Fleet { get; set; }

        [JsonProperty("options")]
        public Dictionary<string, dynamic> Options { get; set; }

        public string getLongRouting()
        {
            return this.routingLongEndpoint;
        }

        public string getShortRouting()
        {
            return this.routingShortEndpoint;
        }

        public void addVisit(string id, Visits visit)
        {
            try
            {
                Visits list = new Visits();
                //list.Add(visit);
                this.Visits.Add(id, visit);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
                   
        }

        public void addVehicle(string id, Vehicle vehicle)
        {
            try
            {
                this.Fleet.Add(id, vehicle);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
 
        }

        public void addOption(string id, dynamic option)
        {
            try
            {
                this.Options.Add(id, option);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

    }


    // Input 

    public partial class Vehicle
    {
        [JsonProperty("start_location")]
        public Location StartLocation { get; set; }

        [JsonProperty("end_location")]
        public Location EndLocation { get; set; }

        [JsonProperty("shift_start")]
        public string ShiftStart { get; set; }

        [JsonProperty("shift_end")]
        public string ShiftEnd { get; set; }
        [JsonProperty("min_visits")]
        public int MinVisits { get; set; }
        [JsonProperty("strict_start")]
        public bool StrictStart { get; set; }

        [JsonProperty("breaks")]
        public List<Break> breaks { get; set; }
    }

    public partial class Break
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("start")]
        public string Start { get; set; }
        [JsonProperty("end")]
        public string End { get; set; }
        [JsonProperty("in_transit")]
        public bool InTransit { get; set; }
    }

    public partial class Options
    {
        [JsonProperty("traffic")]
        public string Traffic { get; set; }

        [JsonProperty("min_visits_per_vehicle")]
        public long MinVisitsPerVehicle { get; set; }

        [JsonProperty("balance")]
        public bool Balance { get; set; }

        [JsonProperty("min_vehicles")]
        public bool MinVehicles { get; set; }

        [JsonProperty("shortest_distance")]
        public bool ShortestDistance { get; set; }

        [JsonProperty("squash_durations")]
        public long SquashDurations { get; set; }

        [JsonProperty("max_vehicle_overtime")]
        public long MaxVehicleOvertime { get; set; }

        [JsonProperty("max_visit_lateness")]
        public long MaxVisitLateness { get; set; }
    }

    public partial class Location
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lng")]
        public double Lng { get; set; }
    }

    public partial class Visits
    {
        [JsonProperty("location")]
        public Location Location { get; set; }

        [JsonProperty("start")]
        public string Start { get; set; }

        [JsonProperty("end")]
        public string End { get; set; }

        [JsonProperty("duration")]
        public long Duration { get; set; }
    }

    // Solution
    public class Solution
    {
        public string vehicle { get; set; }
        public List<Route> route { get; set; }
    }
    public class Route

    {
        [JsonProperty("location_id")]
        public string location_id { get; set; }
        [JsonProperty("location_name")]
        public string location_name { get; set; }
        [JsonProperty("arrival_time")]
        public string arrival_time { get; set; }
        [JsonProperty("finish_time")]
        public string finish_time { get; set; }
        public string type { get; set; }
    }

    public class VRPObject
    {
        public string status { get; set; }
        public int total_travel_time { get; set; }
        public int total_idle_time { get; set; }
        public int total_break_time { get; set; }
        public int num_unserved { get; set; }
        public object unserved { get; set; }
        //public Dictionary<string, List<Solution>> solution { get; set; }
        public List<Solution> solution { get; set; }
        public int total_working_time { get; set; }
        public int num_late_visits { get; set; }
    }


    public partial class LongResultVRP
    {
        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("id")]
        public string InputId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("createdAt")]
        public System.DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("requestId")]
        public string RequestId { get; set; }


        [JsonProperty("finished_at")]
        public System.DateTimeOffset FinishedAt { get; set; }

        [JsonProperty("output")]
        public VRPObject Output { get; set; }

        [JsonProperty("fetchedCount")]
        public long FetchedCount { get; set; }
    }

}
