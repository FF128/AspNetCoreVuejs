﻿using RS_WebApiMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS_WebApiMvc.RepositoryInterfaces
{
    public interface IResidenceTypeRepository
    {
        Task<IEnumerable<ResidenceType>> GetAll();
        Task<ResidenceType> GetById(int id);
        Task Insert(ResidenceType rt);
        Task Update(ResidenceType rt);
        Task Delete(int id);
    }
}