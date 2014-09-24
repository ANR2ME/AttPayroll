﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainModel
{
    public partial class LastEducation
    {
        public int Id { get; set; }

        public string Institute { get; set; }
        public string Major { get; set; }
        public string EducationLevel { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public Nullable<DateTime> GraduationDate { get; set; }
        public int EmployeeId { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }

        public Dictionary<string, string> Errors { get; set; }

        public virtual Employee Employee { get; set; }

    }
}
