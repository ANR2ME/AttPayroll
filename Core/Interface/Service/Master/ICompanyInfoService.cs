﻿using Core.DomainModel;
using Core.Interface.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Interface.Service
{
    public interface ICompanyInfoService
    {
        ICompanyInfoValidator GetValidator();
        IQueryable<CompanyInfo> GetQueryable();
        IList<CompanyInfo> GetAll();
        CompanyInfo GetObjectById(int Id);
        CompanyInfo GetObjectByName(string Name);
        //CompanyInfo FindOrCreateBaseCompanyInfo();
        CompanyInfo CreateObject(CompanyInfo companyInfo);
        CompanyInfo CreateObject(string Name, string Address, string PostalCode, string PhoneNumber, string FaxNumber, string Website, string Email);
        CompanyInfo UpdateObject(CompanyInfo companyInfo);
        CompanyInfo SoftDeleteObject(CompanyInfo companyInfo);
        bool DeleteObject(int Id);
        bool IsNameDuplicated(CompanyInfo companyInfo);
    }
}