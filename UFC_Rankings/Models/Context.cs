using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UFC_Rankings.Models
{
    public class Context : DbContext
    {
        public DbSet<Competitor> Competitors { get; set; }
    }

    public class Competitor
    {
        public string id { get; set; }
        public string name { get; set; }
        public string lname { get; set; }
        public string abbreviation { get; set; }
    }

    public class CompetitorRanking
    {
        public int rank { get; set; }
        public int movement { get; set; }
        public Competitor competitor { get; set; }
    }

    public class Ranking
    {
        public int type_id { get; set; }
        public string name { get; set; }
        public int year { get; set; }
        public int week { get; set; }
        public List<CompetitorRanking> competitor_rankings { get; set; }
    }

    public class RootObject
    {
        public DateTime generated_at { get; set; }
        public List<Ranking> rankings { get; set; }
    }
}