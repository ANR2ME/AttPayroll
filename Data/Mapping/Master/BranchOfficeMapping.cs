﻿using System.Data.Entity.ModelConfiguration;
using Core.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Mapping
{
    public class BranchOfficeMapping : EntityTypeConfiguration<BranchOffice>
    {
        public BranchOfficeMapping()
        {
            HasKey(bo => bo.Id);
            HasOptional(bo => bo.CompanyInfo)
                .WithMany() //ci => ci.BranchOffices)
                .HasForeignKey(bo => bo.CompanyInfoId);
                //.WillCascadeOnDelete(true);
            Ignore(bo => bo.Errors);
        }
    }
}