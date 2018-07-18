using MyProject.Core.Dtos;
using MyProject.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyProject.Services.Extensions;
using Deepleo.Weixin.SDK;
using Newtonsoft.Json;

namespace MyProject.Matrix.Controllers.WeiXinMenu
{
    public class WeiXinMenuController : BaseController
    {
        private readonly WeiXinMenuTask _task = new WeiXinMenuTask();
        private readonly WeiXinReplyMessageTask _replay = new WeiXinReplyMessageTask();

        public ActionResult Index()
        {
            var name_1 = _task.GetById(1);
            ViewBag.name_1 = name_1 == null ? "菜单1" : name_1.name;
            var name_2 = _task.GetById(2);
            ViewBag.name_2 = name_2 == null ? "菜单2" : name_2.name;
            var name_3 = _task.GetById(3);
            ViewBag.name_3 = name_3 == null ? "菜单3" : name_3.name;
            var name_11 = _task.GetById(11);
            ViewBag.name_11 = name_11 == null ? "菜单1-1" : name_11.name;
            var name_12 = _task.GetById(12);
            ViewBag.name_12 = name_12 == null ? "菜单1-2" : name_12.name;
            var name_13 = _task.GetById(13);
            ViewBag.name_13 = name_13 == null ? "菜单1-3" : name_13.name;
            var name_14 = _task.GetById(14);
            ViewBag.name_14 = name_14 == null ? "菜单1-4" : name_14.name;
            var name_15 = _task.GetById(15);
            ViewBag.name_15 = name_15 == null ? "菜单1-5" : name_15.name;
            var name_21 = _task.GetById(21);
            ViewBag.name_21 = name_21 == null ? "菜单2-1" : name_21.name;
            var name_22 = _task.GetById(22);
            ViewBag.name_22 = name_22 == null ? "菜单2-2" : name_22.name;
            var name_23 = _task.GetById(23);
            ViewBag.name_23 = name_23 == null ? "菜单2-3" : name_23.name;
            var name_24 = _task.GetById(24);
            ViewBag.name_24 = name_24 == null ? "菜单2-4" : name_24.name;
            var name_25 = _task.GetById(25);
            ViewBag.name_25 = name_25 == null ? "菜单2-5" : name_25.name;
            var name_31 = _task.GetById(31);
            ViewBag.name_31 = name_31 == null ? "菜单3-1" : name_31.name;
            var name_32 = _task.GetById(32);
            ViewBag.name_32 = name_32 == null ? "菜单3-2" : name_32.name;
            var name_33 = _task.GetById(33);
            ViewBag.name_33 = name_33 == null ? "菜单3-3" : name_33.name;
            var name_34 = _task.GetById(34);
            ViewBag.name_34 = name_34 == null ? "菜单3-4" : name_34.name;
            var name_35 = _task.GetById(35);
            ViewBag.name_35 = name_35 == null ? "菜单3-5" : name_35.name;
            ViewBag.madiaList = _replay.GetByReplayType().ToSelectList(c => c.MatchKey, c => c.MatchKey);
            return View();
        }

        [HttpPost]
        public ActionResult GetByMenuId(int menuId)
        {
            return Json(_task.GetById(menuId));
        }

        public ActionResult Save(string appid, string key, string media_id, int menuid, string name, string pagepath, string type, string url)
        {
            var menuinfo = _task.GetById(menuid);
            if (menuinfo == null)
            {
                menuinfo = new MyProject.Core.Entities.WeiXinMenu()
                {
                    creater = GetCurrentAdmin(),
                    createtime = DateTime.Now,
                    appid = appid,
                    key = key,
                    media_id = media_id,
                    menuid = menuid,
                    name = name,
                    pagepath = pagepath,
                    type = type,
                    url = url
                };
                _task.Add(menuinfo);
                return Json(new RequestResultDto() { Ret = 0, Msg = "添加成功" });
            }
            else
            {
                menuinfo.creater = GetCurrentAdmin();
                menuinfo.createtime = DateTime.Now;
                menuinfo.appid = appid;
                menuinfo.key = key;
                menuinfo.media_id = media_id;
                menuinfo.name = name;
                menuinfo.pagepath = pagepath;
                menuinfo.type = type;
                menuinfo.url = url;
                _task.Update(menuinfo);
                return Json(new RequestResultDto() { Ret = 0, Msg = "修改成功" });
            }

        }

        public ActionResult Delete(int menuId)
        {
            var list = _task.GetList();
            var list1 = list.Where(c => c.menuid >= 11 && c.menuid <= 15).OrderByDescending(c => c.menuid).ToList();
            var list2 = list.Where(c => c.menuid >= 21 && c.menuid <= 25).OrderByDescending(c => c.menuid).ToList();
            var list3 = list.Where(c => c.menuid >= 31 && c.menuid <= 35).OrderByDescending(c => c.menuid).ToList();
            var list4 = list.Where(c => c.menuid == 1 || c.menuid == 2 || c.menuid == 3).OrderByDescending(c => c.menuid).ToList();
            var menuid1 = 1;
            var menuid2 = 2;
            var menuid3 = 3;
            var menuid4 = 1;
            if (list1.Count > 0)
            {
                menuid1 = list1[0].menuid;
            }
            if (list2.Count > 0)
            {
                menuid2 = list2[0].menuid;
            }
            if (list3.Count > 0)
            {
                menuid3 = list3[0].menuid;
            }
            if (list4.Count > 0)
            {
                menuid4 = list4[0].menuid;
            }
            if (menuId != menuid1 || menuId != menuid1 || menuId != menuid1 || menuId != menuid1)
            {
                return Json(new RequestResultDto() { Msg = "不能删除", Ret = -1 });
            }
            _task.Delete(menuId);
            return Json(new RequestResultDto() { Msg = "删除成功", Ret = 0 });
        }

        //发布
        public ActionResult CreateMenu()
        {
            var menuList = _task.GetList();
            var menuModel = new MenuModel() { button=new List<object>()};
            foreach (var itemP in menuList.Where(c => c.menuid <= 3))
            {
                var childList = menuList.Where(c => c.menuid >= itemP.menuid * 10 && c.menuid < itemP.menuid * 10 + 10).ToList().OrderByDescending(c=>c.menuid);
                if (childList.Count() > 0)
                {
                    var menuChildModel = new MenuChildModel() { name = itemP.name,sub_button=new List<object>() };
                    foreach (var itemC in childList)
                    {
                        #region 代码
                        switch (itemC.type)
                        {
                            case "click":
                            case "location_select":
                                var clickModel = new clickModel()
                                {
                                    type = itemC.type,
                                    key = itemC.key,
                                    name = itemC.name
                                };
                                menuChildModel.sub_button.Add(clickModel);
                                break;
                            case "view":
                                var viewModel = new viewModel()
                                {
                                    type = itemC.type,
                                    url = itemC.url,
                                    name = itemC.name
                                };
                                menuChildModel.sub_button.Add(viewModel);
                                break;
                            case "miniprogram":
                                var miniprogramModel = new miniprogramModel()
                                {
                                    type = itemC.type,
                                    url = itemC.url,
                                    appid = itemC.appid,
                                    pagepath = itemC.pagepath,
                                    name = itemC.name
                                };
                                menuChildModel.sub_button.Add(miniprogramModel);
                                break;
                            case "scancode_waitmsg":
                            case "scancode_push":
                            case "pic_sysphoto":
                            case "pic_photo_or_album":
                            case "pic_weixin":
                                var subbuttonModel = new subbuttonModel()
                                {
                                    type = itemC.type,
                                    key = itemC.key,
                                    name = itemC.name,
                                    sub_button = new List<object>(),
                                };
                                menuChildModel.sub_button.Add(subbuttonModel);
                                break;
                            case "media_id":
                            case "view_limited":
                                var mediaidModel = new mediaidModel()
                                {
                                    type = itemC.type,
                                    media_id = itemC.media_id,
                                    name = itemC.name,
                                };
                                menuChildModel.sub_button.Add(mediaidModel);
                                break;
                        } 
                        #endregion 
                    }
                    menuModel.button.Add(menuChildModel);
                }
                else
                {
                    #region 代码
                    switch (itemP.type)
                    {
                        case "click":
                        case "location_select":
                            var clickModel = new clickModel()
                            {
                                type = itemP.type,
                                key = itemP.key,
                                name = itemP.name
                            };
                            menuModel.button.Add(clickModel);
                            break;
                        case "view":
                            var viewModel = new viewModel()
                            {
                                type = itemP.type,
                                url = itemP.url,
                                name = itemP.name
                            };
                            menuModel.button.Add(viewModel);
                            break;
                        case "miniprogram":
                            var miniprogramModel = new miniprogramModel()
                            {
                                type = itemP.type,
                                url = itemP.url,
                                appid = itemP.appid,
                                pagepath = itemP.pagepath,
                                name = itemP.name
                            };
                            menuModel.button.Add(miniprogramModel);
                            break;
                        case "scancode_waitmsg":
                        case "scancode_push":
                        case "pic_sysphoto":
                        case "pic_photo_or_album":
                        case "pic_weixin":
                            var subbuttonModel = new subbuttonModel()
                            {
                                type = itemP.type,
                                key = itemP.key,
                                name = itemP.name,
                                sub_button = new List<object>(),
                            };
                            menuModel.button.Add(subbuttonModel);
                            break;
                        case "media_id":
                        case "view_limited":
                            var mediaidModel = new mediaidModel()
                            {
                                type = itemP.type,
                                media_id = itemP.media_id,
                                name = itemP.name,
                            };
                            menuModel.button.Add(mediaidModel);
                            break;
                    } 
                    #endregion
                }

            }
            var sdk = new WeiXinSdkTask();
            var result =  CustomMenuAPI.Create(sdk.AccountToken(), JsonConvert.SerializeObject(menuModel));
            if (result)
            {
                return Json(new RequestResultDto() { Msg = "发布成功", Ret = 0 });
            }
            return Json(new RequestResultDto() { Msg = "发布失败", Ret = -1 });
        }

    }

    public class MenuModel
    {
        public List<object> button{get;set;} 
    }
    public class MenuChildModel
    {
        public string name { get; set; }
        public List<object> sub_button{get;set;}
    }

    public class clickModel
    {
        public string type { get; set; }
        public string name { get; set; }
        public string key { get; set; }
    }

    public class subbuttonModel
    {
        public string type { get; set; }
        public string name { get; set; }
        public string key { get; set; }
        public List<object> sub_button { get; set; }
    }

    public class mediaidModel
    {
        public string type { get; set; }
        public string name { get; set; }
        public string media_id { get; set; }
    }

    public class viewModel
    {
        public string type { get; set; }
        public string name { get; set; }
        public string url { get; set; }
    }

    public class miniprogramModel
    {
        public string type { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string appid { get; set; }
        public string pagepath { get; set; }
    }
}
