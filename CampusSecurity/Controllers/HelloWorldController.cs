using CampusSecurity.Models;
using Oracle.ManagedDataAccess.Client;
using System.Web;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace MvcMovie.Controllers
{
    public class HelloWorldController : Controller
    {

        // 
        // GET: /HelloWorld/ 
        static string connectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.cise.ufl.edu)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)));User Id = ; Password = ;";
        OracleConnection connection = new OracleConnection(connectionString);
        public string Index()
        {
            return "This is my <b>default</b> action...";
        }

        // 
        // GET: /HelloWorld/Welcome/ 

        public ActionResult Welcome()
        {
            return View();//"This is the Welcome action method...";
        }
        [HttpGet]
        public ActionResult Search()
        {
            UIModel objUI = new UIModel();
           //LINQ query executed first time search is loaded. List of uni returned, converted to select list and added to objIU object
            objUI.University = getUniversity().Select(c => new SelectListItem
            {
                Text = c.ToString(),
                Value = c.ToString()
            }).ToList();
            List<int> lstYear = new List<int> { 2011, 2012, 2013, 2014, 2015 };
            objUI.Year = lstYear.Select(c => new SelectListItem
            {
                Text = c.ToString(),
                Value = c.ToString()
            }).ToList();
            List<String> lstLoc = new List<string> { "Non-campus", "On-campus", "Public Property", "Reported by Local Police", "Residence Hall" };
            List<String> lstType = new List<string> { "Criminal Offense", "Discipline", "Arrests","Violence Against Women"};
            objUI.Location = lstLoc.Select(c => new SelectListItem
            {
                Text = c.ToString(),
                Value = c.ToString()
            }).ToList();
            lstType.Sort();
            objUI.Type = lstType.Select(c => new SelectListItem
            {
                Text = c.ToString(),
                Value = c.ToString()
            }).ToList();
            return View(new Tuple<UIModel,SearchViewModel>(objUI,null));
        }




        [HttpGet]
        public ActionResult AdvSearch()
        {
            UIModel objUI = new UIModel();
            //LINQ query executed first time search is loaded. List of uni returned, converted to select list and added to objIU object
            objUI.University = getUniversityForAdv().Select(c => new SelectListItem
            {
                Text = c.ToString(),
                Value = c.ToString()
            }).ToList();
            List<int> lstYear = new List<int> { 2011, 2012, 2013, 2014, 2015 };
            objUI.Year = lstYear.Select(c => new SelectListItem
            {
                Text = c.ToString(),
                Value = c.ToString()
            }).ToList();
            List<String> lstLoc = new List<string> { "Non-campus", "On-campus", "Public Property", "Reported by Local Police", "Residence Hall" };
            List<String> lstType = new List<string> { "Criminal Offense", "Discipline", "Arrests", "Violence Against Women"};
            objUI.Location = lstLoc.Select(c => new SelectListItem
            {
                Text = c.ToString(),
                Value = c.ToString()
            }).ToList();
            lstType.Sort();
            objUI.Type = lstType.Select(c => new SelectListItem
            {
                Text = c.ToString(),
                Value = c.ToString()
            }).ToList();
            return View(new Tuple<UIModel, SearchViewModel>(objUI, null));
        }

        //-------------------------

        [HttpGet]
        public ActionResult Rankings()
        {
            UIModel objUI = new UIModel();
            //LINQ query executed first time search is loaded. List of uni returned, converted to select list and added to objIU object
            
            List<int> lstYear = new List<int> { 2011, 2012, 2013, 2014, 2015 };
            objUI.Year = lstYear.Select(c => new SelectListItem
            {
                Text = c.ToString(),
                Value = c.ToString()
            }).ToList();
            
            List<String> lstType = new List<string> { "Criminal Offense", "Discipline", "Arrests", "Violence Against Women" };
            
            lstType.Sort();
            objUI.Type = lstType.Select(c => new SelectListItem
            {
                Text = c.ToString(),
                Value = c.ToString()
            }).ToList();
            return View(new Tuple<UIModel, SearchViewModel>(objUI, null));
        }


        //-------------------------------------------------------------------

        [HttpGet]
        public ActionResult Trends()
        {
            UIModel objUI = new UIModel();
            //LINQ query executed first time search is loaded. List of uni returned, converted to select list and added to objIU object

            objUI.University = getUniversityForTrends().Select(c => new SelectListItem
            {
                Text = c.ToString(),
                Value = c.ToString()
            }).ToList();


            List<String> lstType = new List<string> { "Criminal Offense", "Discipline", "Arrests", "Violence Against Women" };

            lstType.Sort();
            objUI.Type = lstType.Select(c => new SelectListItem
            {
                Text = c.ToString(),
                Value = c.ToString()
            }).ToList();


            return View(new Tuple<UIModel, SearchViewModel>(objUI, null));
        }



        //-------------------------
        private List<string> getUniversityForAdv()
        {
            List<String> lstUni = new List<string>();
            using (connection)
            {
                string temp;
                //int tempInt;
                connection.Open();
                string sql = "select name || ':' || branch || ':' || address as newname,id from locationyear where location='Non-campus' and year='2014' order by newname asc";
                OracleCommand cmd = new OracleCommand(sql, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    temp = reader.GetString(0);
                    //tempInt = reader.GetInt32(1);
                    lstUni.Add(temp);
                  // uniId.Add(temp, tempInt );
                  //  System.Diagnostics.Debug.WriteLine(temp+" "+tempInt);

                }
                connection.Close();
            }
            return lstUni;
        }


        private List<string> getUniversityForTrends()
        {
            List<String> lstUni = new List<string>();
            using (connection)
            {
                string temp;
                //int tempInt;
                connection.Open();
                string sql = "select name || ':' || branch || ':' || city || ':' || state as newname,id from locationyear where location='Non-campus' and year='2014' order by newname asc";
                OracleCommand cmd = new OracleCommand(sql, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    temp = reader.GetString(0);
                    //tempInt = reader.GetInt32(1);
                    lstUni.Add(temp);
                    // uniId.Add(temp, tempInt );
                    //  System.Diagnostics.Debug.WriteLine(temp+" "+tempInt);

                }
                connection.Close();
            }
            return lstUni;
        }


        private List<string> getUniversity()
        {
            List<String> lstUni = new List<string>();
            using (connection)
            {
                connection.Open();
                string sql = "Select DISTINCT Name as University from LOCATIONYEAR order by NAME asc";
                OracleCommand cmd = new OracleCommand(sql, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lstUni.Add(reader.GetString(0));
                }
                connection.Close();
            }
            return lstUni;
        }

        public ActionResult loadGrid(String Uni, int Year, String Location, String Type)
        {
            SearchViewModel model = new SearchViewModel();
            model.PageSize = 25;
            int columns = 0;
            String sql = "Select ";
            
            
                sql = sql + "*";
            
           
            
               
            
            int flag = 0;
            Type = (Type == "Criminal Offense") ? "Criminal_Offense" : Type;

            if (Type == "Violence Against Women")
                Type = "VAWA";
            sql = sql + " from " + Type + " where id in (select id from locationyear where name = '" + Uni + "' AND year = " + Year + " AND location = '" + Location + "')";
            //var temp;
            //Console.WriteLine(sql);
            List<string> tempList = new List<string>();
            if (Type == "Discipline")
            {
                flag = 1;
                var temp = new System.Collections.Generic.List<Discipline>();
                tempList.Add("drug"); tempList.Add("weapon"); tempList.Add("liquor");
                model.generalList = temp.Cast<object>().ToList();

            }
            else if (Type == "Arrests")
            {
                flag = 2;
                tempList.Add("drug"); tempList.Add("weapon"); tempList.Add("liquor");
                var temp = new System.Collections.Generic.List<Arrests>();
                model.generalList = temp.Cast<object>().ToList();
            }
            else if (Type == "Criminal_Offense")
            {
                flag = 3;
                 tempList.Add("burgla"); tempList.Add("murd"); tempList.Add("vehic"); tempList.Add("neg_m"); tempList.Add("robbe"); tempList.Add("forcib"); tempList.Add("nonfor"); tempList.Add("agg_a"); tempList.Add("arson");
                var temp = new System.Collections.Generic.List<Criminal_Offense>();
                model.generalList = temp.Cast<object>().ToList();

            }

            else if (Type == ("VAWA"))
            {
                flag = 4;
                 tempList.Add("stalk"); tempList.Add("dating"); tempList.Add("domest");
                var temp = new System.Collections.Generic.List<VAWA>();
                model.generalList = temp.Cast<object>().ToList();
            }
            //new System.Collections.Generic.List<Discipline>();
            //model.Location.Add(new LocYear(2014, "UF", "FL", "Campus"));
            model.PageSize = 25;
            
            int class_type = 0;
            using (connection)
            {

                connection.Open();
                //string sql = "Select NAME, STATE, YEAR, LOCATION FROM LOCATIONYEAR WHERE STATE = 'FL'";
                OracleCommand cmd = new OracleCommand(sql, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                OracleDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    switch (flag)
                    {
                        case 1:
                            Discipline di = new Discipline(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3));
                            
                            class_type = 1;
                            columns = 4;
                            model.generalList.Add(di);
                            break;
                        case 2:
                            Arrests ar = new Arrests(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3));
                            
                            columns = 4;
                            class_type = 2;
                            model.generalList.Add(ar);
                            break;
                        case 3:
                            Criminal_Offense co = new Criminal_Offense(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9));
                            
                            columns = 10;
                            class_type = 3;
                            model.generalList.Add(co);
                            break;
                        case 4:
                            VAWA va = new VAWA(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3));
                            
                            columns = 4;
                            class_type = 4;
                            model.generalList.Add(va);
                            break;
                    }
                    //;

                }
                connection.Close(); 
            }
            ViewBag.NbColumns = columns;
            ViewBag.Tlist = tempList;
            ViewBag.Class_Type = class_type;
            return View("loadGrid",new Tuple<SearchViewModel, Discipline>(model, null));
            //return Json(model);
        }

        public JsonResult trendsPie(String Uni, String Type)
        {
            String[] temp = Uni.Split(':');
            TrendsModel TPmodel = new TrendsModel();
            TPmodel.answer2 = new List<TrendsPie>();
            String sql = "select * from pie_store";
            String sendType = Type;
            if (Type == "Criminal Offense")
            {
                sendType = "CRIMINAL_OFFENSE";

            }
            else if (Type == "Violence Against Women")
            {
                sendType = "VAWA";

            }
            else if (Type == "Arrests")
            {
                sendType = "ARRESTS";

            }

            else
            {

                sendType = "DISCIPLINE";
                
            }


            using (connection)
            {

                connection.Open();
                OracleCommand cmd0 = new OracleCommand("GENERATEPIE", connection);
                cmd0.CommandType = System.Data.CommandType.StoredProcedure;

                cmd0.Parameters.Add("UniversityFilter", OracleDbType.Varchar2).Value = temp[0];
                cmd0.Parameters.Add("BranchFilter", OracleDbType.Varchar2).Value = temp[1];
                cmd0.Parameters.Add("CityFilter", OracleDbType.Varchar2).Value = temp[2];
                cmd0.Parameters.Add("StateFilter", OracleDbType.Varchar2).Value = temp[3];
                cmd0.Parameters.Add("TypeFilter", OracleDbType.Varchar2).Value = sendType;
                cmd0.ExecuteNonQuery();
                OracleCommand cmd = new OracleCommand(sql, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    TrendsPie robj = new TrendsPie(reader.GetString(0), reader.GetInt32(1));
                    TPmodel.answer2.Add(robj);
                }

                connection.Close();
            }


            return Json(TPmodel.answer2, JsonRequestBehavior.AllowGet);

        }

        public JsonResult trends(String[] Uni, String Type, String SubType)
        {
            TrendsModel Tmodel = new TrendsModel();
            Tmodel.answer = new List<TrendsObject>();
            String sql = "select * from trends";
            String sendSubType = SubType;
            String sendType = Type;
            if (Type == "Criminal Offense")
            {
                sendType = "CRIMINAL_OFFENSE";
                
                if (SubType == "Burglary")
                    sendSubType = "BURGLA";
                else if (SubType == "Murder")
                    sendSubType = "MURD";
                else if (SubType == "Vehicle theft")
                    sendSubType = "VEHIC";
                else if (SubType == "Man slaughter")
                    sendSubType = "NEG_M";
                else if (SubType == "Robbery")
                    sendSubType = "ROBBE";
                else if (SubType == "Forcible Sex offence")
                    sendSubType = "FORCIB";
                else if (SubType == "non-forcible sex offence")
                    sendSubType = "NONFOR";
                else if (SubType == "Assault")
                    sendSubType = "AGG_A";
                else
                    sendSubType = "ARSON";

            }
            else if (Type == "Violence Against Women")
            {
                sendType = "VAWA";
                
                if (SubType == "Stalking")
                    sendSubType = "Stalk";
                else if (SubType == "Dating violence")
                    sendSubType = "Dating";
                else
                    sendSubType = "Domest";
            }
            else if (Type == "Arrests")
            {
                sendType = "ARRESTS";
                

                if (SubType == "Drug")
                    sendSubType = "DRUG";
                else if (SubType == "Weapon")
                    sendSubType = "Weapon";
                else
                    sendSubType = "Liquor";
            }

            else
            {
                
                sendType = "DISCIPLINE";
                if (SubType == "Drug")
                    sendSubType = "DRUG";
                else if (SubType == "Weapon")
                    sendSubType = "Weapon";
                else
                    sendSubType = "Liquor";
            }

            using (connection)
            {

                connection.Open();
                for (int itr1 = 0; itr1 < Uni.Length; itr1++)
                {
                    OracleCommand cmd0 = new OracleCommand("GENERATETREND", connection);
                    cmd0.CommandType = System.Data.CommandType.StoredProcedure;

                    //cmd0.Parameters.Add(new OracleParameter("TypeFilter", sendType));
                    //cmd0.Parameters.Add(new OracleParameter("FocusFilter", sendSubType));
                    //cmd0.Parameters.Add(new OracleParameter("yearFilter", Year));
                    String[] temp = Uni[itr1].Split(':');
                    cmd0.Parameters.Add("UniversityFilter", OracleDbType.Varchar2).Value = temp[0];
                    cmd0.Parameters.Add("BranchFilter", OracleDbType.Varchar2).Value = temp[1];
                    cmd0.Parameters.Add("CityFilter", OracleDbType.Varchar2).Value = temp[2];
                    cmd0.Parameters.Add("StateFilter", OracleDbType.Varchar2).Value = temp[3];
                    cmd0.Parameters.Add("TypeFilter", OracleDbType.Varchar2).Value = sendType;
                    cmd0.Parameters.Add("FocusFilter", OracleDbType.Varchar2).Value = sendSubType;
                    //OracleDataAdapter da = new OracleDataAdapter(cmd0);
                    cmd0.ExecuteNonQuery();
                    Console.WriteLine();
                }
                    //sql = "select * from test_table";
                    OracleCommand cmd = new OracleCommand(sql, connection);
                    cmd.CommandType = System.Data.CommandType.Text;
                    OracleDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        TrendsObject robj = new TrendsObject(reader.GetString(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(4));
                        Tmodel.answer.Add(robj);
                    }

                OracleCommand cmd2 = new OracleCommand("truncate table trends", connection);
                cmd2.CommandType = System.Data.CommandType.Text;
                cmd2.ExecuteNonQuery();
                connection.Close();
            }


            //ViewBag.NbColumns = columns;
            //ViewBag.Tlist = tempList;
            //ViewBag.Class_Type = class_type;
            return Json(Tmodel.answer, JsonRequestBehavior.AllowGet);
            
            //return View("loadTrends", new Tuple<TrendsModel, Discipline>(Tmodel, null));
        }

        public void doNothing()
        {
            //check
        }
        public ActionResult ranking(int Year,String Type, String SubType)
        {

            RankingsModel Rmodel = new RankingsModel();
            String sql;
            String sendSubType = SubType;
            String sendType = Type;
            if (Type == "Criminal Offense")
            {
                sendType = "CRIMINAL_OFFENSE";
                sql = "select * from criminal_offense_rank order by incidents desc";
                if (SubType == "Burglary")
                    sendSubType = "BURGLA";
                else if (SubType == "Murder")
                    sendSubType = "MURD";
                else if (SubType == "Vehicle theft")
                    sendSubType = "VEHIC";
                else if (SubType == "Man slaughter")
                    sendSubType = "NEG_M";
                else if (SubType == "Robbery")
                    sendSubType = "ROBBE";
                else if (SubType == "Forcible Sex offence")
                    sendSubType = "FORCIB";
                else if (SubType == "non-forcible sex offence")
                    sendSubType = "NONFOR";
                else if (SubType == "Assault")
                    sendSubType = "AGG_A";
                else
                    sendSubType = "ARSON";

            }
            else if(Type=="Violence Against Women")
            {
                sendType = "VAWA";
                sql = "select * from vawa_rank order by incidents desc";
                if (SubType == "Stalking")
                    sendSubType = "Stalk";
                else if (SubType == "Dating violence")
                    sendSubType = "Dating";
                else
                    sendSubType = "Domest";
            }
            else if (Type == "Arrests")
            {
                sendType = "ARRESTS";
                sql = "select * from arrests_rank order by incidents desc";
                
                if (SubType == "Drug")
                    sendSubType = "DRUG";
                else if (SubType == "Weapon")
                    sendSubType = "Weapon";
                else
                    sendSubType = "Liquor";
            }

            else
            {
                sql = "select * from discipline_rank order by incidents desc";
                sendType = "DISCIPLINE";
                if (SubType == "Drug")
                    sendSubType = "DRUG";
                else if (SubType == "Weapon")
                    sendSubType = "Weapon";
                else
                    sendSubType = "Liquor";
            }
                

            Rmodel.answer = new List<RankingsObject>();
            using (connection)
            {
                
                connection.Open();
                OracleCommand cmd0 = new OracleCommand("GENERATERANKTABLE", connection);
                cmd0.CommandType = System.Data.CommandType.StoredProcedure;

                //cmd0.Parameters.Add(new OracleParameter("TypeFilter", sendType));
                //cmd0.Parameters.Add(new OracleParameter("FocusFilter", sendSubType));
                //cmd0.Parameters.Add(new OracleParameter("yearFilter", Year));

                cmd0.Parameters.Add("TypeFilter", OracleDbType.Varchar2).Value = sendType;
                cmd0.Parameters.Add("FocusFilter", OracleDbType.Varchar2).Value = sendSubType;
                cmd0.Parameters.Add("yearFilter", OracleDbType.Int32).Value = Year;
                //OracleDataAdapter da = new OracleDataAdapter(cmd0);
                cmd0.ExecuteNonQuery();

                //sql = "select * from test_table";
                OracleCommand cmd = new OracleCommand(sql, connection);
                cmd.CommandType = System.Data.CommandType.Text;
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    RankingsObject robj = new RankingsObject(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4));
                    Rmodel.answer.Add(robj);
                }

                
                connection.Close();
            }
            return View("RankingloadGrid", new Tuple<RankingsModel, Discipline>(Rmodel, null));
        }




        public ActionResult loadGridAdvanced(String[] Uni, int[] Year, String Location, String[] Type)
        {
            //["UF", "USC"], [2012,2013,2014], "On-Campus", ["Arrests", "Discipline"]
                

            AdvSearchViewModel Amodel = new AdvSearchViewModel();
            Amodel.generalList = new List<string>();
            List<AdvSearchObject> tempList = new List<AdvSearchObject>();
            Amodel.answer = new List<List<AdvSearchObject>>();
            String[] temp;
            String sql;
            int flag = 0;
            Amodel.PageSize = 25;
            //int a;
            using (connection)
            {
                connection.Open();
                for (int itr0 = 0; itr0 < Type.Length; itr0++)
                {
                    if (Type[itr0].Equals("Arrests"))
                        flag = 1;
                    else if (Type[itr0].Equals("Criminal Offense"))
                        flag = 2;
                    else if (Type[itr0].Equals("Discipline"))
                        flag = 3;
                    else 
                        flag = 4;

                    tempList = new List<AdvSearchObject>();
                    for (int itr1 = 0; itr1 < Uni.Length; itr1++)
                    {
                        temp = Uni[itr1].Split(':');
                        
                        AdvSearchObject ObjOfListOfInt = new AdvSearchObject();
                        ObjOfListOfInt.rows = new List<int>();
                        for (int itr2 = 0; itr2 < Year.Length; itr2++)
                        {
                            if(flag ==1 || flag ==3)
                            sql = "select nvl(drug,0) + nvl(weapon,0) + nvl(liquor,0) as ItemSum from " + Type[itr0] + " where id in (select id from locationyear where name = '" + temp[0] + "' and branch = '"+ temp[1] +"' and address = '" +temp[2]+"' and year = "+Year[itr2]+" and location = '"+Location+"')";
                            else if(flag ==2)
                                sql = "select nvl(burgla,0) + nvl(murd,0) + nvl(vehic,0) + nvl(neg_m,0) + nvl(robbe,0) + nvl(forcib,0) + nvl(nonfor,0) + nvl(agg_a,0) + nvl(arson,0) as ItemSum from Criminal_Offense where id in (select id from locationyear where name = '" + temp[0] + "' and branch = '" + temp[1] + "' and address = '" + temp[2] + "' and year = " + Year[itr2] + " and location = '" + Location + "')";
                            else
                                sql = "select nvl(stalk,0) + nvl(dating,0) + nvl(domest,0) as ItemSum from VAWA where id in (select id from locationyear where name = '" + temp[0] + "' and branch = '" + temp[1] + "' and address = '" + temp[2] + "' and year = " + Year[itr2] + " and location = '" + Location + "')";

                            OracleCommand cmd = new OracleCommand(sql, connection);
                            cmd.CommandType = System.Data.CommandType.Text;
                            OracleDataReader reader = cmd.ExecuteReader();
                            while(reader.Read())
                            {
                                
                                ObjOfListOfInt.rows.Add(reader.GetInt32(0));
                            }
                            //select nvl(drug,0) + nvl(weapon,0) + nvl(liquor,0) as ItemSum from arrests where id in (select id from locationyear where name = temp[0] and branch = temp[1] and address = temp[2] and year = Year[itr2] and location = Location)
                        }
                        tempList.Add(ObjOfListOfInt);
                        //Amodel.generalList.Add(Uni[itr1]);
                    }
                    Amodel.answer.Add(tempList);
                }
                connection.Close();
            }
                     
            ViewBag.UniList = Uni;
            ViewBag.YearList = Year;
            ViewBag.TypeList = Type;
            return View("AdvloadGrid", new Tuple<AdvSearchViewModel, Discipline>(Amodel, null));
            
        }


        [HttpPost]
        public ActionResult Search(UIModel input)//string University)
        {
            SearchViewModel model = new SearchViewModel();
            //model.Location = new List<LocYear>();
            //model.Location.Add(new LocYear(2014, "abc", "dbs","dsh"));
            //model.Location.Add(new LocYear(2014, "UF", "FL", "Campus"));
            model.PageSize = 25;
            ViewBag.model = model;
            return View(model);
        }

        //public ActionResult Trends()
        //{
        //    return View();
        //}


    }
}
