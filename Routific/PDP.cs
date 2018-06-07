using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Routific
{
    public class PDP
    {
        private string routingShortEndpoint = "/pdp";
        private string routingLongEndpoint = "/pdp-long";

        public PDP()
        {

            Visits = new Dictionary<string, VisitsPDP>();
            Fleet = new Dictionary<string, VehiclePDP>();
            Options = new Dictionary<string, dynamic>();

        }
        [JsonProperty("visits")]
        public Dictionary<string, VisitsPDP> Visits { get; set; }

        [JsonProperty("fleet")]
        public Dictionary<string, VehiclePDP> Fleet { get; set; }

        [JsonProperty("options")]
        public Dictionary<string, dynamic> Options { get; set; }

        public void addVisitPDP(string id, VisitsPDP visit)
        {
            try
            {
                Visits list = new Visits();
                //list.Add(visit);
                this.Visits.Add(id, visit);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void addVehiclePDP(string id, VehiclePDP vehicle)
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

        public string getLongRoutingPDP()
        {
            return this.routingLongEndpoint;
        }

        public string getShortRoutingPDP()
        {
            return this.routingShortEndpoint;
        }

        public void addOptionPDP(string id, dynamic option)
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

    /* Input */

    public partial class VisitsPDP

    {
        [JsonProperty("load")]
        public int Load { get; set; }
        [JsonProperty("pickup")]
        public Pickup Pickup { get; set; }
        [JsonProperty("dropoff")]
        public Dropoff Dropoff { get; set; }

    }

    public partial class Pickup

    {
        [JsonProperty("location")]
        public LocationPDP Location { get; set; }
        [JsonProperty("start")]
        public string Start { get; set; }
        [JsonProperty("end")]
        public string End { get; set; }
        [JsonProperty("duration")]
        public int Duration { get; set; }

    }



    public partial class LocationPDP

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



    public partial class Dropoff

    {
        [JsonProperty("location")]
        public LocationPDP Location { get; set; }
        [JsonProperty("start")]
        public string Start { get; set; }
        [JsonProperty("end")]
        public string End { get; set; }
        [JsonProperty("duration")]
        public int Duration { get; set; }

    }

    public partial class VehiclePDP
    {
        [JsonProperty("start_location")]
        public LocationPDP StartLocation { get; set; }

        [JsonProperty("end_location")]
        public LocationPDP EndLocation { get; set; }

        [JsonProperty("shift_start")]
        public string ShiftStart { get; set; }

        [JsonProperty("shift_end")]
        public string ShiftEnd { get; set; }

        [JsonProperty("capacity")]
        public int Capacity { get; set; }
    }


    /* Out */

    public partial class LongResultPDP
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
        public PDPObject Output { get; set; }

        [JsonProperty("fetchedCount")]
        public long FetchedCount { get; set; }
    }

    public class PDPObject

    {

        public string status { get; set; }

        public int total_travel_time { get; set; }
        public int total_break_time { get; set; }
        public int total_idle_time { get; set; }

        public int num_unserved { get; set; }

        public object unserved { get; set; }

        public List<SolutionPDP> solution { get; set; }

    }



    public class SolutionPDP

    {
        public string vehicle { get; set; }

        public List<RoutePDP> route { get; set; }

    }

    public class RoutePDP

    {
        public string location_id { get; set; }

        public string location_name { get; set; }

        public string arrival_time { get; set; }

        public string finish_time { get; set; }

        public string type { get; set; }

    }

    public partial class OptionsPDP
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


}
