using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.IO; // for File operation
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting.Server;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : ControllerBase
    {
        public HomeController() {
            

        }

        static byte[] Compress(string input)
        {
            byte[] inputData = Encoding.UTF8.GetBytes(input);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
                {
                    gzipStream.Write(inputData, 0, inputData.Length);
                }

                return memoryStream.ToArray();
            }
        }

        static string Decompress(byte[] compressedData)
        {
            using (MemoryStream memoryStream = new MemoryStream(compressedData))
            using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
            using (StreamReader reader = new StreamReader(gzipStream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        // Create a string get endpoint
        [HttpGet]
        [Route("getstring")]
        public IActionResult GetString()
        {
            string responseString = "Hello, this is a sample API response string!";
            return Ok(responseString);

        }



        [HttpGet]
        public byte[] Get()
        {
            StreamReader r = new StreamReader("C:/Users/Moram.Reddy/source/repos/WebApplication1/WebApplication1/jsonsample.json");
            string jsonfile = r.ReadToEnd();

            string jsonDatatest = "C:\\Users\\Moram.Reddy\\source\\repos\\WebApplication1\\WebApplication1\\jsonsample.json";
            string jsonData1 = "{ \r\n \"page\": 1, \r\n \"per_page\": 6, \r\n \"total\": 12, \r\n \"total_pages\":14, \r\n \"data\": [\r\n     {\r\n      \"id\": 1,\r\n      \"name\": \"cerulean\",\r\n      \"year\": 2000,\r\n      \"color\": \"#98B2D1\",\r\n      \"pantone_value\": \"15-4020\"\r\n     },\r\n     {\r\n      \"id\": 2,\r\n      \"name\": \"fuchsia rose\", \r\n      \"year\": 2001, \r\n      \"color\": \"#C74375\",\r\n      \"pantone_value\": \"17-2031\"\r\n     },\r\n     {\r\n      \"id\": 3, \r\n      \"name\": \"true red\", \r\n      \"year\": 2002, \r\n      \"color\": \"#BF1932\", \r\n      \"pantone_value\": \"19-1664\" \r\n     }, \r\n     {\r\n      \"id\": 4, \r\n      \"name\": \"aqua sky\",\r\n      \"year\": 2003,\r\n      \"color\": \"#7BC4C4\",\r\n      \"pantone_value\": \"14-4811\"\r\n     },\r\n     { \r\n      \"id\": 5,\r\n      \"year\": 2004, \r\n      \"color\": \"#E2583E\", \r\n      \"pantone_value\": \"17-1456\" \r\n     },\r\n     {  \r\n      \"id\": 6, \r\n      \"name\": \"blue turquoise\", \r\n      \"year\": 2005,            \r\n      \"color\": \"#53B0AE\",\r\n      \"pantone_value\": \"15-5217\"\r\n     }\r\n      ]\r\n}";
            string jsonData = @"
        {
            ""id"": ""0001"",
            ""type"": ""donut"",
            ""name"": ""Cake"",
            ""ppu"": 0.55,
            ""batters"": {
                ""batter"": [
                    { ""id"": ""1001"", ""type"": ""Regular"" },
                    { ""id"": ""1002"", ""type"": ""Chocolate"" },
                    { ""id"": ""1003"", ""type"": ""Blueberry"" },
                    { ""id"": ""1004"", ""type"": ""Devil's Food"" }
                ]
            },
            ""topping"": [
                { ""id"": ""5001"", ""type"": ""None"" },
                { ""id"": ""5002"", ""type"": ""Glazed"" },
                { ""id"": ""5005"", ""type"": ""Sugar"" },
                { ""id"": ""5007"", ""type"": ""Powdered Sugar"" },
                { ""id"": ""5006"", ""type"": ""Chocolate with Sprinkles"" },
                { ""id"": ""5003"", ""type"": ""Chocolate"" },
                { ""id"": ""5004"", ""type"": ""Maple"" }
            ]
        }";

            // Compress JSON data
            byte[] compressedData = Compress(jsonfile);

            // Decompress JSON data
            string decompressedJsonData = Decompress(compressedData);

            Console.WriteLine("compressed JSON:");
            Console.WriteLine(compressedData);

            Console.WriteLine("Decompressed JSON:");
            Console.WriteLine(decompressedJsonData);

            return compressedData;
        }
    }
}
