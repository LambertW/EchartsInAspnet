using EchartsInAspnet.Ajax.Models;
using EchartsInAspnet.Ajax.Models.TuShare;
using NPoco;
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

        [HttpPost]
        public ActionResult GetData()
        {
            DateTime startMonth = DateTime.Now.AddDays(-5);
            DateTime endMonth = DateTime.Now;
            //图表的category是字符串数组形式显示
            List<string> categoryList = new List<string>();//{"周一","周二", "周三", "周四", "周五","周六"};
                                                           //图表的series数据为一个对象数组 需定义一个series的类
            List<Series> seriesList = new List<Series>();

            //Echarts图表需要设置legend内的data数组为series的name集合这里需要定义一个legend数组
            List<string> legendList = new List<string>();
            //设置legend数组
            legendList.Add("每天订单金额"); //这里的名称必须和series类里面的name保持一致

            //定义一个Series对象
            Series seriesObj = new Series();
            seriesObj.name = "每天订单金额";
            seriesObj.type = "line"; //柱状图显示,可以是其他
            seriesObj.data = new List<decimal>(); //初始化seriesObj.data 否则data.Add(x)添加时会报错

            PortalSearchDataContext.AddData();
            var getMonthData = (from t in PortalSearchDataContext.DOC_Order_Header.OrderByDescending(t => t.OrderTime).Take(500)
                                                 where
                                                 t.OrderTime >= startMonth.Date.Date && t.OrderTime <= DateTime.Parse(endMonth.Date.ToString("yyyy-MM-dd 23:59"))
                                                 select new
                                                 {
                                                     t.OrderTime,
                                                     t.TotalPrice
                                                 }).OrderBy(t => t.OrderTime).ToList();
            //设置数据
            for (int i = 0; i < getMonthData.Count(); i++)
            {
                //加入category数组
                categoryList.Add(getMonthData[i].OrderTime.ToString("yyyy-MM-dd"));
                //为series序列数组中data添加数据
                seriesObj.data.Add(getMonthData[i].TotalPrice);
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

            return new JsonResult() { Data = newObj };
        }

        [HttpPost]
        public ActionResult GetStockData(string code, DateTime begin, DateTime end)
        {
            var db = new Database("TuShare");
            var list = db.Query<Market>()
                .OrderBy(t => t.Date)
                .Where(t => t.Code == code)
                .Where(t => t.Date > begin)
                .Where(t => t.Date <= end)
                .ToList();

            //图表的category是字符串数组形式显示
            List<string> categoryList = new List<string>();

            //Echarts图表需要设置legend内的data数组为series的name集合这里需要定义一个legend数组
            List<string> legendList = new List<string>();
            //设置legend数组
            legendList.Add(code = code + "开盘价"); //这里的名称必须和series类里面的name保持一致
            legendList.Add(code = code + "成交量");

            //定义一个Series对象
            Series seriesObj = new Series
            {
                name = code + "开盘价",
                type = "line", //柱状图显示,可以是其他
                data = new List<decimal>() //初始化seriesObj.data 否则data.Add(x)添加时会报错
            };
            Series seriesObj1 = new Series
            {
                name = code + "成交量",
                type = "line", //柱状图显示,可以是其他
                data = new List<decimal>() //初始化seriesObj.data 否则data.Add(x)添加时会报错
            };

            //设置数据
            for (int i = 0; i < list.Count(); i++)
            {
                //加入category数组
                categoryList.Add(list[i].Date.ToString("yyyy-MM-dd HH:mm"));
                //为series序列数组中data添加数据
                seriesObj.data.Add(list[i].Open);
                seriesObj1.data.Add(list[i].Volume);
            }

            //将sereis对象压入sereis数组列表内
            List<Series> seriesList = new List<Series>();
            seriesList.Add(seriesObj);
            seriesList.Add(seriesObj1);

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