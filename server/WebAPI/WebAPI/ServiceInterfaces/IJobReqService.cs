﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;

namespace WebAPI.ServiceInterfaces
{
    public interface IJobReqService
    {
        Task<CustomMessage> Insert(JobReq jr);
        Task<CustomMessage> Update(JobReq jr);
        Task<CustomMessage> Delete(int id);
    }
}
