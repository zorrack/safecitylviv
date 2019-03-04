using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Orleans;
using SafeCity.Model;
using SafeCity.GrainInterfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SafeCityWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class IssuesController : Controller
    {
        private IClusterClient clusterClient;

        public IssuesController(IClusterClient client)
        {
            clusterClient = client;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<IssueMessageItem> Get()
        {
            return new List<IssueMessageItem>();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IssueMessageItem Get(Guid id)
        {
            var message = this.clusterClient.GetGrain<IIssueMessage>(id);
            return message.GetItem().Result;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]IssueMessageItem value)
        {
            var message = this.clusterClient.GetGrain<IIssueMessage>(value.Id);
            message.SetItem(value).Wait();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody]IssueMessageItem value)
        {
            var message = this.clusterClient.GetGrain<IIssueMessage>(value.Id);
            message.SetItem(value).Wait();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
        }

        [HttpPost("{id}/postrelateditem")]
        public async Task PostRelatedItem(Guid id, [FromBody]IssueMessageItem relatedItem)
        {
            var grain = this.clusterClient.GetGrain<IIssueMessage>(id);
            var relatedGrain = this.clusterClient.GetGrain<IIssueMessage>(relatedItem.Id);

            var item = await grain.GetItem();
            item.RelatedMessage = relatedGrain;
            await relatedGrain.SetItem(relatedItem);
            await grain.SetItem(item);
        }

        [HttpGet("{id}/relateditem")]
        public async Task<IssueMessageItem> GetRelatedItem(Guid id)
        {
            var message = this.clusterClient.GetGrain<IIssueMessage>(id);

            var item = await message.GetItem();
            var relatedMessage = item.RelatedMessage;
            var relatedItem = await relatedMessage.GetItem();

            return relatedItem;
        }
    }
}
