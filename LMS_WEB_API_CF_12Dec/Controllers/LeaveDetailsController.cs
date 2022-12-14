using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LMS_WEB_API_CF_12Dec.Models;

namespace LMS_WEB_API_CF_12Dec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveDetailsController : ControllerBase
    {
        private readonly EmployeeDbContext _context, _context2, _context3;

        public LeaveDetailsController(EmployeeDbContext context)
        {
            _context= _context2= _context3 = context;
        }

        // GET: api/LeaveDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ApplyLeave>>> GetLeaves()
        {
            return await _context.Leaves.ToListAsync();
        }

        // GET: api/LeaveDetails/5
        [Route("Leavedetailsbyid")]
        [HttpGet]
        public async Task<ActionResult<ApplyLeave>> GetLeaveDetailsbyId(int id)
        {
            var leaveDetails = await _context.Leaves.FindAsync(id);

            if (leaveDetails == null)
            {
                return NotFound();
            }

            return leaveDetails;
        }
        //GET:api/LeaveDetails/5
        [Route("Leavedetailsemployee")]
        [HttpPost]
        public async Task<ActionResult<IEnumerable<ApplyLeave>>>GetLeaveDetails(string eid)
        {
            var result = await _context.Leaves.Where(e => e.EmployeeId == eid).ToListAsync();
            if(result == null)
            {
                return NotFound();
            }
            return result;
        }
        //****************
        //Prabi
        [Route("GetPendingLeaveapplications")]
        [HttpPost]
        public IEnumerable<ApplyLeave> GetPendingLeaveapplications(string eid)
        {
            var managerName = _context.Employees.Find(int.Parse(eid)).Name;//getmangerName
            var employees = _context2.Employees.AsEnumerable().Where(e => e.ManagerName == managerName).ToList();//getAllEmployees having this manager
            var pendingleaves = _context3.Leaves.AsEnumerable().Where(l => l.Status == "Pending").ToList();//all pending leaves
            var result = pendingleaves.Where(l => employees.Any(e => int.Parse(l.EmployeeId) == e.EmployeeId)).ToList();//sort pendingleaves having this manager
            return result;
        }
        //Prabi
        [Route("ActionByManager")]
        [HttpPatch]
        public string ActionByManager(int lid,string status,string comment)
        {

            var leave =_context.Leaves.FirstOrDefault(l => l.ApplyLeaveId == lid);
            leave.Status = status;
            leave.ManagerComments = comment;
            _context.SaveChanges();
            return "Success";

        }
        //****************
        //// GET: api/LeaveDetails/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<ApplyLeave>> GetApplyLeave(int id)
        //{
        //    var applyLeave = await _context.Leaves.FindAsync(id);

        //    if (applyLeave == null)
        //    {
        //        return NotFound();
        //    }

        //    return applyLeave;
        //}

        // PUT: api/LeaveDetails/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApplyLeave(int id, ApplyLeave applyLeave)
        {
            if (id != applyLeave.ApplyLeaveId)
            {
                return BadRequest();
            }

            _context.Entry(applyLeave).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplyLeaveExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/LeaveDetails
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ApplyLeave>> PostApplyLeave(ApplyLeave applyLeave)
        {
            _context.Leaves.Add(applyLeave);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApplyLeave", new { id = applyLeave.ApplyLeaveId }, applyLeave);
        }

        // DELETE: api/LeaveDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApplyLeave>> DeleteApplyLeave(int id)
        {
            var applyLeave = await _context.Leaves.FindAsync(id);
            if (applyLeave == null)
            {
                return NotFound();
            }

            _context.Leaves.Remove(applyLeave);
            await _context.SaveChangesAsync();

            return applyLeave;
        }

        [Route("overlappingDates")]
        [HttpGet]
        public Response OverlapDates(string startDate,string endDate)
        {
            
            var leave = _context.Leaves.Where(l => l.StartDate.Equals(DateTime.Parse(startDate))  && l.EndDate.Equals(DateTime.Parse(endDate)));
            if (leave.Count() == 0)
            {
                return new Response { Status = "Success", Message = "Not Overlap" };
            }
            else
            {
                return new Response { Status = "Invalid", Message = "Overlaped" };
            }
               
            

        }
        private bool ApplyLeaveExists(int id)
        {
            return _context.Leaves.Any(e => e.ApplyLeaveId == id);
        }
    }
}
