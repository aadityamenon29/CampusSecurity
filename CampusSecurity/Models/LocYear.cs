using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CampusSecurity.Models
{
    public class LocYear
    {
        readonly int year;
        readonly string name;
        readonly string state;
        readonly string location;

        public int Year { get { return year; } }
        public string Name { get { return name; } }
        public string State { get { return state; } }
        public string Location { get { return location; } }

        public LocYear(int year, string name, string state, string location)
        {
            this.year = year;
            this.name = name;
            this.state = state;
            this.location = location;
        }

        LocYear()
        { }
    }


    public class AdvSearchObject
    {
        public List<int> rows;
        public List<int> Rows { get { return rows; } }
        
    }


    public class TrendsPie
    {
        readonly String subType_name;
        readonly int incidents;

        public String SubType_name { get { return subType_name; } }

        public int Incidents { get { return incidents; } }

        public TrendsPie(String subType_name, int incidents)
        {
            this.subType_name = subType_name;
            this.incidents= incidents;
            
        }


    }
    public class TrendsObject
    {

        readonly String uni_name;
        public String Uni_name { get { return uni_name; } }

        readonly int year11;
        readonly int year12;
        readonly int year13;
        readonly int year14;
        readonly int year15;

        public int Year11 { get { return year11; } }
        public int Year12 { get { return year12; } }
        public int Year13 { get { return year13; } }
        public int Year14 { get { return year14; } }
        public int Year15 { get { return year15; } }

        public TrendsObject(String uni_name, int year11, int year12, int year13, int year14, int year15)
        {
            this.uni_name = uni_name;
            this.year11 = year11;            
            this.year12 = year12;
            this.year13 = year13;
            this.year14 = year14;
            this.year15 = year15;
        }

    }

    public class RankingsObject
    {
        readonly int incidents;
        readonly String uni_name;
        readonly String branch_name;
        readonly String city_name;
        readonly String state_name;
        public int Incidents { get { return incidents; } }
        public String Uni_name { get { return uni_name; } }
        public String Branch_name { get { return branch_name; } }
        public String City_name { get { return city_name; } }
        public String State_name { get { return state_name; } }

        public RankingsObject(String uni_name, String branch_name, String city_name, String state_name, int incidents)
        {
            this.incidents = incidents;
            this.uni_name = uni_name;
            this.branch_name = branch_name;
            this.city_name = city_name;
            this.state_name = state_name;
        }

    }


    public class Discipline
    {
        readonly int id;
        readonly int drug;
        readonly int weapon;
        readonly int liquor;



        public int Id { get { return id; } }
        public int Drug { get { return drug; } }
        public int Weapon { get { return weapon; } }
        public int Liquor { get { return liquor; } }

        public Discipline(int id, int drug, int weapon, int liquor)
        {
            this.id = id;
            this.drug = drug;
            this.weapon = weapon;
            this.liquor = liquor;
        }

        Discipline()
        { }
    }

    public class Criminal_Offense
    {
        readonly int id;
        readonly int burgla;
        readonly int murd;
        readonly int vehic;
        readonly int neg_m;
        readonly int robbe;
        readonly int forcib;
        readonly int nonfor;
        readonly int agg_a;
        readonly int arson;



        public int Id { get { return id; } }
        public int Burgla { get { return burgla; } }
        public int Murd { get { return murd; } }
        public int Vehic { get { return vehic; } }

        public int Neg_m { get { return neg_m; } }
        public int Robbe { get { return robbe; } }
        public int Forcib { get { return forcib; } }
        public int Nonfor { get { return nonfor; } }

        public int Agg_a { get { return agg_a; } }
        public int Arson { get { return arson; } }

        public Criminal_Offense(int id, int burgla, int murd, int vehic, int neg_m,
        int robbe,
        int forcib,
        int nonfor,
        int agg_a,
        int arson)
        {
            this.id = id;
            this.burgla = burgla;
            this.murd = murd;
            this.vehic = vehic;
            this.neg_m = neg_m;
            this.robbe = robbe;
            this.nonfor = nonfor;
            this.agg_a = agg_a;
            this.arson = arson;

        }

        Criminal_Offense()
        { }
    }

    public class VAWA
    {
        readonly int id;
        readonly int Stalking;
        readonly int Dating_violence;
        readonly int Domestic_violence;



        public int Id { get { return id; } }
        public int stalking { get { return Stalking; } }
        public int dating_violence { get { return Dating_violence; } }
        public int domestic_violence { get { return Domestic_violence; } }

        public VAWA(int id, int Stalking, int Dating_violence, int Domestic_violence)
        {
            this.id = Id;
            this.Stalking = Stalking;
            this.Dating_violence = Dating_violence;
            this.Domestic_violence = Domestic_violence;
        }

        VAWA()
        { }
    }


    public class Arrests
    {
        readonly int id;
        readonly int drug;
        readonly int weapon;
        readonly int liquor;

        public int Id { get { return id; } }
        public int Drug { get { return drug; } }
        public int Weapon { get { return weapon; } }
        public int Liquor { get { return liquor; } }

        public Arrests(int id, int drug, int weapon, int liquor)
        {
            this.id = id;
            this.drug = drug;
            this.weapon = weapon;
            this.liquor = liquor;
        }

        Arrests()
        { }
    }
}