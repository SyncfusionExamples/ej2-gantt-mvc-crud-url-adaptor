using Syncfusion.EJ2.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web;
using System.Web.Mvc;
using GanttCrudSql.Models;

namespace GanttCrudSql.Controllers
{
    public class HomeController : Controller
    {
        GanttDataSourceEntities db = new GanttDataSourceEntities();     
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UrlDatasource(DataManagerRequest dm)
        {
          
            List<GanttData>DataList = db.GanttDatas.ToList();
            var count = DataList.Count();
            return Json(new { result = DataList, count = count });
        }

        public class ICRUDModel<T> where T : class
        {

            public object key { get; set; }

            public T value { get; set; }

            public List<T> added { get; set; }

            public List<T> changed { get; set; }

            public List<T> deleted { get; set; }

        }
        public ActionResult BatchSave([FromBody]ICRUDModel<GanttData> data)
        {

            List<GanttData> uAdded = new List<GanttData>();
            List<GanttData> uChanged = new List<GanttData>();
            List<GanttData> uDeleted = new List<GanttData>();

            //Performing insert operation
            if (data.added != null && data.added.Count() > 0)
            {
                foreach (var rec in data.added)
                {
                    uAdded.Add(this.Create(rec));
                }
            }

            //Performing update operation
            if (data.changed != null && data.changed.Count() > 0)
            {
                foreach (var rec in data.changed)
                {
                    uChanged.Add(this.Edit(rec));
                }
            }

            //Performing delete operation
            if (data.deleted != null && data.deleted.Count() > 0)
            {
                foreach (var rec in data.deleted)
                {
                    uDeleted.Add(this.Delete(rec.TaskId));
                }
            }
            return Json(new { addedRecords = uAdded, changedRecords = uChanged, deletedRecords = uDeleted });
        }
        public GanttData Create(GanttData value)
        {
            db.GanttDatas.Add(value);
            db.SaveChanges();
            return value;
        }

        public GanttData Edit(GanttData value)
        {
            GanttData result = db.GanttDatas.Where(currentData => currentData.TaskId == value.TaskId).FirstOrDefault();
            if (result != null)
            {
                result.TaskId = value.TaskId;
                result.TaskName = value.TaskName;
                result.StartDate = value.StartDate;
                result.EndDate = value.EndDate;
                result.Duration = value.Duration;
                result.Progress = value.Progress;
                result.Predecessor = value.Predecessor;
                db.SaveChanges();
                return result;
            }
            else
            {
                return null;
            }
        }


        public GanttData Delete(string value)
        {
            var result = db.GanttDatas.Where(currentData => currentData.TaskId == value).FirstOrDefault();
            db.GanttDatas.Remove(result);
            RemoveChildRecords(value);
            db.SaveChanges();
            return result;
        }
        public void RemoveChildRecords(string key)
        {
            var childList = db.GanttDatas.Where(x => x.ParentId == key).ToList();
            foreach (var item in childList)
            {
                db.GanttDatas.Remove(item);
                RemoveChildRecords(item.TaskId);
            }
        }
    }
}