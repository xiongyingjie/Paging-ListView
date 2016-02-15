using Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using xyj.Plugs;
namespace Demo.Controllers
{
    public class HomeController : Controller, ICutPage, IModelNote
    {
        //
        // GET: /Home/
        DemoDbContext dc = new DemoDbContext();
        public ActionResult Index(int id=1)
        {
            return (id == 1 ? RedirectToAction("NoticeTypeShow") : RedirectToAction("NoticeShow"));
        }
        //调用常规函数
        public ActionResult NoticeTypeShow(int perCount = 8, int pageIndex = 1)
        {
            this.CutPage<
                /*数据源模型*/T_NoticeType>(
                /*数据源模型*/this.GetModelNote<T_NoticeType>()
                , this, perCount, pageIndex
                ,/*数据源*/dc.T_NoticeType.ToList(),
                /*要显示的列索引*/new int[] { 0, 1, 2 },
                /*页面标题*/"公告列表",
                /*列表视图操作栏*/new Dictionary<string, string>() { { "编辑", "NoticeTypeEdit" }, { "删除", " NoticeTypeDelete" } }
                );
            return View("Index");
        }

        //调用带连接查询的函数
        public ActionResult NoticeShow(int perCount = 8, int pageIndex = 1)
        {
            var data = dc.T_Notice.ToList();
            this.CutPage<
                /*数据源模型*/T_Notice
                ,/*视图数据源模型*/T_Notice_Linked>(
                /*视图数据源模型*/this.GetModelNote<T_Notice_Linked>()
                , this, perCount, pageIndex
                ,/*数据源*/data
                ,/*视图数据源*/data.Join(dc.T_NoticeType,
                      a => a.NoticeTypeID,
                      b => b.NoticeTypeID,
                      (a, b) => new T_Notice_Linked { NoticeID = a.NoticeID.ToString(), NoticeTypeID = a.NoticeID.ToString(), Contents = a.Contents, NOTE = a.NOTE, NoticeTypeName = b.NoticeTypeName, NoticeTypeNOTE = b.NOTE, SubmitTime = a.SubmitTime.ToString(), Title = a.Title, UserID = a.UserID }).ToList()
                ,/*要显示的视图列索引*/new int[] { 0, 1, 2, 3, 4, 5 }
                ,/*页面标题*/"公告列表"
                ,/*列表视图操作栏*/ new Dictionary<string, string>() { { "编辑", "NoticeEdit" }, { "删除", "NoticeDelete" } }
                );

            return View("Index");
        }

    }
}
