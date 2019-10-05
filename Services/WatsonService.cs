using IBM.Cloud.SDK.Core.Authentication.Iam;
using IBM.Watson.PersonalityInsights.v3.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace IBM.Watson.PersonalityInsights.v3.Examples
{
    public class ServiceExample
    {
        string apikey = "PERSONALITY_INSIGHTS_APIKEY";
        string url = "PERSONALITY_INSIGHTS_URL";
        string versionDate = "2017-10-13";

        static void Main(string[] args)
        {
            ServiceExample example = new ServiceExample();

            example.Profile();
            example.ProfileAsCsv();

            Console.WriteLine("Examples complete. Press any key to close the application.");
            Console.ReadKey();
        }

        #region Profile
        public void Profile()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "PERSONALITY_INSIGHTS_APIKEY");

            PersonalityInsightsService service = new PersonalityInsightsService("2017-10-13", authenticator);
            service.SetServiceUrl("PERSONALITY_INSIGHTS_URL");

            Content content = null;
            content = JsonConvert.DeserializeObject<Content>(File.ReadAllText("profile.json"));

            var result = service.Profile(
                content: content,
                contentType: "application/json",
                rawScores: true,
                consumptionPreferences: true
                );

            Console.WriteLine(result.Response);
        }

        public void ProfileAsCsv()
        {
            IamAuthenticator authenticator = new IamAuthenticator(
                apikey: "PERSONALITY_INSIGHTS_APIKEY");

            PersonalityInsightsService service = new PersonalityInsightsService("2017-10-13", authenticator);
            service.SetServiceUrl("PERSONALITY_INSIGHTS_URL");

            Content content = null;
            content = JsonConvert.DeserializeObject<Content>(File.ReadAllText("profile.json"));

            var result = service.ProfileAsCsv(
                content: content,
                contentType: "application/json",
                consumptionPreferences: true,
                rawScores: true,
                csvHeaders: true
                );

            using (FileStream fs = File.Create("output.csv"))
            {
                result.Result.WriteTo(fs);
                fs.Close();
                result.Result.Close();
            }
        }
        #endregion
    }
}
