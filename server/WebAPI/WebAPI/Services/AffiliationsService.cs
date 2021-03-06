﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Services
{
    public class AffiliationsService : IAffiliationsService
    {
        private readonly IAffiliationsRepository repo;
        private readonly IAuditTrailService<Affiliations> auditTrailService;
        private readonly ICompanyInformationRepository compInfoRepo;
        private readonly IUserRepository userRepo;
        public AffiliationsService(IAffiliationsRepository repo,
             IAuditTrailService<Affiliations> auditTrailService,
             ICompanyInformationRepository compInfoRepo,
             IUserRepository userRepo)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.compInfoRepo = compInfoRepo;
            this.userRepo = userRepo;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var affiliation = await repo.GetById(id);
            if (affiliation != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new Affiliations(), affiliation, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(Affiliations aff)
        {
            if (String.IsNullOrEmpty(aff.AffiliationsCode) || String.IsNullOrWhiteSpace(aff.AffiliationsCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(aff.AffiliationsCode)) == null)
            {
                aff.CompanyCode = compInfoRepo.GetCompanyCode();
                aff.CreatedBy = userRepo.GetEmpCode();
                await repo.Insert(aff);

                await auditTrailService.Save(new Affiliations(), aff, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(Affiliations aff)
        {
            var affiliation = await repo.GetByCode(aff.AffiliationsCode);
            if (affiliation != null)
            {
                aff.EditedBy = userRepo.GetEmpCode();
                await repo.Update(aff);

                //Audit Trail
                await auditTrailService.Save(affiliation, aff, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
