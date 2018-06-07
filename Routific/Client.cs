using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Routific;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Routific.Respone.Error;

namespace Routific.Client
{
    public class Client
    {

        public string url;
        public int version;
        public string token;
        public int pollDelay;

        public Client()
        {
            url = baseConfig.url;
            version = baseConfig.version;
            pollDelay = baseConfig.pollDelay;
            token = System.Configuration.ConfigurationManager.AppSettings["accessToken"];

        }
        

        // Gets a configuration value from either the `configuration` or the default
        //public dynamic getConfig(Configuration configuration, string key)
        //{
        //    if (configuration != null)
        //        return configuration.+ "key";
        //    return System.Configuration.ConfigurationManager.AppSettings[key];
        //}

        // Returns the full url for a given endpoint.
        public string endpoint(Client client, string endpointPath)
        {
            return client.url + "/v" + this.version + endpointPath;
        }

        public async Task<string> postRequest(Client client2, string endpointPath, Dictionary<string, string> data)
        {
            var url = endpoint(client2, endpointPath);
            using (var client = new HttpClient())
            {
                var values = data;

                var content = new FormUrlEncodedContent(values);

                var response = await client.PostAsync(url, content);

                var responseString = await response.Content.ReadAsStringAsync();

                return responseString;
            }
        }

        public JObject Login(string email, string password)
        {
            var self = this;
            var values = new Dictionary<string, string>
            {
                { "email", email },
                { "password", password }
            };

            dynamic respone = postRequest(this, "/users/login", values).Result;
            var x = JObject.Parse(respone);
            
            if (x["token"] != null)
            {
                //this.token = x["token"].ToString();
            }

            return x;
        }





        public dynamic Long_Route_PDP(PDP pdp)
        {

            string endpointPath = "";

            endpointPath = pdp.getLongRoutingPDP();

            // Define your target
            string url = this.url + "/v" + this.version + endpointPath;
        
            // Console.Write(this.token);
            try
            {


                var json = JsonConvert.SerializeObject(pdp);
                PDPObject root = new PDPObject();
                WebRequest myReq = WebRequest.Create(url);
                myReq.Method = "POST";
                myReq.ContentType = "application/json; charset=UTF-8";
                myReq.Headers.Add("Authorization:Bearer " + this.token);
               
                using (var streamWriter = new StreamWriter(myReq.GetRequestStream()))
                {

                    streamWriter.Write(json);

                    streamWriter.Flush();
                    streamWriter.Close();
                }
                var result = "";
                using (HttpWebResponse response = myReq.GetResponse() as HttpWebResponse)
                {
                    int statusCode = (int)response.StatusCode;
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    result = reader.ReadToEnd();

                }
                try
                {


                    dynamic json_string = JsonConvert.DeserializeObject(result);

                    var respone = this.jobPoll(json_string["job_id"].ToString());

                    List<SolutionPDP> slt = new List<SolutionPDP>();

                    dynamic responejson = JsonConvert.DeserializeObject(respone.ToString());
                    dynamic output = JsonConvert.DeserializeObject(respone["output"].ToString());
                    dynamic dynJson = JsonConvert.DeserializeObject(respone["output"]["solution"].ToString());

                    //dynamic solution = JsonConvert.DeserializeObject(dynJson.solution.ToString());

                    foreach (var vehicle_item in dynJson)
                    {

                        List<RoutePDP> d = JsonConvert.DeserializeObject<List<RoutePDP>>(vehicle_item.Value.ToString());
                        SolutionPDP m = new SolutionPDP();
                        m.vehicle = vehicle_item.Name;
                        m.route = d;
                        slt.Add(m);
                    }


                    root.status = output.status;
                    root.total_travel_time = output.total_travel_time;
                    root.total_break_time = output.total_break_time;
                    root.total_idle_time = output.total_idle_time;
                    root.num_unserved = output.num_unserved;
                    root.unserved = output.unserved;

                    root.solution = slt;

                    LongResultPDP l = new LongResultPDP();
                    l.Id = responejson._id;
                    l.Status = responejson.status;
                    l.InputId = responejson.id;
                    l.RequestId = responejson.requestId;
                    l.CreatedAt = responejson.createdAt;
                    l.FinishedAt = responejson.finished_at;
                    l.Output = root;
                    l.FetchedCount = responejson.fetchedCount;

                    return l;
                }
                catch
                {
                    Console.WriteLine("job Id fail");
                    return null;
                }
            }
            catch (WebException ex)
            {
                var resp = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                dynamic obj = JsonConvert.DeserializeObject(resp);
                return obj;
                //Console.WriteLine(obj);
                //throw new Exception(ex.Message);

            }

        }


        public dynamic Long_Route_VRP(VRP vrp)
        {

            string endpointPath = "";

            endpointPath = vrp.getLongRouting();
            dynamic json_string = "";
            // Define your target
            string url = this.url + "/v" + this.version + endpointPath;

            try
            {
                var json = JsonConvert.SerializeObject(vrp);
                VRPObject root = new VRPObject();

                WebRequest myReq = WebRequest.Create(url);
                myReq.Method = "POST";
                myReq.ContentType = "application/json; charset=UTF-8";
                myReq.Headers.Add("Authorization:Bearer " + this.token);

                using (var streamWriter = new StreamWriter(myReq.GetRequestStream()))
                {

                    streamWriter.Write(json);
                    streamWriter.Flush();
                    streamWriter.Close();
                }
                var result = "";
                using (HttpWebResponse response = myReq.GetResponse() as HttpWebResponse)
                {
                    int statusCode = (int)response.StatusCode;
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    result = reader.ReadToEnd();

                }
                try
                {
                    json_string = JsonConvert.DeserializeObject(result);
                    string job_id = json_string["job_id"].ToString();

                    var respone = this.jobPoll(job_id);

                    List<Solution> slt = new List<Solution>();
                    dynamic responejson = JsonConvert.DeserializeObject(respone.ToString());
                    dynamic output = JsonConvert.DeserializeObject(respone["output"].ToString());
                    dynamic dynJson = JsonConvert.DeserializeObject(respone["output"]["solution"].ToString());

                    //dynamic solution = JsonConvert.DeserializeObject(dynJson.solution.ToString());

                    foreach (var vehicle_item in dynJson)
                    {

                        List<Route> d = JsonConvert.DeserializeObject<List<Route>>(vehicle_item.Value.ToString());
                        Solution m = new Solution();
                        m.vehicle = vehicle_item.Name;
                        m.route = d;
                        slt.Add(m);
                    }


                    root.status = output.status;
                    root.total_travel_time = output.total_travel_time;
                    root.total_break_time = output.total_break_time;
                    root.total_idle_time = output.total_idle_time;
                    root.num_unserved = output.num_unserved;
                    root.unserved = output.unserved;

                    root.solution = slt;

                    LongResultVRP l = new LongResultVRP();
                    l.Id = responejson._id;
                    l.Status = responejson.status;
                    l.InputId = responejson.id;
                    l.RequestId = responejson.requestId;
                    l.CreatedAt = responejson.createdAt;
                    l.FinishedAt = responejson.finished_at;
                    l.Output = root;
                    l.FetchedCount = responejson.fetchedCount;

                    return l;
                }
                catch
                {
                    Console.WriteLine("job Id fail");
                    return null;
                }
            }
            catch (WebException ex)
            {
                var resp = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                dynamic obj = JsonConvert.DeserializeObject(resp);
                //Console.WriteLine(obj);
                //throw new Exception(ex.Message);
                return obj;

            }

        }

        public async Task<string> getRequest(Client client, string endpointPath)
        {
            var url = endpoint(client, endpointPath);

            HttpClient client2 = new HttpClient();
            HttpResponseMessage response = await client2.GetAsync(url);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }
        // Performs calls to the jobs endpoint until it is processed.

        public JObject jobPoll(string jobId)
        {
            try
            {
                var self = this;
                var res = this.getRequest(this, "/jobs/" + jobId).Result;
                var x = JObject.Parse(res);

                if (x["status"].ToString() == "error")
                {
                    Console.WriteLine("error");
                    return null;
                }
                while (x["status"].ToString() != "finished")
                {
                    Task.Delay(self.pollDelay);
                    return this.jobPoll(jobId);
                }

                return x;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                //throw new RoutificException(ex.Message);

            }


        }



    }
}
