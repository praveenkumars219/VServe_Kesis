using GraphQl.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GraphQl.Repository
{
    public class AssignmentsRepository
    {
        private readonly MongoContext _context;
        private readonly IMongoCollection<Assignment> _assignments;
        private readonly IMongoCollection<Payment> _payments;
        public AssignmentsRepository(MongoContext context)
        {
            _context = context;
            _assignments = _context.MongoDb.GetCollection<Assignment>("Assignments");
            _payments = _context.MongoDb.GetCollection<Payment>("Payments");
        }

        public async Task<IEnumerable<Assignment>> Get() =>
          await _assignments.Find(job => true).ToListAsync().ConfigureAwait(false);

        public async Task<IEnumerable<Assignment>> Get(string id) =>
           (await _assignments.Find<Assignment>(job => job.AssignedUserId == id).ToListAsync().ConfigureAwait(false));

        public async Task<IEnumerable<Payment>> GetPayments(string id) =>
            (await _payments.Find<Payment>(payment => payment.Payer == id || payment.Payee == id).ToListAsync().ConfigureAwait(false));

        //public async Task<IEnumerable<Assignment>> GetByType(string jobtype, string location) =>
        //    (await _assignments.Find<Assignment>(job => job.JobProfile.JobTitle.Contains(jobtype) && job.Location.LocationName.Contains(location)).ToListAsync().ConfigureAwait(false));

        public async Task<bool> Create(Assignment job)
        {
            try
            {
                await _assignments.InsertOneAsync(job).ConfigureAwait(false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> CreatePayment(Payment payment)
        {
            try
            {
                await _payments.InsertOneAsync(payment).ConfigureAwait(false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> Update(string id, Assignment job)
        {
            try
            {
                await _assignments.ReplaceOneAsync(job => job.Id == id, job).ConfigureAwait(false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdatePayment(string id, Payment payment)
        {
            try
            {
                await _payments.ReplaceOneAsync(job => job.Id == id, payment).ConfigureAwait(false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task Remove(Assignment jobIn) =>
           await _assignments.DeleteOneAsync(job => job.Id == jobIn.Id).ConfigureAwait(false);

        public async Task Remove(string id) =>
           await _assignments.DeleteOneAsync(job => job.Id == id).ConfigureAwait(false);
    }
}
