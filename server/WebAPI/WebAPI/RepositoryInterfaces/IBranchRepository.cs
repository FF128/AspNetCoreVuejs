﻿using WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Dtos;
using WebAPI.Dtos.Branch;

namespace WebAPI.RepositoryInterfaces
{
    public interface IBranchRepository
    {
        Task<IEnumerable<Branch>> GetAll();
        Task<Branch> GetById(int id);
        Task<Branch> GetByCode(string code);
        Task<dynamic> GetByCodeFromPayroll(string code, string payrollDB);
        Task<dynamic> GetByCodeFromHRIS(string code, string hrisDB);
        Task<dynamic> GetByCodeFromTKS(string code, string tskDB);
        Task Insert(Branch branch);
        Task InsertFileSetup(BranchInsertToFSDto dto);
        Task InsertToPayrollFileSetUp(BranchInsertToPayrollFSDto dto);
        Task InsertToHRISFileSetUp(BranchInsertToHRISFSDto dto);
        Task InsertToTSKFileSetUp(BranchInsertToTKSFSDto dto);
        Task Update(Branch branch);
        Task UpdateFileSetup(BranchUpdateToFSDto dto);
        Task UpdateToPayrollFileSetUp(BranchUpdateToPayrollFSDto dto);
        Task UpdateToHRISFileSetUp(BranchUpdateToHRISFSDto dto);
        Task UpdateToTKSFileSetUp(BranchUpdateToTKSFSDto dto);
        Task Delete(string code);
        Task DeleteFromHRISFileSetUp(DeleteSetUpDto dto);
        Task DeleteFromPayrollFileSetUp(DeleteSetUpDto dto);
        Task DeleteFromTKSFileSetUp(DeleteSetUpDto dto);
        Task DeleteFileSetUp(string code);
    }
}
