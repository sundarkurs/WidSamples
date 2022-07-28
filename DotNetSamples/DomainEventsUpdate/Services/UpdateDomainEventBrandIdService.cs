using DomainEventsUpdate.Model;
using Newtonsoft.Json;
using System;
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
                IQueryable<DomainEvent> queryableEvents = context.DomainEvents.Select(x => x).Where(c => c.BrandId == null);

                foreach (var domainEvent in queryableEvents)
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

                        Console.WriteLine($"Permanent Key: {permanentKey}, BrandId: {brandId}\n");
                    }
                    catch (Exception) { }
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
