﻿using CampusSecurity.Models;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcMovie.Controllers
{
    public class HelloWorldController : Controller
    {
        // 
        // GET: /HelloWorld/ 
        static string connectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.cise.ufl.edu)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=orcl)));User Id = menon; Password = Zxqw29!!;";
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
        /*[HttpGet]
        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]*/
        public ActionResult Search()//UIModel objModel)//string University)
        {
            int columns = 0;
            UIModel objUI = new UIModel();
            //objModel.Location = new System.Collections.Generic.List<SelectListItem>(new SelectListItem("On-campus"));

            List<String> lstLoc = new List<String> { "On-campus"};
            List<String> lstType = new List<string> { "Discipline"};
            List<String> lstUni = new List<string> { "Tompkins Cortland Community College" };
            List<String> lstYear = new List<string> { "2012" };
            List<String> lstSub = new List<string> { "All" };
            objUI.Location = lstLoc.Select(c => new SelectListItem
            {
                Text = c.ToString(),
                Value = c.ToString()
            }).ToList();

            objUI.University = lstUni.Select(c => new SelectListItem
            {
                Text = c.ToString(),
                Value = c.ToString()
            }).ToList();

            objUI.Type = lstType.Select(c => new SelectListItem
            {
                Text = c.ToString(),
                Value = c.ToString()
            }).ToList();

            objUI.Year = lstYear.Select(c => new SelectListItem
            {
                Text = c.ToString(),
                Value = c.ToString()
            }).ToList();

            objUI.SubType = lstSub.Select(c => new SelectListItem
            {
                Text = c.ToString(),
                Value = c.ToString()
            }).ToList();

            SearchViewModel model = new SearchViewModel();

            String sql = "Select ";
            // * from objUI.Type[0].Value where id in (select id from locationyear where name = objUI.University[0].Value AND year = objUI.Year[0].Value AND location = objUI.Location[0].Value)
            // objUI.SubType[0].Value  from objUI.Type[0].Value where id in (select id from locationyear where name = objUI.University[0].Value AND year = objUI.Year[0].Value AND location = objUI.Location[0].Value)


            if (objUI.SubType[0].Value.Equals("All"))
            {
                sql = sql + "*";
            }
            else
            {
                sql = sql + objUI.SubType[0].Value;
            }
            int flag = 0;
            sql = sql + " from "+objUI.Type[0].Value+" where id in (select id from locationyear where name = '"+objUI.University[0].Value+"' AND year = "+objUI.Year[0].Value+" AND location = '"+ objUI.Location[0].Value+"')";
            //var temp;
            Console.WriteLine(sql);
            if (objUI.Type[0].Value == "Discipline")
            {
                flag = 1;
                   var temp = new System.Collections.Generic.List<Discipline>();
                    model.generalList = temp.Cast<object>().ToList();
                
            }
            else if(objUI.Type[0].Value == "Arrests")
            {
                flag = 2;
                 var temp = new System.Collections.Generic.List<Arrests>();
                model.generalList = temp.Cast<object>().ToList();
            }
            else if(objUI.Type[0].Value == "Criminal_Offense")
            {
                flag = 3;
                var temp = new System.Collections.Generic.List<Criminal_Offense>();
                model.generalList = temp.Cast<object>().ToList();

            }
           
            else if(objUI.Type[0].Value == ("VAWA"))
            {
                flag = 4;
                var temp = new System.Collections.Generic.List<VAWA>();
                model.generalList = temp.Cast<object>().ToList();
            }
            //new System.Collections.Generic.List<Discipline>();
            //model.Location.Add(new LocYear(2014, "UF", "FL", "Campus"));
            model.PageSize = 25;
            List<string> tempList = new List<string>();
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

                    switch(flag)
                    {
                        case 1:
                            Discipline di = new Discipline(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3));
                            tempList.Add("id"); tempList.Add("drug"); tempList.Add("weapon"); tempList.Add("liquor");
                            class_type = 1;
                            columns = 4;
                            model.generalList.Add(di);
                            break;
                        case 2:
                            Arrests ar = new Arrests(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3));
                            tempList.Add("id"); tempList.Add("drug"); tempList.Add("weapon"); tempList.Add("liquor");
                            columns = 4;
                            class_type = 2;
                            model.generalList.Add(ar);
                            break;
                        case 3:
                            Criminal_Offense co = new Criminal_Offense(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4), reader.GetInt32(5), reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9));
                            tempList.Add("id"); tempList.Add("burgla"); tempList.Add("murd"); tempList.Add("vehic"); tempList.Add("neg_m"); tempList.Add("robbe"); tempList.Add("forcib"); tempList.Add("nonfor"); tempList.Add("agg_a"); tempList.Add("arson"); 
                            columns = 10;
                            class_type = 3;
                            model.generalList.Add(co);
                            break;
                        case 4:
                            VAWA va = new VAWA(reader.GetInt32(0), reader.GetInt32(1), reader.GetInt32(2), reader.GetInt32(3));
                            tempList.Add("id"); tempList.Add("stalk"); tempList.Add("dating"); tempList.Add("domest");
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
            return View(new Tuple<SearchViewModel,Discipline>(model,null));
        }

    }
}
