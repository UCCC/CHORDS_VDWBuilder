using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace CHORDS_VDWBuilder.CHORDS.FHIRToVDW
{
    internal interface IGeocode
    {
        HttpResponseMessage GetResponse(string url, string parameters);
    }

    public class Geocode<T> : IGeocode
    where T : class
    {
        private T service;

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string CongressionalDistrictName { get; set; }

        public Geocode()
        {
        }

        public T Service
        {
            get
            {
                return service;
            }
            set
            {
                service = value;
            }
        }

        public HttpResponseMessage GetResponse(string url, string parameters)
        {
            // SSL/TLS Type
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add
                (new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(parameters).Result;
                return response;
            }
        }
    }

    internal interface ICensus
    {
        Geocode<Census> GeocodeAddress
        (string street, string city, string state, string zip);
    }

    public class Census : ICensus
    {
        private string apiUrl;
        private string apiParameters;

        public Result result { get; set; }

        public Census()
        {
            apiUrl = @"https://geocoding.geo.census.gov/geocoder/geographies/address";

            // Parameters: Benchmark, Vintage, Format, Layers
            // 56 - 115th Congressional Districts
            // 86 - Counties
            // For more information on layers, 
            // https://tigerweb.geo.census.gov/arcgis/rest/services/TIGERweb/tigerWMS_Current/MapServer
            apiParameters = @"&benchmark=4&vintage=4&format=json&layers=54,86";
        }

        public class Result
        {
            public AddressMatches[] addressMatches { get; set; }
        }

        public class Address
        {
            public string state { get; set; }
            public string street { get; set; }
            public string city { get; set; }
        }

        public class AddressMatches
        {
            public Geographies geographies { get; set; }
            public string matchedAddress { get; set; }
            public Coordinates coordinates { get; set; }
            public AddressComponents addressComponents { get; set; }
        }

        public class Geographies
        {
            [JsonProperty("115th Congressional Districts")]
            public CongressionalDistricts[] congressionalDistricts { get; set; }
            public Counties[] counties { get; set; }
            [JsonProperty("Census Blocks")]
            public CensusBlocks[] censusblocks { get; set; }
        }

        public class CensusBlocks
        {
            public string GEOID { get; set; }
        }

        public class Counties
        {
            public int STATE { get; set; }
            public string NAME { get; set; }
            public string BASENAME { get; set; }
        }

        public class CongressionalDistricts
        {
            public string NAME { get; set; }
            public int CDSESSN { get; set; }
            public string BASENAME { get; set; }
        }

        public class Coordinates
        {
            public double x { get; set; }
            public double y { get; set; }
        }

        public class AddressComponents
        {
            public string preDirection { get; set; }
            public string preType { get; set; }
            public string streetName { get; set; }
            public string suffixType { get; set; }
            public string toAddress { get; set; }
            public string preQualifier { get; set; }
            public string suffixDirection { get; set; }
            public string suffixQualifier { get; set; }
            public string fromAddress { get; set; }
            public string state { get; set; }
            public string zip { get; set; }
            public string city { get; set; }
        }

        public Geocode<Census> GeocodeAddress
              (string street, string city = "",
              string state = "", string zip = "")
        {
            Census json = null;
            var geocode = new Geocode<Census>();

            HttpResponseMessage response = geocode.GetResponse
                  (this.apiUrl, this.GetUrlParameters(street, city, state, zip));
            using (response)
            {
                if (response.IsSuccessStatusCode)
                {
                    var data = response.Content.ReadAsStringAsync().Result;
                    json = JsonConvert.DeserializeObject<Census>(data);

                    if (json != null)
                    {
                        geocode.Latitude = json.result.addressMatches[0].coordinates.x;
                        geocode.Longitude =
                        json.result.addressMatches[0].coordinates.y;
                        geocode.CongressionalDistrictName =
                        json.result.addressMatches[0].geographies.congressionalDistricts[0].NAME;
                    }
                }
                return geocode;
            }
        }

        internal string GetUrlParameters
              (string street,
              string city = "", string state = "", string zip = "")
        {
            string urlParameters = "";
            urlParameters += "?street=" + street;
            if (city.Length > 0) urlParameters += "&city=" + city;
            if (state.Length > 0) urlParameters += "&state=" + state;
            if (zip.Length > 0) urlParameters += "&zip=" + zip;

            // Append API Parameters
            urlParameters += this.apiParameters;
            return urlParameters;
        }
    }
}
