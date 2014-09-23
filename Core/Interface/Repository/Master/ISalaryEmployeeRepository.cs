﻿using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Interface.Repository
{
    public interface ISalaryEmployeeRepository : IRepository<SalaryEmployee>
    {
        IQueryable<SalaryEmployee> GetQueryable();
        IList<SalaryEmployee> GetAll();
        SalaryEmployee GetObjectById(int Id);
        SalaryEmployee CreateObject(SalaryEmployee salaryEmployee);
        SalaryEmployee UpdateObject(SalaryEmployee salaryEmployee);
        SalaryEmployee SoftDeleteObject(SalaryEmployee salaryEmployee);
        bool DeleteObject(int Id);
    }
}