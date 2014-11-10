﻿using Core.DomainModel;
using Core.Interface.Validation;
using Core.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Constants;

namespace Validation.Validation
{
    public class OtherIncomeDetailValidator : IOtherIncomeDetailValidator
    {
        public OtherIncomeDetail VHasOtherIncome(OtherIncomeDetail otherIncomeDetail, IOtherIncomeService _otherIncomeService)
        {
            OtherIncome otherIncome = _otherIncomeService.GetObjectById(otherIncomeDetail.OtherIncomeId);
            if (otherIncome == null)
            {
                otherIncomeDetail.Errors.Add("OtherIncome", "Tidak ada");
            }
            return otherIncomeDetail;
        }

        public OtherIncomeDetail VHasEmployee(OtherIncomeDetail otherIncomeDetail, IEmployeeService _employeeService)
        {
            Employee employee = _employeeService.GetObjectById(otherIncomeDetail.EmployeeId);
            if (employee == null)
            {
                otherIncomeDetail.Errors.Add("Employee", "Tidak ada");
            }
            return otherIncomeDetail;
        }

        public OtherIncomeDetail VHasEffectiveDate(OtherIncomeDetail otherIncomeDetail)
        {
            if (otherIncomeDetail.EffectiveDate == null || otherIncomeDetail.EffectiveDate.Equals(DateTime.FromBinary(0)))
            {
                otherIncomeDetail.Errors.Add("EffectiveDate", "Tidak valid");
            }
            return otherIncomeDetail;
        }

        public OtherIncomeDetail VHasAmount(OtherIncomeDetail otherIncomeDetail)
        {
            if (otherIncomeDetail.Amount < 0)
            {
                otherIncomeDetail.Errors.Add("Amount", "Harus lebih besar atau sama dengan 0");
            }
            return otherIncomeDetail;
        }

        public OtherIncomeDetail VHasRecurring(OtherIncomeDetail otherIncomeDetail)
        {
            if (otherIncomeDetail.Recurring < 1)
            {
                otherIncomeDetail.Errors.Add("Recurring", "Harus lebih besar atau sama dengan 1");
            }
            return otherIncomeDetail;
        }

        public bool ValidCreateObject(OtherIncomeDetail otherIncomeDetail, IOtherIncomeService _otherIncomeService, IEmployeeService _employeeService)
        {
            VHasOtherIncome(otherIncomeDetail, _otherIncomeService);
            if (!isValid(otherIncomeDetail)) { return false; }
            VHasEmployee(otherIncomeDetail, _employeeService);
            if (!isValid(otherIncomeDetail)) { return false; }
            VHasEffectiveDate(otherIncomeDetail);
            if (!isValid(otherIncomeDetail)) { return false; }
            VHasAmount(otherIncomeDetail);
            if (!isValid(otherIncomeDetail)) { return false; }
            VHasRecurring(otherIncomeDetail);
            FixDate(otherIncomeDetail, _otherIncomeService);
            return isValid(otherIncomeDetail);
        }

        public bool ValidUpdateObject(OtherIncomeDetail otherIncomeDetail, IOtherIncomeService _otherIncomeService, IEmployeeService _employeeService)
        {
            otherIncomeDetail.Errors.Clear();
            ValidCreateObject(otherIncomeDetail, _otherIncomeService, _employeeService);
            return isValid(otherIncomeDetail);
        }

        public bool ValidDeleteObject(OtherIncomeDetail otherIncomeDetail)
        {
            otherIncomeDetail.Errors.Clear();
            return isValid(otherIncomeDetail);
        }

        public bool isValid(OtherIncomeDetail obj)
        {
            bool isValid = !obj.Errors.Any();
            return isValid;
        }

        public string PrintError(OtherIncomeDetail obj)
        {
            string erroroutput = "";
            KeyValuePair<string, string> first = obj.Errors.ElementAt(0);
            erroroutput += first.Key + "," + first.Value;
            foreach (KeyValuePair<string, string> pair in obj.Errors.Skip(1))
            {
                erroroutput += Environment.NewLine;
                erroroutput += pair.Key + "," + pair.Value;
            }
            return erroroutput;
        }

        public OtherIncomeDetail FixDate(OtherIncomeDetail otherIncomeDetail, IOtherIncomeService _otherIncomeService)
        {
            otherIncomeDetail.EndDate = otherIncomeDetail.EffectiveDate;
            OtherIncome otherIncome = _otherIncomeService.GetObjectById(otherIncomeDetail.OtherIncomeId);
            if (otherIncome != null)
            {
                DateTime curDay = otherIncomeDetail.EffectiveDate.GetValueOrDefault();
                int cnt = otherIncomeDetail.Recurring;
                while (cnt > 1)
                {
                    switch ((Constant.SalaryItemStatus)otherIncome.SalaryStatus)
                    {
                        case Constant.SalaryItemStatus.Daily: otherIncomeDetail.EndDate = curDay.AddDays(1); break;
                        case Constant.SalaryItemStatus.Weekly: otherIncomeDetail.EndDate = curDay.AddDays(7); break;
                        case Constant.SalaryItemStatus.Monthly: otherIncomeDetail.EndDate = curDay.AddMonths(1); break;
                        case Constant.SalaryItemStatus.Yearly: otherIncomeDetail.EndDate = curDay.AddYears(1); break;
                    }
                    curDay = otherIncomeDetail.EndDate.GetValueOrDefault();
                    cnt--;
                }
            }
            return otherIncomeDetail;
        }


    }
}
