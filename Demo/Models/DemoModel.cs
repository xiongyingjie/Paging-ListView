using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using xyj.Plugs;//必须引用
namespace Demo.Models
{
   
        public partial class T_Notice
        {
            [Key]
            public int NoticeID { get; set; }
            public string Title { get; set; }
            public string Contents { get; set; }
            public System.DateTime SubmitTime { get; set; }
            public string UserID { get; set; }
            public int NoticeTypeID { get; set; }
            public string NOTE { get; set; }
        }
  
        public partial class T_NoticeType
        {
             [Key]
            [ModelNoteAttrabute("公告类型编号")]
            public int NoticeTypeID { get; set; }
            [ModelNoteAttrabute("公告类型名称")]
            public string NoticeTypeName { get; set; }
            [ModelNoteAttrabute("公告类型备注")]
            public string NOTE { get; set; }
        }

        //视图模型
        public partial class T_Notice_Linked
        {
            [Key]
            [ModelNoteAttrabute("公告编号")]
            public string NoticeID { get; set; }
            [ModelNoteAttrabute("公告标题")]
            public string Title { get; set; }
            [ModelNoteAttrabute("公告内容")]
            public string Contents { get; set; }
            [ModelNoteAttrabute("发布时间")]
            public string SubmitTime { get; set; }
            [ModelNoteAttrabute("发布者")]
            public string UserID { get; set; }

            [ModelNoteAttrabute("公告备注")]
            public string NOTE { get; set; }
            [ModelNoteAttrabute("公告类型编号")]
            public string NoticeTypeID { get; set; }
            [ModelNoteAttrabute("公告类型名称")]
            public string NoticeTypeName { get; set; }
            [ModelNoteAttrabute("公告类型备注")]
            public string NoticeTypeNOTE { get; set; }
        }
    
}