using CompanyAlpha.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompanyAlpha.DataInfo;
using WebCompanyAlpha.Filters;
using WebCompanyAlpha.Models;

namespace WebCompanyAlpha.Controllers
{
    [AdminAccess]
    public class RoomController : BaseController
    {
        /// <summary>
        /// Работа с переговрными
        /// </summary>
        /// <param name="dataProvider">Работа с данными</param>
        public RoomController(IDataProvider dataProvider) : base(dataProvider)
        {
            this.dataProvider = dataProvider;
        }

        // GET: Room
        public ActionResult Index()
        {
            ArreyOfModel arreyOfModel = new ArreyOfModel();
            List<RoomModel> roomModels = dataProvider.Room.GetRooms(0, RoomIsProjector.All, RoomIsMarkerBoard.All)
                .Select(x => new RoomModel
                {
                    ID = x.ID,
                    Name = x.Name,
                    SeatsCount = x.SeatsCount,
                    IsProjector = x.IsProjector,
                    IsMarkerBoard = x.IsMarkerBoard
                }).ToList();
            arreyOfModel.Title = "Переговорные";
            arreyOfModel.RoomModels = roomModels;
            return View(arreyOfModel);
        }
        
        // GET: Room/Create
        public ActionResult Insert()
        {
            RoomModel role = new RoomModel
            {
                Title = "Новая переговорная"
            };
            return View();
        }

        // POST: Room/Create
        [HttpPost]
        public ActionResult Insert(RoomModel model)
        {
            RoomInfo room = new RoomInfo
            {
                Name = model.Name,
                SeatsCount = model.SeatsCount,
                IsMarkerBoard = model.IsMarkerBoard,
                IsProjector = model.IsProjector,
                IsBlock = false
            };
            dataProvider.Room.Insert(room);
            return RedirectToAction("Index");
        }

        // GET: Room/Edit/5
        public ActionResult Edit(int id)
        {
            RoomInfo roomInfo = dataProvider.Room.GetRoom(id);
            if (roomInfo == null)
                return View();
            RoomModel roomModel = new RoomModel
            {
                ID = roomInfo.ID,
                Name = roomInfo.Name,
                SeatsCount = roomInfo.SeatsCount,
                IsMarkerBoard = roomInfo.IsMarkerBoard,
                IsProjector = roomInfo.IsProjector,
                IsBlock = false,
                Title = "Редактирование переговорной"
            };
            return View(roomModel);
        }

        // POST: Room/Edit/5
        [HttpPost]
        public ActionResult Edit(RoomModel model)
        {
            try
            {
                RoomInfo room = new RoomInfo
                {
                    ID = model.ID,
                    Name = model.Name,
                    SeatsCount = model.SeatsCount,
                    IsMarkerBoard = model.IsMarkerBoard,
                    IsProjector = model.IsProjector,
                    IsBlock = false
                };
                dataProvider.Room.Edit(room);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Room/Delete/5
        public ActionResult Delete(int id)
        {
            RoomInfo roomInfo = dataProvider.Room.GetRoom(id);
            if (roomInfo == null)
                return View();
            RoomModel roomModel = new RoomModel
            {
                ID = roomInfo.ID,
                Name = roomInfo.Name,
                SeatsCount = roomInfo.SeatsCount,
                IsMarkerBoard = roomInfo.IsMarkerBoard,
                IsProjector = roomInfo.IsProjector,
                IsBlock = false,
                Title = "Удаление переговорной"
            };
            roomModel.OrderRooms = dataProvider.OrderRoom.GetPreDeleteRoomInfos(roomModel.ID).Select(x => new OrderRoomModel
            {
                ID = x.ID,
                Start = x.Start,
                End = x.Start,
                MainDate = x.MainDate,
                Status = x.Status,
                UserCur = x.UserCur != null ? x.UserCur.ToString() : string.Empty
            }).ToList();
            roomModel.Title = "Удаление переговорной";
            if (roomModel.OrderRooms?.Count > 0)
            {
                return RedirectToAction("DeleteDetails", roomModel);
            }
            else
            {
                return View(roomModel);
            }
        }

        // POST: Room/Delete/5
        [HttpPost]
        public ActionResult Delete(RoomModel modal)
        {
            try
            {
                dataProvider.Room.Delete(modal.ID);
                return RedirectToAction("Index");
            }
            catch
            {
                modal.Title = "Удаление переговорной";
                return View(modal);
            }
        }

        // GET: Room/Delete/5
        public ActionResult DeleteDetails(RoomModel model)
        {
            model.Title = "Удаление переговорной";
            return View(model);
        }

        // POST: Room/Delete/5
        [HttpPost]
        public ActionResult DeleteDetails(RoomModel model, int id)
        {
            try
            {
                dataProvider.Room.Delete(model.ID);
                return RedirectToAction("Index");
            }
            catch
            {
                model.Title = "Удаление переговорной";
                return View(model);
            }
        }
    }
}
