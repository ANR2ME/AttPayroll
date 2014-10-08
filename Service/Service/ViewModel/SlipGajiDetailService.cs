﻿using Core.DomainModel;
using Core.Interface.Repository;
using Core.Interface.Service;
using Core.Interface.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Service.Service
{
    public class SlipGajiDetailService : ISlipGajiDetailService
    {
        private ISlipGajiDetailRepository _repository;
        private ISlipGajiDetailValidator _validator;
        public SlipGajiDetailService(ISlipGajiDetailRepository _slipGajiDetailRepository, ISlipGajiDetailValidator _slipGajiDetailValidator)
        {
            _repository = _slipGajiDetailRepository;
            _validator = _slipGajiDetailValidator;
        }

        public ISlipGajiDetailValidator GetValidator()
        {
            return _validator;
        }

        public ISlipGajiDetailRepository GetRepository()
        {
            return _repository;
        }

        public IQueryable<SlipGajiDetail> GetQueryable()
        {
            return _repository.GetQueryable();
        }

        public IList<SlipGajiDetail> GetAll()
        {
            return _repository.GetAll();
        }

        public SlipGajiDetail GetObjectById(int Id)
        {
            return _repository.GetObjectById(Id);
        }

        public SlipGajiDetail GetObjectByEmployeeMonth(int EmployeeId, DateTime YearMonth)
        {
            return _repository.FindAll(x => x.EmployeeId == EmployeeId && x.MONTH.Year == YearMonth.Year && x.MONTH.Month == YearMonth.Month).FirstOrDefault();
        }

        public SlipGajiDetail FindOrCreateObject(SlipGajiDetail slipGajiDetail, IEmployeeService _employeeService, ISlipGajiDetail1Service _slipGajiDetail1Service, ISlipGajiDetail2AService _slipGajiDetail2AService)
        {
            slipGajiDetail.Errors = new Dictionary<String, String>();
            if (_validator.ValidCreateObject(slipGajiDetail, _employeeService, _slipGajiDetail1Service, _slipGajiDetail2AService))
            {
                SlipGajiDetail slipGajiDetail2 = GetObjectByEmployeeMonth(slipGajiDetail.EmployeeId, slipGajiDetail.MONTH);
                if (slipGajiDetail2 == null)
                {
                    _repository.CreateObject(slipGajiDetail);
                }
                else
                {
                    slipGajiDetail.Id = slipGajiDetail2.Id;
                    _repository.UpdateObject(slipGajiDetail);
                }
            }
            return slipGajiDetail;
        }

        public SlipGajiDetail CreateObject(SlipGajiDetail slipGajiDetail, IEmployeeService _employeeService, ISlipGajiDetail1Service _slipGajiDetail1Service, ISlipGajiDetail2AService _slipGajiDetail2AService)
        {
            slipGajiDetail.Errors = new Dictionary<String, String>();
            return (_validator.ValidCreateObject(slipGajiDetail, _employeeService, _slipGajiDetail1Service, _slipGajiDetail2AService) ? _repository.CreateObject(slipGajiDetail) : slipGajiDetail);
        }

        public SlipGajiDetail UpdateObject(SlipGajiDetail slipGajiDetail, IEmployeeService _employeeService, ISlipGajiDetail1Service _slipGajiDetail1Service, ISlipGajiDetail2AService _slipGajiDetail2AService)
        {
            return (slipGajiDetail = _validator.ValidUpdateObject(slipGajiDetail, _employeeService, _slipGajiDetail1Service, _slipGajiDetail2AService) ? _repository.UpdateObject(slipGajiDetail) : slipGajiDetail);
        }

        public bool DeleteObject(int Id)
        {
            return _repository.DeleteObject(Id);
        }
    }
}