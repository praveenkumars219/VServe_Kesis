using GraphQl.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQl.Repository
{
    public class JobDetailRepository
    {
        private readonly MongoContext _context;
        private readonly IMongoCollection<JobDetail> _jobDetails;

        public JobDetailRepository(MongoContext context)
        {
            _context = context;
            _jobDetails = _context.MongoDb.GetCollection<JobDetail>("JobDetails");
        }

        public async Task<IEnumerable<JobDetail>> Get() =>
           await _jobDetails.Find(job => true).ToListAsync().ConfigureAwait(false);

        public async Task<IEnumerable<JobDetail>> Get(string id) =>
           (await _jobDetails.Find<JobDetail>(job => job.UserId == id).ToListAsync().ConfigureAwait(false));

        public async Task<IEnumerable<JobDetail>> GetByType(string jobtype,string location) =>
            (await _jobDetails.Find<JobDetail>(job => job.JobProfile.JobTitle.Contains(jobtype) && job.Location.LocationName.Contains(location)).ToListAsync().ConfigureAwait(false));

        public async Task<bool> Create(JobDetail job)
        {
            job.Location.Id = ObjectId.GenerateNewId().ToString();
            job.JobProfile.Id = ObjectId.GenerateNewId().ToString();
            try
            {
                await _jobDetails.InsertOneAsync(job).ConfigureAwait(false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Update(string id, JobDetail jobIn)
        {
            try
            {
                await _jobDetails.ReplaceOneAsync(job => job.Id == id, jobIn).ConfigureAwait(false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task Remove(JobDetail jobIn) =>
           await _jobDetails.DeleteOneAsync(job => job.Id == jobIn.Id).ConfigureAwait(false);

        public async Task Remove(string id) =>
           await _jobDetails.DeleteOneAsync(job => job.Id == id).ConfigureAwait(false);
    }
}
