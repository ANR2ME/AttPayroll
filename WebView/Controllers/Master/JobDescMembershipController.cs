﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Service;
using Core.Interface.Service;
using Core.DomainModel;
using Data.Repository;
using Validation.Validation;
using System.Linq.Dynamic;
using System.Data.Entity;
using Service;

namespace WebView.Controllers
{
    
    public class JobDescMembershipController : Controller
    {
        private readonly static log4net.ILog LOG = log4net.LogManager.GetLogger("JobDescMembershipController");
        public IEmployeeService _employeeService;
        public MockService _mockService;

        public JobDescMembershipController()
        {
            _employeeService = new EmployeeService(new EmployeeRepository(), new EmployeeValidator());
            _mockService = new MockService();
        }

        public ActionResult Index()
        {
            if (!AuthenticationModel.IsAllowed("View", Core.Constants.Constant.MenuName.EmployeeEducationInfo, Core.Constants.Constant.MenuGroupName.Setting))
            {
                return Content(Core.Constants.Constant.ErrorPage.PageViewNotAllowed);
            }

            return View(this);
        }

        public dynamic GetList(string _search, long nd, int rows, int? page, string sidx, string sord, int id, string filters = "")
        {
            // Construct where statement
            string strWhere = GeneralFunction.ConstructWhere(filters);
            string filter = null;
            GeneralFunction.ConstructWhereInLinq(strWhere, out filter);
            if (filter == "") filter = "true";

            // Get Data
            IList<JobDescMembership> q = new List<JobDescMembership>();
            for (int i = 1; i < 3; i++)
            {
                q.Add(_mockService.GenerateJobDescMembership(i));
            }

            var query = q.Where(x => x.ApplicantId == id);

            var list = query.AsEnumerable();

            var pageIndex = Convert.ToInt32(page) - 1;
            var pageSize = rows;
            var totalRecords = query.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);
            // default last page
            if (totalPages > 0)
            {
                if (!page.HasValue)
                {
                    pageIndex = totalPages - 1;
                    page = totalPages;
                }
            }

            list = list.Skip(pageIndex * pageSize).Take(pageSize);

            return Json(new
            {
                total = totalPages,
                page = page,
                records = totalRecords,
                rows = (
                    from model in list
                    select new
                    {
                        id = model.Id,
                        cell = model.GetType()
                                .GetProperties()
                                .Select(p =>
                                {
                                    object value = p.GetValue(model, null);
                                    return value == null ? null : value; //.ToString();
                                })
                                .ToArray()
                    }).ToArray()
            }, JsonRequestBehavior.AllowGet);
        }

        public dynamic GetListByJobDesc(string _search, long nd, int rows, int? page, string sidx, string sord, int id, string filters = "")
        {
            // Construct where statement
            string strWhere = GeneralFunction.ConstructWhere(filters);
            string filter = null;
            GeneralFunction.ConstructWhereInLinq(strWhere, out filter);
            if (filter == "") filter = "true";

            // Get Data
            IList<JobDescMembership> q = new List<JobDescMembership>();
            for (int i = 1; i < 3; i++)
            {
                q.Add(_mockService.GenerateJobDescMembership(i));
            }

            var query = q.Where(x => x.JobDescId == id);

            var list = query.AsEnumerable();

            var pageIndex = Convert.ToInt32(page) - 1;
            var pageSize = rows;
            var totalRecords = query.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);
            // default last page
            if (totalPages > 0)
            {
                if (!page.HasValue)
                {
                    pageIndex = totalPages - 1;
                    page = totalPages;
                }
            }

            list = list.Skip(pageIndex * pageSize).Take(pageSize);

            return Json(new
            {
                total = totalPages,
                page = page,
                records = totalRecords,
                rows = (
                    from model in list
                    select new
                    {
                        id = model.Id,
                        cell = model.GetType()
                                .GetProperties()
                                .Select(p =>
                                {
                                    object value = p.GetValue(model, null);
                                    return value == null ? null : value; //.ToString();
                                })
                                .ToArray()
                    }).ToArray()
            }, JsonRequestBehavior.AllowGet);
        }

        public dynamic GetInfo(int Id)
        {
            JobDescMembership model = new JobDescMembership();
            try
            {
                model = _mockService.GenerateJobDescMembership(Id); //_employeePersonalService.GetObjectById(Id);
            }
            catch (Exception ex)
            {
                LOG.Error("GetInfo", ex);
                model.Errors.Add("Generic", "Error " + ex);

                return Json(new
                {
                    model
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                model
            }, JsonRequestBehavior.AllowGet);
        }

        public dynamic GetDefaultInfo()
        {
            JobDescMembership model = new JobDescMembership();
            try
            {
                model = _mockService.GenerateJobDescMembership(1); //_employeePersonalService.GetObjectById(Id);
            }
            catch (Exception ex)
            {
                LOG.Error("GetInfo", ex);
                model.Errors.Add("Generic", "Error " + ex);

                return Json(new
                {
                    model
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                model
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic Insert(JobDescMembership model)
        {
            try
            {
                if (!AuthenticationModel.IsAllowed("Create", Core.Constants.Constant.MenuName.EmployeeEducationInfo, Core.Constants.Constant.MenuGroupName.Setting))
                {
                    Dictionary<string, string> Errors = new Dictionary<string, string>();
                    Errors.Add("Generic", "You are Not Allowed to Add record");

                    return Json(new
                    {
                        Errors
                    }, JsonRequestBehavior.AllowGet);
                }

                model = new JobDescMembership(); // _employeeJobDescMembershipService.CreateObject(model, _workingTimeService, _employeeService);
            }
            catch (Exception ex)
            {
                LOG.Error("Insert Failed", ex);
                Dictionary<string, string> Errors = new Dictionary<string, string>();
                Errors.Add("Generic", "Error " + ex);

                return Json(new
                {
                    Errors
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                model.Errors
            });
        }

        [HttpPost]
        public dynamic Update(JobDescMembership model)
        {
            try
            {
                if (!AuthenticationModel.IsAllowed("Edit", Core.Constants.Constant.MenuName.EmployeeEducationInfo, Core.Constants.Constant.MenuGroupName.Setting))
                {
                    Dictionary<string, string> Errors = new Dictionary<string, string>();
                    Errors.Add("Generic", "You are Not Allowed to Edit record");

                    return Json(new
                    {
                        Errors
                    }, JsonRequestBehavior.AllowGet);
                }

                //var data = _employeeJobDescMembershipService.GetObjectById(model.Id);
                //data.WorkingTimeId = model.WorkingTimeId;
                //data.EmployeeId = model.EmployeeId;
                ////data.IsEnabled = model.IsEnabled;
                ////data.IsShiftable = model.IsShiftable;
                //data.StartDate = model.StartDate;
                //data.EndDate = model.EndDate;
                //model = _employeeJobDescMembershipService.UpdateObject(data, _workingTimeService, _employeeService);
            }
            catch (Exception ex)
            {
                LOG.Error("Update Failed", ex);
                Dictionary<string, string> Errors = new Dictionary<string, string>();
                Errors.Add("Generic", "Error " + ex);

                return Json(new
                {
                    Errors
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                model.Errors
            });
        }

        [HttpPost]
        public dynamic UpdateEnable(JobDescMembership model, bool isEnabled)
        {
            try
            {
                if (!AuthenticationModel.IsAllowed("Edit", Core.Constants.Constant.MenuName.EmployeeEducationInfo, Core.Constants.Constant.MenuGroupName.Setting))
                {
                    Dictionary<string, string> Errors = new Dictionary<string, string>();
                    Errors.Add("Generic", "You are Not Allowed to Edit record");

                    return Json(new
                    {
                        Errors
                    }, JsonRequestBehavior.AllowGet);
                }

                //var data = _employeeJobDescMembershipService.GetObjectById(model.Id);
                ////data.IsEnabled = isEnabled;
                //model = _employeeJobDescMembershipService.UpdateObject(data, _workingTimeService, _employeeService);
            }
            catch (Exception ex)
            {
                LOG.Error("Update Failed", ex);
                Dictionary<string, string> Errors = new Dictionary<string, string>();
                Errors.Add("Generic", "Error " + ex);

                return Json(new
                {
                    Errors
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                model.Errors
            });
        }

        [HttpPost]
        public dynamic Delete(JobDescMembership model)
        {
            try
            {
                if (!AuthenticationModel.IsAllowed("Delete", Core.Constants.Constant.MenuName.EmployeeEducationInfo, Core.Constants.Constant.MenuGroupName.Setting))
                {
                    Dictionary<string, string> Errors = new Dictionary<string, string>();
                    Errors.Add("Generic", "You are Not Allowed to Delete Record");

                    return Json(new
                    {
                        Errors
                    }, JsonRequestBehavior.AllowGet);
                }

                //var data = _employeeJobDescMembershipService.GetObjectById(model.Id);
                //model = _employeeJobDescMembershipService.SoftDeleteObject(data, _employeeService);
            }

            catch (Exception ex)
            {
                LOG.Error("Delete Failed", ex);
                Dictionary<string, string> Errors = new Dictionary<string, string>();
                Errors.Add("Generic", "Error " + ex);

                return Json(new
                {
                    Errors
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                model.Errors
            });
        }
    }
}
