﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DomainModel
{
    public partial class Employee
    {
        public int Id { get; set; }

        public string NIK { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string PlaceOfBirth { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int Sex { get; set; }
        public int MaritalStatus { get; set; } //Single/Married
        public int Children { get; set; }
        public int Religion { get; set; }
        public string Father { get; set; }
        public string Mother { get; set; }
        public int ChildNo { get; set; }
        public int NumberOfChild { get; set; }
        public string NPWP { get; set; }
        //public string NPWPAddress { get; set; }
        public string JamsostekNo { get; set; }
        public string BankCode { get; set; }
        public string BankAccount { get; set; }
        public int WorkingStatus { get; set; } // Magang/Kontrak/MasaPercobaan/PegawaiTetap
        public Nullable<DateTime> StartWorking { get; set; }
        public Nullable<DateTime> Appointment { get; set; } // Mulai jadi pegawai tetap
        public Nullable<int> LastEducationId { get; set; }
        public Nullable<int> LastEmploymentId { get; set; }
        public bool IsActive { get; set; } // NonAktif/Aktif/MedicalCheck?
        public Nullable<DateTime> NonActiveDate { get; set; } 
        public int DivisionId { get; set; }
        public int TitleInfoId { get; set; }
        public Nullable<int> EmployeeWorkingTimeId { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
        public Nullable<DateTime> DeletedAt { get; set; }

        public Dictionary<string, string> Errors { get; set; }

        public virtual LastEducation LastEducation { get; set; }
        public virtual LastEmployment LastEmployment { get; set; }
        public virtual Division Division { get; set; }
        public virtual TitleInfo TitleInfo { get; set; }
        public virtual EmployeeWorkingTime EmployeeWorkingTime { get; set; }
    }
}
