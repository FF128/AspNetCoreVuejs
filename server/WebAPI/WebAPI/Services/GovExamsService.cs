using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Helpers;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;

namespace WebAPI.Services
{
    public class GovExamsService : IGovExamsService
    {
        private readonly IGovExamsRepository repo;
        private readonly IAuditTrailService<GovExams> auditTrailService;
        private readonly ICompanyInformationRepository compInfoRepo;
        public GovExamsService(IGovExamsRepository repo,
             IAuditTrailService<GovExams> auditTrailService,
             ICompanyInformationRepository compInfoRepo)
        {
            this.repo = repo;
            this.auditTrailService = auditTrailService;
            this.compInfoRepo = compInfoRepo;
        }

        public async Task<CustomMessage> Delete(int id)
        {
            var govExam = await repo.GetById(id);
            if (govExam != null)
            {
                await repo.Delete(id);

                await auditTrailService.Save(new GovExams(), govExam, "DELETE");

                return CustomMessageHandler.RecordDeleted();
            }
            return CustomMessageHandler.Error("Data doesn't exist");
        }

        public async Task<CustomMessage> Insert(GovExams ge)
        {
            if (String.IsNullOrEmpty(ge.ExamCode) || String.IsNullOrWhiteSpace(ge.ExamCode))
            {
                return CustomMessageHandler.Error("Code: field is required");
            }

            if ((await repo.GetByCode(ge.ExamCode)) == null)
            {
                ge.CompanyCode = compInfoRepo.GetCompanyCode();
                await repo.Insert(ge);

                await auditTrailService.Save(new GovExams(), ge, "ADD");

                return CustomMessageHandler.RecordAdded();

            }
            return CustomMessageHandler.Error("Code is already used");
        }

        public async Task<CustomMessage> Update(GovExams ge)
        {
            var govExam = await repo.GetByCode(ge.ExamCode);
            if (govExam != null)
            {
                await repo.Update(ge);

                //Audit Trail
                await auditTrailService.Save(govExam, ge, "EDIT");

                return CustomMessageHandler.RecordUpdated();

            }
            return CustomMessageHandler.Error("You can't edit this code");
        }
    }
}
