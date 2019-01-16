using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models;
using WebAPI.RepositoryInterfaces;
using WebAPI.ServiceInterfaces;
using _128Utility.Network.Machine;
using WebAPI.Utilities;

namespace WebAPI.Services
{
    public class AuditTrailService<T>: IAuditTrailService<T> where T: class
    {
        private readonly IAuditTrailRepository repo;
        MachineIdentifier mi = new MachineIdentifier();
        public AuditTrailService(IAuditTrailRepository repo)
        {
            this.repo = repo;
        }

        public string GetHost { get => mi.SetHost(); }
        public string GetIpAddress { get => mi.SetIPAddress(); }

        public async Task Save(T oldObj, T newObj, string trans)
        {
            try
            {
                var utils = new Utility();
                // get message
                var message = utils.CreateMesssage<T>(oldObj, newObj);

                //var cloneData = utils.Clone<T>(newObj);

                var auditTrail = new AuditTrail();
                //auditTrail.
                auditTrail.ClientNetAddress = GetIpAddress;
                auditTrail.HostName = GetHost;
                auditTrail.Trans = trans;
                auditTrail.Message = message;

                await repo.Insert(auditTrail);
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
