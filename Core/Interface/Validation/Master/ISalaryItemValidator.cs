﻿using Core.DomainModel;
using Core.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Interface.Validation
{
    public interface ISalaryItemValidator
    {
        SalaryItem VCreateObject(SalaryItem salaryItem, ISalaryItemService _salaryItemService);
        SalaryItem VUpdateObject(SalaryItem salaryItem, ISalaryItemService _salaryItemService);
        SalaryItem VDeleteObject(SalaryItem salaryItem);
        bool ValidCreateObject(SalaryItem salaryItem, ISalaryItemService _salaryItemService);
        bool ValidUpdateObject(SalaryItem salaryItem, ISalaryItemService _salaryItemService);
        bool ValidDeleteObject(SalaryItem salaryItem);
        bool isValid(SalaryItem salaryItem);
        string PrintError(SalaryItem salaryItem);
    }
}