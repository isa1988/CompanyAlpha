using CompanyAlpha.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompanyAlpha.DataInfo;
using WebCompanyAlpha.Filters;
using WebCompanyAlpha.Helper;
using WebCompanyAlpha.Models;

namespace WebCompanyAlpha.Controllers
{
    public class OrderRoomController : BaseController
    {
        /// <summary>
        /// Заказы комнат
        /// </summary>
        /// <param name="dataProvider">Работа с данными</param>
        public OrderRoomController(IDataProvider dataProvider) : base(dataProvider)
        {
            this.dataProvider = dataProvider;
        }
        public JsonResult GetOrderRoomJSonList()
        {
            List<OrderRoomJSonModel> orderRoomJSon = dataProvider.OrderRoom.GetOrderRoomInfos().Select(x => new OrderRoomJSonModel
            {
                ID = x.ID,
                StartDate = x.Start,
                EndDate = x.End,
                RoomID = x.RoomID,
                Status = x.Status
            }).ToList();

            if (orderRoomJSon == null || orderRoomJSon.Count == 0)
                return Json(orderRoomJSon, JsonRequestBehavior.AllowGet);

            List<RoomInfo> roomInfos = dataProvider.Room.GetRooms();/*GetRoomsOfFilters(0, RoomIsProjector.All,
                RoomIsMarkerBoard.All,
                new DateTime(2019, 1, 1), new DateTime(2019, 12, 31), OrderRoomStatusFilter.All);*/

            for (int i = 0; i < orderRoomJSon.Count; i++)
            {
                orderRoomJSon[i].Start = orderRoomJSon[i].StartDate.ToString("yyyy.MM.dd HH:mm:ss");
                orderRoomJSon[i].End = orderRoomJSon[i].EndDate.ToString("yyyy.MM.dd HH:mm:ss");
                orderRoomJSon[i].RoomCur = roomInfos.FirstOrDefault(x => x.ID == orderRoomJSon[i].RoomID).ToString();
                orderRoomJSon[i].Name = orderRoomJSon[i].RoomCur + " занята с " +
                                        orderRoomJSon[i].StartDate.ToString("HH:ss") +
                                        " по " + orderRoomJSon[i].EndDate.ToString("HH:ss");
                orderRoomJSon[i].SetColor();
            }

            return Json(orderRoomJSon, JsonRequestBehavior.AllowGet);
        }

        // GET: OrderRoom
        [UserPublicAccess]
        public ActionResult Index()
        {
            Page page = new Page();
            if (Cookies.IsChangeRoom && Cookies.IsEditUser)
                page.Layout = "Admin";
            else if (!Cookies.IsChangeRoom && !Cookies.IsEditUser)
                page.Layout = "User";
            return View(page);
        }

        // GET: OrderRoom/Details/5
        public ActionResult Details(int id)
        {
            OrderRoomInfo orderRoomInfo = dataProvider.OrderRoom.GetOrderRoom(id);
            if (orderRoomInfo == null) RedirectToAction("Index");
            RoomInfo roomInfo = dataProvider.Room.GetRoom(orderRoomInfo.RoomID);
            UserInfo userInfo = dataProvider.User.GetUser(Cookies.Login);
            OrderRoomModel model = new OrderRoomModel
            {
                ID = orderRoomInfo.ID,
                Status = orderRoomInfo.Status,
                StatusName = orderRoomInfo.GetStatusName(),
                MainDate = orderRoomInfo.MainDate,
                StartDT = orderRoomInfo.Start,
                EndDT = orderRoomInfo.End,
                UserID = orderRoomInfo.UserID
            };
            model.MainUserID = userInfo.ID;
            model.RoomCur = roomInfo.ToString();
            model.Title = "Детали брони";
            model.IsChangeRoom = Cookies.IsChangeRoom;
            model.IsEditUser = Cookies.IsEditUser;
            if (model.IsChangeRoom && model.IsEditUser)
                model.Layout = "Admin";
            else if (!model.IsChangeRoom && !model.IsEditUser)
                model.Layout = "User";
            return View(model);
        }

        // GET: OrderRoom/Create
        [UserPrivateAccess]
        public ActionResult Insert(string start)
        {
            start = start.Substring(4, 20);
            DateTime dt1 = DateTime.Parse(start);
            OrderRoomModel model = new OrderRoomModel
            {
                Title = "Регистрация",
                MainDate = dt1,
                StartDT = dt1.AddMinutes(1),
                EndDT = dt1.AddMinutes(30),
                Status = 0,
            };
            model.Rooms = dataProvider.Room.GetRooms().Select(x =>
                new SelectListItem { Text = x.ToString(), Value = x.ID.ToString() }).ToList();
            return View(model);
        }

        // POST: OrderRoom/Create
        [UserPrivateAccess]
        [HttpPost]
        public ActionResult Insert(OrderRoomModel model)
        {
            try
            {
                RoomInfo room = dataProvider.Room.GetRoom(model.RoomID);
                UserInfo userInfo = dataProvider.User.GetUser(Cookies.Login);

                OrderRoomInfo orderRoomInfo = new OrderRoomInfo
                {
                    MainDate = model.MainDate,
                    Start = model.StartDT,
                    End = model.EndDT,
                    RoomCur = room,
                    UserCur = userInfo
                };
                dataProvider.OrderRoom.Insert(orderRoomInfo);
                return RedirectToAction("Index");
            }
            catch
            {
                model.Rooms = dataProvider.Room.GetRooms().Select(x =>
                    new SelectListItem { Text = x.ToString(), Value = x.ID.ToString() }).ToList();
                return View(model);
            }
        }

        // GET: OrderRoom/Edit/5
        [UserPrivateAccess]
        public ActionResult Edit(int id)
        {
            OrderRoomInfo orderRoomInfo = dataProvider.OrderRoom.GetOrderRoom(id);
            if (orderRoomInfo == null || (int)orderRoomInfo.Status != 0) RedirectToAction("Index");
            OrderRoomModel model = new OrderRoomModel
            {
                ID = orderRoomInfo.ID,
                Status = orderRoomInfo.Status,
                StatusName = orderRoomInfo.GetStatusName(),
                MainDate = orderRoomInfo.MainDate,
                StartDT = orderRoomInfo.Start,
                EndDT = orderRoomInfo.End,
                UserID = orderRoomInfo.UserID,
                RoomID = orderRoomInfo.RoomID
            };
            model.Rooms = dataProvider.Room.GetRooms().Select(x =>
                new SelectListItem { Text = x.ToString(), Value = x.ID.ToString() }).ToList();
            return View(model);
        }

        // POST: OrderRoom/Edit/5
        [UserPrivateAccess]
        [HttpPost]
        public ActionResult Edit(OrderRoomModel model)
        {
            try
            {
                RoomInfo room = dataProvider.Room.GetRoom(model.RoomID);

                OrderRoomInfo orderRoomInfo = new OrderRoomInfo
                {
                    MainDate = model.MainDate,
                    Start = model.StartDT,
                    End = model.EndDT,
                    RoomCur = room,
                };
                dataProvider.OrderRoom.Edit(orderRoomInfo);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: OrderRoom/Delete/5
        [UserPublicAccess]
        public ActionResult Delete(int id)
        {
            OrderRoomInfo orderRoomInfo = dataProvider.OrderRoom.GetOrderRoom(id);
            if (orderRoomInfo == null) RedirectToAction("Index");
            RoomInfo roomInfo = dataProvider.Room.GetRoom(orderRoomInfo.RoomID);
            OrderRoomModel model = new OrderRoomModel
            {
                ID = orderRoomInfo.ID,
                Status = orderRoomInfo.Status,
                StatusName = orderRoomInfo.GetStatusName(),
                MainDate = orderRoomInfo.MainDate,
                StartDT = orderRoomInfo.Start,
                EndDT = orderRoomInfo.End,
                UserID = orderRoomInfo.UserID
            };
            model.RoomCur = roomInfo.ToString();
            model.Title = "Удаление";
            if (model.IsChangeRoom && model.IsEditUser)
                model.Layout = "Admin";
            else if (!model.IsChangeRoom && !model.IsEditUser)
                model.Layout = "User";
            return View(model);
        }

        // POST: OrderRoom/Delete/5
        [UserPublicAccess]
        [HttpPost]
        public ActionResult Delete(OrderRoomModel model)
        {
            try
            {

                dataProvider.OrderRoom.Delete(model.ID);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        /// <summary>
        /// Подтвердить бронь
        /// </summary>
        /// <param name="id">Идентификатор</param>
        [AdminAccess]
        public ActionResult ReservationApproved(int id)
        {
            OrderRoomInfo orderRoomInfo = dataProvider.OrderRoom.GetOrderRoom(id);
            if (orderRoomInfo == null || (int)orderRoomInfo.Status != 0) RedirectToAction("Index");
            RoomInfo roomInfo = dataProvider.Room.GetRoom(orderRoomInfo.RoomID);
            OrderRoomModel model = new OrderRoomModel
            {
                ID = orderRoomInfo.ID,
                Status = orderRoomInfo.Status,
                StatusName = orderRoomInfo.GetStatusName(),
                MainDate = orderRoomInfo.MainDate,
                StartDT = orderRoomInfo.Start,
                EndDT = orderRoomInfo.End,
                UserID = orderRoomInfo.UserID,
                RoomID = orderRoomInfo.RoomID
            };
            model.RoomCur = roomInfo.ToString();
            model.ReservationApprovedList = dataProvider.OrderRoom.ChekReservationApproved(id).Select(x =>
                new OrderRoomModel
                {
                    ID = x.ID,
                    Status = x.Status,
                    StatusName = x.GetStatusName(),
                    MainDate = x.MainDate,
                    StartDT = x.Start,
                    EndDT = x.End,
                    UserID = x.UserID,
                    RoomID = x.RoomID
                }).ToList();
            if (model.ReservationApprovedList != null && model.ReservationApprovedList.Count > 0)
            {
                List<RoomInfo> roomInfos = dataProvider.Room.GetRooms(0, RoomIsProjector.All, RoomIsMarkerBoard.All);
                for (int i = 0; i < model.ReservationApprovedList.Count; i++)
                {
                    model.ReservationApprovedList[i].RoomCur = roomInfos.FirstOrDefault(x => x.ID ==
                                     model.ReservationApprovedList[i].RoomID).ToString();
                }

                return View("ReservationApprovedList", model);
            }
            return View(model);
        }

        [AdminAccess]
        [HttpPost]
        public ActionResult ReservationApproved(OrderRoomModel model)
        {
            try
            {
                dataProvider.OrderRoom.ReservationApproved(model.ID);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        [AdminAccess]
        public ActionResult ReservationApprovedList(OrderRoomModel model, int id)
        {
            return View(model);
        }

        [AdminAccess]
        [HttpPost]
        public ActionResult ReservationApprovedList(OrderRoomModel model)
        {
            try
            {
                dataProvider.OrderRoom.ReservationApproved(model.ID);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }
    }
}
