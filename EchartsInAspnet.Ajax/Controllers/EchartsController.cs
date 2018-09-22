using EchartsInAspnet.Ajax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EchartsInAspnet.Ajax.Controllers
{
    public class EchartsController : Controller
    {
        // GET: Echarts
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetData()
        {
            DateTime startMonth = new DateTime(2018, 01, 01);
            DateTime endMonth = new DateTime(2018, 09, 22);
            //图表的category是字符串数组形式显示
            List<string> categoryList = new List<string>();//{"周一","周二", "周三", "周四", "周五","周六"};
                                                            //图表的series数据为一个对象数组 需定义一个series的类
            List<Series> seriesList = new List<Series>();

            //Echarts图表需要设置legend内的data数组为series的name集合这里需要定义一个legend数组
            List<string> legendList = new List<string>();
            //设置legend数组
            legendList.Add("每天订单数(单)"); //这里的名称必须和series类里面的name保持一致

            //定义一个Series对象
            Series seriesObj = new Series();
            seriesObj.name = "每天订单数(单)";
            seriesObj.type = "bar"; //柱状图显示,可以是其他
            seriesObj.data = new List<int>(); //初始化seriesObj.data 否则data.Add(x)添加时会报错

            using (PortalSearchDataContext db = new PortalSearchDataContext())
            {
                List<MonethOrdersNo> getMonthData = (from t in db.DOC_Order_Header
                                                        where
                                                        t.OrderTime >= startMonth.Date.Date && t.OrderTime <= DateTime.Parse(endMonth.Date.ToString("yyyy-MM-dd 23:59"))
                                                        group t by new
                                                        {
                                                            Day = t.OrderTime.Date
                                                        } into g
                                                        select new MonethOrdersNo
                                                        {
                                                            Day = g.Key.Day,
                                                            DayOrderNo = g.Count()
                                                        }).OrderBy(x => x.Day).ToList<MonethOrdersNo>();
                //设置数据
                for (int i = 0; i < getMonthData.Count(); i++)
                {
                    //加入category数组
                    categoryList.Add(getMonthData[i].Day.Value.ToString("yyyy-MM-dd"));
                    //为series序列数组中data添加数据
                    seriesObj.data.Add(getMonthData[i].DayOrderNo);
                }

            }
            //将sereis对象压入sereis数组列表内
            seriesList.Add(seriesObj);

            //结果显示需要category和series、legend多个对象 因此new一个新的对象来封装返回的多个对象
            var newObj = new
            {
                category = categoryList,
                series = seriesList,
                legend = legendList
            };

            return new JsonResult() { Data = newObj, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}