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
    public class SalaryEmployeeService : ISalaryEmployeeService
    {
        private ISalaryEmployeeRepository _repository;
        private ISalaryEmployeeValidator _validator;
        public SalaryEmployeeService(ISalaryEmployeeRepository _salaryEmployeeRepository, ISalaryEmployeeValidator _salaryEmployeeValidator)
        {
            _repository = _salaryEmployeeRepository;
            _validator = _salaryEmployeeValidator;
        }

        public ISalaryEmployeeValidator GetValidator()
        {
            return _validator;
        }

        public IQueryable<SalaryEmployee> GetQueryable()
        {
            return _repository.GetQueryable();
        }

        public IList<SalaryEmployee> GetAll()
        {
            return _repository.GetAll();
        }

        public SalaryEmployee GetObjectById(int Id)
        {
            return _repository.GetObjectById(Id);
        }

        public SalaryEmployee CreateObject(SalaryEmployee salaryEmployee)
        {
            salaryEmployee.Errors = new Dictionary<String, String>();
            return (_validator.ValidCreateObject(salaryEmployee, this) ? _repository.CreateObject(salaryEmployee) : salaryEmployee);
        }

        public SalaryEmployee UpdateObject(SalaryEmployee salaryEmployee)
        {
            return (salaryEmployee = _validator.ValidUpdateObject(salaryEmployee, this) ? _repository.UpdateObject(salaryEmployee) : salaryEmployee);
        }

        public SalaryEmployee SoftDeleteObject(SalaryEmployee salaryEmployee)
        {
            return (salaryEmployee = _validator.ValidDeleteObject(salaryEmployee) ?
                    _repository.SoftDeleteObject(salaryEmployee) : salaryEmployee);
        }

        public bool DeleteObject(int Id)
        {
            return _repository.DeleteObject(Id);
        }

        
    }
}