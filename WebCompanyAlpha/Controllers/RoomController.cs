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
            List<RoomModel> roomModels = dataProvider.Room.GetRooms(0, RoomIsProjector.All, RoomIsMarkerBoard.All)
                .Select(x => new RoomModel
                {
                    ID = x.ID,
                    Name = x.Name,
                    SeatsCount = x.SeatsCount,
                    IsProjector = x.IsProjector,
                    IsMarkerBoard = x.IsMarkerBoard
                }).ToList();
            return View(roomModels);
        }
        
        // GET: Room/Create
        public ActionResult Insert()
        {
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
                IsBlock = false
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
                IsBlock = false
            };
            return View(roomModel);
        }

        // POST: Room/Delete/5
        [HttpPost]
        public ActionResult Delete(RoomModel modal)
        {
            try
            {
                List<OrderRoomInfo> orderRoomInfos = dataProvider.OrderRoom.GetPreDeleteRoomInfos(modal.ID);
                if (orderRoomInfos?.Count > 0)
                {
                    dataProvider.Room.Delete(modal.ID);
                    return RedirectToAction("Index");
                }
                else
                    return RedirectToAction("DeleteDetails", modal);
            }
            catch
            {
                return View(modal);
            }
        }

        // GET: Room/Delete/5
        public ActionResult DeleteDetails(RoomModel model)
        {
            return View(model);
        }

        // POST: Room/Delete/5
        [HttpPost]
        public ActionResult DeleteDetails(int id)
        {
            try
            {
                dataProvider.Room.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(id);
            }
        }
    }
}
