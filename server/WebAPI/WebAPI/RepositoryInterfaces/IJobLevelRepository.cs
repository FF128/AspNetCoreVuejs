﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;

namespace WebAPI.RepositoryInterfaces
{
    public interface IJobLevelRepository
    {
        Task<IEnumerable<JobLevel>> GetAll();
        Task<JobLevel> GetById(int id);
        Task Insert(JobLevel jobLevel);
        Task Update(JobLevel jobLevel);
        Task Delete(int id);
    }
}
