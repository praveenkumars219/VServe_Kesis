using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQl.Models;
using GraphQl.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraphQl.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobDetailController : ControllerBase
    {
        private readonly JobDetailRepository _jobDetailRepository;

        public JobDetailController(JobDetailRepository jobDetailRepository)
        {
            _jobDetailRepository = jobDetailRepository;
        }

        // GET: api/JobDetail
        [HttpGet]
        public async Task<IEnumerable<JobDetail>> Get()
        {
            return await _jobDetailRepository.Get().ConfigureAwait(false);
        }

        // GET: api/JobDetail/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IEnumerable<JobDetail>> Get(string id)
        {
            return await _jobDetailRepository.Get(id).ConfigureAwait(false);
        }

        [Route("GetJobByType")]
        [HttpGet]
        public async Task<IEnumerable<JobDetail>> GetByType(string jobtype,string location)
        {
            return await _jobDetailRepository.GetByType(jobtype, location).ConfigureAwait(false);
        }

        // POST: api/JobDetail
        [HttpPost]
        public async Task<bool> Post([FromBody] JobDetail detail)
        {
            if (string.IsNullOrWhiteSpace(detail.Id))
            {
                return await _jobDetailRepository.Create(detail).ConfigureAwait(false);
            }
            return await _jobDetailRepository.Update(detail.Id,detail).ConfigureAwait(false);
        }

        // PUT: api/JobDetail/5
        [HttpPost("{id}")]
        public async Task<bool> Put(string id, [FromBody] JobDetail detail)
        {
            return await _jobDetailRepository.Update(id,detail).ConfigureAwait(false);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
