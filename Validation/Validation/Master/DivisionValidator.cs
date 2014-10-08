﻿using Core.DomainModel;
using Core.Interface.Validation;
using Core.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Validation.Validation
{
    public class DivisionValidator : IDivisionValidator
    {
        public Division VHasDepartment(Division division, IDepartmentService _departmentService)
        {
            Department department = _departmentService.GetObjectById(division.DepartmentId);
            if (department == null)
            {
                division.Errors.Add("Department", "Tidak valid");
            }
            return division;
        }

        public Division VHasUniqueName(Division division, IDivisionService _divisionService)
        {
            if (String.IsNullOrEmpty(division.Name) || division.Name.Trim() == "")
            {
                division.Errors.Add("Name", "Tidak boleh kosong");
            }
            else if (_divisionService.IsNameDuplicated(division))
            {
                division.Errors.Add("Name", "Tidak boleh ada duplikasi");
            }
            return division;
        }

        public bool ValidCreateObject(Division division, IDivisionService _divisionService, IDepartmentService _departmentService)
        {
            VHasDepartment(division, _departmentService);
            if (!isValid(division)) { return false; }
            VHasUniqueName(division, _divisionService);
            return isValid(division);
        }

        public bool ValidUpdateObject(Division division, IDivisionService _divisionService, IDepartmentService _departmentService)
        {
            division.Errors.Clear();
            ValidCreateObject(division, _divisionService, _departmentService);
            return isValid(division);
        }

        public bool ValidDeleteObject(Division division)
        {
            division.Errors.Clear();
            return isValid(division);
        }

        public bool isValid(Division obj)
        {
            bool isValid = !obj.Errors.Any();
            return isValid;
        }

        public string PrintError(Division obj)
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

    }
}
