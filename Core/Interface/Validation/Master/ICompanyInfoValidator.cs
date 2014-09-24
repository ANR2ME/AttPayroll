﻿using Core.DomainModel;
using Core.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Interface.Validation
{
    public interface ICompanyInfoValidator
    {
        CompanyInfo VHasUniqueName(CompanyInfo companyInfo, ICompanyInfoService _companyInfoService);
        CompanyInfo VHasAddress(CompanyInfo companyInfo);
        CompanyInfo VHasPhoneNumber(CompanyInfo companyInfo);
        CompanyInfo VHasEmail(CompanyInfo companyInfo);
        CompanyInfo VCreateObject(CompanyInfo companyInfo, ICompanyInfoService _companyInfoService);
        CompanyInfo VUpdateObject(CompanyInfo companyInfo, ICompanyInfoService _companyInfoService);
        CompanyInfo VDeleteObject(CompanyInfo companyInfo);
        bool ValidCreateObject(CompanyInfo companyInfo, ICompanyInfoService _companyInfoService);
        bool ValidUpdateObject(CompanyInfo companyInfo, ICompanyInfoService _companyInfoService);
        bool ValidDeleteObject(CompanyInfo companyInfo);
        bool isValid(CompanyInfo companyInfo);
        string PrintError(CompanyInfo companyInfo);
    }
}