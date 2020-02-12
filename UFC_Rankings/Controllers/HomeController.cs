using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UFC_Rankings.Models;

namespace UFC_Rankings.Controllers
{
    public class HomeController : Controller
    {
        private Context db = new Context();

        HttpClient Ranking_API = new HttpClient()
        {
            BaseAddress = new Uri("https://api.sportradar.us/ufc/trial/v2/en/rankings.json?api_key=n7abkrz2txkrsj9nmfrac259")
        };

        public async Task<ActionResult> Index()
        {
            HttpResponseMessage response = await Ranking_API.GetAsync("");
            var rsp = "";
            var li2 = new List<Ranking>();
            if (response.IsSuccessStatusCode)
            {
                rsp = await response.Content.ReadAsStringAsync();

                var li = JObject.Parse(rsp).ToObject<RootObject>();

                foreach (var i in li.rankings)
                {

                    if (i.name.Contains("_"))
                    {
                        i.name = i.name.Replace("_", " ");
                    }
                    foreach(var competitor in i.competitor_rankings)
                    {
                        int index = competitor.competitor.name.IndexOf(" ");
                        if (index > 0)
                        {
                            competitor.competitor.lname = competitor.competitor.name.Substring(0, index - 1);
                            competitor.competitor.name = competitor.competitor.name.Substring(index + 1, competitor.competitor.name.Length - index - 1);
                        }
                    }
                    li2.Add(i);
                }
            }
            else
            {
                rsp = "nothing";
            }
            return View(li2);
        }
    }
}
