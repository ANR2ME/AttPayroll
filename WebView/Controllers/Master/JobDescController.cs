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
    
    public class JobDescController : Controller
    {
        private readonly static log4net.ILog LOG = log4net.LogManager.GetLogger("JobDescController");
        //public IJobDescService _jobDescService;
        //public ITitleInfoService _titleInfoService;
        //public IDivisionService _divisionService;
        //public IDepartmentService _departmentService;
        //public IBranchOfficeService _branchOfficeService;
        public IEmployeeService _employeeService;
        public MockService _mockService;

        public JobDescController()
        {
            //_jobDescService = new JobDescService(new JobDescRepository(), new JobDescValidator());
            //_titleInfoService = new TitleInfoService(new TitleInfoRepository(), new TitleInfoValidator());
            //_divisionService = new DivisionService(new DivisionRepository(), new DivisionValidator());
            //_departmentService = new DepartmentService(new DepartmentRepository(), new DepartmentValidator());
            //_branchOfficeService = new BranchOfficeService(new BranchOfficeRepository(), new BranchOfficeValidator());
            _employeeService = new EmployeeService(new EmployeeRepository(), new EmployeeValidator());
            _mockService = new MockService();

        }

        public ActionResult Index()
        {
            if (!AuthenticationModel.IsAllowed("View", Core.Constants.Constant.MenuName.JobDesc, Core.Constants.Constant.MenuGroupName.Setting))
            {
                return Content(Core.Constants.Constant.ErrorPage.PageViewNotAllowed);
            }

            return View(this);
        }

        public dynamic GetList(string _search, long nd, int rows, int? page, string sidx, string sord, string filters = "", int ParentId = 0)
        {
            // Construct where statement
            string strWhere = GeneralFunction.ConstructWhere(filters);
            string filter = null;
            GeneralFunction.ConstructWhereInLinq(strWhere, out filter);
            if (filter == "") filter = "true";

            // Get Data
            //var q = _jobDescService.GetQueryable().Include("Division").Include("TitleInfo").Include("JobDescWorkingTime").Where(x => (ParentId == 0 || x.DivisionId == ParentId));
            IList<JobDesc> q = new List<JobDesc>();
            for (int i = 1; i < 2; i++)
            {
                q.Add(_mockService.GenerateJobDesc(i));
            }

            var query = q;

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
            JobDesc model = new JobDesc();
            try
            {
                model = _mockService.GenerateJobDesc(Id);
            }
            catch (Exception ex)
            {
                LOG.Error("GetInfo", ex);
                Dictionary<string, string> Errors = new Dictionary<string, string>();
                Errors.Add("Generic", "Error " + ex);

                return Json(new
                {
                    Errors
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                model
            }, JsonRequestBehavior.AllowGet);
        }

        public dynamic GetDefaultInfo()
        {
            JobDesc model = new JobDesc();
            try
            {
                model = _mockService.GenerateJobDesc(1);
            }
            catch (Exception ex)
            {
                LOG.Error("GetInfo", ex);
                Dictionary<string, string> Errors = new Dictionary<string, string>();
                Errors.Add("Generic", "Error " + ex);

                return Json(new
                {
                    Errors
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                model
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public dynamic Insert(JobDesc model)
        {
            try
            {
                if (!AuthenticationModel.IsAllowed("Create", Core.Constants.Constant.MenuName.JobDesc, Core.Constants.Constant.MenuGroupName.Setting))
                {
                    Dictionary<string, string> Errors = new Dictionary<string, string>();
                    Errors.Add("Generic", "You are Not Allowed to Add record");

                    return Json(new
                    {
                        Errors
                    }, JsonRequestBehavior.AllowGet);
                }

                model = _mockService.GenerateJobDesc(1);
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
        public dynamic Update(JobDesc model)
        {
            try
            {
                if (!AuthenticationModel.IsAllowed("Edit", Core.Constants.Constant.MenuName.JobDesc, Core.Constants.Constant.MenuGroupName.Setting))
                {
                    Dictionary<string, string> Errors = new Dictionary<string, string>();
                    Errors.Add("Generic", "You are Not Allowed to Edit record");

                    return Json(new
                    {
                        Errors
                    }, JsonRequestBehavior.AllowGet);
                }

                model = _mockService.GenerateJobDesc(model.Id);
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
        public dynamic Delete(JobDesc model)
        {
            try
            {
                if (!AuthenticationModel.IsAllowed("Delete", Core.Constants.Constant.MenuName.JobDesc, Core.Constants.Constant.MenuGroupName.Setting))
                {
                    Dictionary<string, string> Errors = new Dictionary<string, string>();
                    Errors.Add("Generic", "You are Not Allowed to Delete Record");

                    return Json(new
                    {
                        Errors
                    }, JsonRequestBehavior.AllowGet);
                }

                //var data = _jobDescService.GetObjectById(model.Id);
                //model = _jobDescService.SoftDeleteObject(data);
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
