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
    [Route("api/assignments")]
    [ApiController]
    public class AssignmentController  : ControllerBase
    {
        private readonly AssignmentsRepository _assignment;

        public AssignmentController(AssignmentsRepository assignment)
        {
            _assignment = assignment;
        }

        // GET: api/Assignment
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Assignment/5
        [HttpGet("{id}", Name = "Get Assignments")]
        public string Get(int id)
        {
            return "value";
        }

        [Route("GetAssignmentsByUserId")]
        [HttpGet]
        public async Task<IEnumerable<Assignment>> GetAssignments(string id)
        {
            return await _assignment.Get(id).ConfigureAwait(false);
        }

        [Route("GetPaymentsByUserId")]
        [HttpGet]
        public async Task<IEnumerable<Payment>> GetPayments(string id)
        {
            return await _assignment.GetPayments(id).ConfigureAwait(false);
        }

        // POST: api/Assignment
        [HttpPost]
        public async Task<bool> Post([FromBody] Assignment assignment)
        {
            if (string.IsNullOrWhiteSpace(assignment.Id))
            {
                return await _assignment.Create(assignment).ConfigureAwait(false);
            }
            
            return await _assignment.Update(assignment.Id,assignment).ConfigureAwait(false);
        }

        [Route("SavePayment")]
        [HttpPost]
        public async Task<bool> PostPayments([FromBody] Payment payment)
        {
            if (string.IsNullOrWhiteSpace(payment.Id))
            {
                return await _assignment.CreatePayment(payment).ConfigureAwait(false);
            }

            return await _assignment.UpdatePayment(payment.Id, payment).ConfigureAwait(false);
        }

        // PUT: api/Assignment/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
