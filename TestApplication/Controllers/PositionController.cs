using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestApplication.Context;
using TestApplication.Models;

namespace TestApplication.Controllers
{
    public class PositionController : Controller
    {
        private readonly IPositionRepo position_repo_;
        public PositionController(IPositionRepo position_repo)
        {
            position_repo_ = position_repo;
        }
        // GET: Position
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetPositions()
        {
            var result = position_repo_.GetPositions();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPositionById(int position_id)
        {
            return Json(position_repo_.GetPosition(position_id), JsonRequestBehavior.AllowGet);
        }
        public JsonResult CreatePosition(PositionDTO position)
        {
            return Json(position_repo_.CreatePosition(position), JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdatePosition(PositionDTO position)
        {
            return Json(position_repo_.UpdatePosition(position), JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeletePosition(int position_id)
        {
            return Json(position_repo_.DeletePosition(position_id), JsonRequestBehavior.AllowGet);
        }
    }
}