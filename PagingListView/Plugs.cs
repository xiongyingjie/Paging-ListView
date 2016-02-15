using xyj.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace xyj.Plugs
{

    #region 分页
    //分页接口
    public interface ICutPage
    {

    }
    //分页接口扩展方法
    public static class ECutPage
    {
       
        /// <summary>
        /// 分页功能
        /// </summary>
        /// <typeparam name="T">数据源类型</typeparam>
        /// <typeparam name="T_View">数据源视图类型</typeparam>
        /// <param name="plus">接口</param>
        /// <param name="obj">当前控制器=this </param>
        /// <param name="perCount">每页显示条数=perCount</param>
        /// <param name="pageIndex">当前页索引=pageIndex</param> 
        /// <param name="dataSrc">数据源</param>
        /// <param name="viewDataSrc">视图数据源</param>
        /// <param name="indexToShow">要显示的列索引</param>
        /// <param name="pageTitle">页面标题</param>
        /// <param name="headerTitle">表头数组</param>
        /// <param name="operateDictionary">操作列数据字典</param>
        /// <param name="actionDictionary">页面内超链数据字典</param>
        public static void CutPage<T, T_View>(this ICutPage plus,
            List<string> headerTitle,
            Controller obj,
            int perCount,
            int pageIndex,
            List<T> dataSrc,
            List<T_View> viewDataSrc,
            int[] indexToShow,
            string pageTitle,
            Dictionary<string, string> operateDictionary = null,
            Dictionary<string, string> actionDictionary = null
            ) where T_View : new()
        {
            //----------------分页相关开始  
            obj.ViewBag.link = obj.Request.Path;//当前页面url,用于构造分页
            obj.ViewBag.total = dataSrc.Count;
            obj.ViewBag.currentPage = pageIndex;
            obj.ViewBag.perCount = perCount;
            obj.ViewBag.linkParms = PageHelper.GetLinkParms<T>(dataSrc, perCount, pageIndex);//链接参数
            obj.ViewBag.dataList = PageHelper.GetPagedArray<T_View>(viewDataSrc, perCount, pageIndex, indexToShow);//要显示的数据


            obj.ViewData["Title"] = pageTitle;//页面标题  
            obj.ViewBag.headerTitle = PageHelper.FilterColums(headerTitle, indexToShow);// PageHelper.GetHeadTitle<T_Note>(indexToShow);//配置表头        
            obj.ViewBag.operate = (operateDictionary == null ? new Dictionary<string, string>() : operateDictionary);//配置操作
            obj.ViewBag.actionDictionary = (actionDictionary == null ? new Dictionary<string, string>() : actionDictionary);//配置页面链接
            //----------------页面配置结束
        }

        /// <summary>
        /// 分页功能
        /// </summary>
        /// <typeparam name="T">数据源类型</typeparam>
        /// <param name="plus"></param>
        /// <param name="pageTitle">页面标题</param>
        /// <param name="obj">当前控制器=this </param>
        /// <param name="perCount">每页显示条数=perCount</param>
        /// <param name="pageIndex">当前页索引=pageIndex</param> 
        /// <param name="dataSrc">数据源</param>
        /// <param name="indexToShow">要显示的列索引</param>
        /// <param name="headerTitle">表头数组</param>
        /// <param name="operateDictionary">操作列数据字典</param>
        /// <param name="actionDictionary">页面内超链数据字典</param>
        public static void CutPage<T>(this ICutPage plus,
           List<string> headerTitle,
           Controller obj,
           int perCount,
           int pageIndex,
           List<T> dataSrc,
           int[] indexToShow,
           string pageTitle,
           Dictionary<string, string> operateDictionary = null,
           Dictionary<string, string> actionDictionary = null
           )
        {
            //----------------分页相关开始  
            obj.ViewBag.link = obj.Request.Path;//当前页面url,用于构造分页
            obj.ViewBag.total = dataSrc.Count;
            obj.ViewBag.currentPage = pageIndex;
            obj.ViewBag.perCount = perCount;
            obj.ViewBag.linkParms = PageHelper.GetLinkParms<T>(dataSrc, perCount, pageIndex);//链接参数
            obj.ViewBag.dataList = PageHelper.GetPagedArray<T>(dataSrc, perCount, pageIndex, indexToShow);//要显示的数据


            obj.ViewData["Title"] = pageTitle;//页面标题  
            obj.ViewBag.headerTitle = PageHelper.FilterColums(headerTitle, indexToShow);// PageHelper.GetHeadTitle<T_Note>(indexToShow);//配置表头        
            obj.ViewBag.operate = (operateDictionary == null ? new Dictionary<string, string>() : operateDictionary);//配置操作
            obj.ViewBag.actionDictionary = (actionDictionary == null ? new Dictionary<string, string>() : actionDictionary);//配置页面链接
            //----------------页面配置结束
        }
    }

    #endregion

    #region 获取模型注释
    //注释自定义属性
    [AttributeUsage(AttributeTargets.Property)]
    public class ModelNoteAttrabute : Attribute
    {
        public string note;
        public ModelNoteAttrabute(string note)
        {
            this.note = note;
        }
    }
    //注释接口
    public interface IModelNote
    {

    }
    //注释接口扩展方法
    public static class EModelNote
    {
        public static List<string> GetModelNote<T>(this IModelNote plus) where T:new()
        {
            
          
             var array=new List<string >();
             //属性集合
             var attrList = typeof(T).GetProperties();
             //遍历
             foreach (var item in attrList)
             {
                 
                 var attr = (item.GetCustomAttributes(typeof(ModelNoteAttrabute), false) as ModelNoteAttrabute[]);
                 if (attr != null && attr.Length>0)
                 {
                     array.Add(attr[0].note);
                 }
                else
                {
                    array.Add("获取注释失败");
                }
             }
             return array;
        }
    }
     #endregion


    #region 表单提交接口
    //表单提交接口
    public interface IFormInfo
    {

    }
    //表单提交接口扩展方法
    public static class EPostTo
    {
        public static void SetForm(this IFormInfo plus, Controller obj, string title = null, string actionUrl = null)
        {
            obj.ViewBag.action = (actionUrl == null ? obj.Request.Path : actionUrl);
            obj.ViewData["Title"]=(title == null?"无标题":title);         
        }
    }
     #endregion
}