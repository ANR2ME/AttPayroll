﻿using Core.DomainModel;
using Core.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Interface.Validation
{
    public interface IOtherIncomeValidator
    {

        bool ValidCreateObject(OtherIncome otherIncome, IEmployeeService _employeeService, ISalaryItemService _salaryItemService);
        bool ValidUpdateObject(OtherIncome otherIncome, IEmployeeService _employeeService, ISalaryItemService _salaryItemService);
        bool ValidDeleteObject(OtherIncome otherIncome);
        bool isValid(OtherIncome otherIncome);
        string PrintError(OtherIncome otherIncome);
    }
}