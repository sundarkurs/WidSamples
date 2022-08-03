using DomainEventsUpdate.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DomainEventsUpdate.Services
{
    public class UpdateDomainEventBrandIdService
    {
        public UpdateDomainEventBrandIdService()
        {
        }

        public bool UpdateBrandId()
        {
            using (tstmsplmwsasdbContext context = new tstmsplmwsasdbContext())
            {
                int skip = 0;
                int take = 100;

                //IQueryable<DomainEvent> queryableEvents = context.DomainEvents.Select(x => x).Where(c => c.PermanentKey == null);
                List<DomainEvent> domainEvents = context.DomainEvents.Select(x => x).OrderBy(o => o.Id).Skip(skip).Take(take).ToList();

                while (domainEvents.Count > 0)
                {
                    foreach (var domainEvent in domainEvents)
                    {
                        try
                        {
                            dynamic data = JsonConvert.DeserializeObject(domainEvent.Payload);
                            string permanentKey = Convert.ToString(data.State.PermanentKey);
                            string brandId = string.Empty;

                            if (!string.IsNullOrEmpty(permanentKey))
                            {
                                brandId = permanentKey.Split('_')[0];

                                if (domainEvent.PermanentKey != permanentKey)
                                {
                                    domainEvent.PermanentKey = permanentKey;
                                    domainEvent.BrandId = brandId;
                                    UpdateEvent(domainEvent);
                                }
                            }

                            skip = domainEvent.Id;

                            Console.WriteLine($"Id: {domainEvent.Id}, Permanent Key: {permanentKey}, BrandId: {brandId}\n");
                        }
                        catch (Exception) { }
                    }

                    domainEvents.Clear();
                    domainEvents = context.DomainEvents.Select(x => x).OrderBy(o => o.Id).Skip(skip).Take(take).ToList();
                }


            }

            return true;
        }

        private void UpdateEvent(DomainEvent domainEvent)
        {
            using (tstmsplmwsasdbContext context = new tstmsplmwsasdbContext())
            {
                context.DomainEvents.Update(domainEvent);
                context.Entry(domainEvent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
