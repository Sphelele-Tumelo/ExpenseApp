using Banking_App._2.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Banking_App._2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ExpenseDbContxt _configuration;

        public HomeController(ILogger<HomeController> logger, ExpenseDbContxt configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }
       // the method below is the one going to be called when you press the overview button
       // going to return the Expenses html page
        public IActionResult Expenses()
        {
            var allExpenses = _configuration.Expenses.ToList();

            var totalExpenses = allExpenses.Sum(x => x.Value);

            ViewBag.Expenses = totalExpenses;

            return View(allExpenses);
        }

        // the method below is the one going to be called when you press the Create/ Edit button
        // going to return the Expenses html page
        public IActionResult CreateEditExpense(int Id)
        {
            if(Id == 0)
            {
                var expenseinDb = _configuration.Expenses.SingleOrDefault(expense => expense.Id == Id);
                return View(expenseinDb);
                //editing --> load an expense by an Id
            }


            return View();
        }
        public IActionResult DeleteExpense(int? Id)
        {
            var expenseinDb = _configuration.Expenses.SingleOrDefault(expense => expense.Id == Id);
            _configuration.Expenses.Remove(expenseinDb);
            _configuration.SaveChanges();
            return RedirectToAction("Expense");   
        }
        // the method below is the one going to be called when you press the overview button
        // going to return the Expenses html page

        /// <summary>
        ///  this method will will call and render the index method in the application, this means after the user clicks 
        ///  ok button the INDEX METHOD WILL be called and the view will appear
        /// </summary>
        /// <returns></returns>
        public IActionResult CreateEditExpenseForm(Expense model)
        {
            if (model.Id == 0)
            {
                // Creating an expense
              _configuration.Expenses.Add(model);
            }
            else
            {
                //  Editing an expense
                _configuration.Expenses.Update(model);
            }

            _configuration.SaveChanges();

            return RedirectToAction("Expenses");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
