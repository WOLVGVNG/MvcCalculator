#nullable enable
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcCalculator.Data;
using MvcCalculator.Models;
using X.PagedList;


namespace MvcCalculator.Controllers
{
    [Authorize]
    public class CalculatorController : Controller
    {
        private readonly MvcCalculatorContext _context;

        public CalculatorController(MvcCalculatorContext context)
        {
            _context = context;
        }

        // GET
        public IActionResult Index(float? result, int page = 1)
        {
            ViewData["userId"] = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (result != null) ViewData["result"] = result.ToString().Replace(",", ".");
            else ViewData["result"] = "null";
            ViewData["Calculations"] = _context.Calculation
                .Where(c => c.UserId.Contains((string) ViewData["userId"]))
                .OrderByDescending(c => c.Id)
                .ToPagedList(page, 10);
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Calculate([Bind("UserId, Number1, Number2, Operator")] Calculation calculation)
        {
            if (!ModelState.IsValid)
            {
                var query = from state in ModelState.Values
                    from error in state.Errors
                    select error.ErrorMessage;
                var errors = query.ToArray(); // ToList() and so on...
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ToString());
                }
                return Redirect("/Calculator");
            }
            _context.Add(calculation);
            await _context.SaveChangesAsync();
            float result = 0;
            Console.Write(calculation.Number1);
            Console.Write(calculation.Number2);
            switch (calculation.Operator)
            {
                case "+": 
                    result = calculation.Number1 + calculation.Number2;
                    break;
                case "-":
                    result = calculation.Number1 + calculation.Number2;
                    break;
                case "*":
                    result = calculation.Number1 + calculation.Number2;
                    break;
                case "/":
                    result = calculation.Number1 + calculation.Number2;
                    break;
            }
            Console.Write(result);
            return RedirectToAction("Index", "Calculator", new { result = result });
        }
    }
};