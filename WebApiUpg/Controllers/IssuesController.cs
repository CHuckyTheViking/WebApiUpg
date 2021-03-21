using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedLibrary.Data;
using SharedLibrary.Models.Issue;

namespace WebApiUpg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuesController : ControllerBase
    {
        private readonly SqlDbContext _context;

        public IssuesController(SqlDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> AddIssue([FromBody] AddIssueModel model)
        {
            try
            {

                var issue = new Issue
                {
                    Created = DateTime.Now,
                    Information = model.Information,
                    Status = _context.Statuses.Where(a => a.Id == model.StatusId).FirstOrDefault(),
                    Customer = _context.Customers.Where(a => a.Id == model.CustomerId).FirstOrDefault(),
                    User = _context.Users.Where(a => a.Id == model.UserId).FirstOrDefault()
                };

                _context.Issues.Add(issue);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return new OkResult();
        }

        [Authorize]
        [HttpPost("customer")]
        public async Task<IActionResult> SortByCustomer([FromBody] SortIssues model)
        {
            try
            {
                var customer = await _context.Customers.FirstOrDefaultAsync(a => a.CustomerName == model.CustomerName);

                if (customer != null)
                {
                    var issues = _context.Issues.Where(a => a.CustomerId == customer.Id).ToList();

                }

            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return new OkResult();
        }


        [Authorize]
        [HttpPost("user")]
        public async Task<IActionResult> SortByUser([FromBody] SortIssues model)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(a => a.Id == model.UserId);

                if (user != null)
                {
                    var issues = _context.Issues.Where(a => a.UserId == user.Id).ToList();
                }

            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return new OkResult();
        }

        [Authorize]
        [HttpPost("status")]
        public IActionResult SortByStatus([FromBody] SortIssues model)
        {
            try
            {
                var issues = _context.Issues.Where(a => a.StatusId == model.StatusId).ToList();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return new OkResult();
        }


        [Authorize]
        [HttpGet("statuses")]
        public IActionResult AllStatuses()
        {
            try
            {
                var status = _context.Statuses.ToList();
                return new OkObjectResult(status);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("all")]
        public IActionResult AllIssues()
        {
            try
            {

                var issues = _context.Issues.Include(a => a.Customer).Include(a => a.User).Include(a=> a.Status).ToList();
                return new OkObjectResult(issues);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("update")]
        public IActionResult UpdateIssue(Issue issue)
        {
            try
            {
                
                
                var changeIssue = _context.Issues.FirstOrDefaultAsync(a => a.Id == issue.Id).Result;

                if (changeIssue != null)
                {
                    changeIssue.Changed = DateTime.Now;
                    changeIssue.Information = issue.Information;
                    changeIssue.StatusId = issue.StatusId;
                    changeIssue.CustomerId = issue.CustomerId;
                    changeIssue.UserId = issue.UserId;

                    _context.Issues.Update(changeIssue);
                    _context.SaveChanges();


                }
                
                
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return new OkResult();
        }

        [Authorize]
        [HttpGet("id")]
        public IActionResult SelectedIssue(int id)
        {
            try
            {

                var issue = _context.Issues.Where(a => a.Id == id).Include(a => a.Customer).Include(a => a.User).ToList();
                return new OkObjectResult(issue);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }

        }






    }
}
