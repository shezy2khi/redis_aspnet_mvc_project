using CacheRedisASP.Models;
using CacheRedisASP.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CacheRedisASP.Controllers
{
    public class TransactionController : Controller
    {
        private readonly MyDbContext _context;
        private readonly DataService _dataService;
        


        public TransactionController(MyDbContext context, DataService dataService)
        {
            _context = context;
            _dataService = dataService;


        }

        // GET: Transactions
             public async Task<IActionResult> Index()
        {
          
            return View(await _context.Transactions.ToListAsync());
        }

 
        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int id)
        {
            

            var transaction = await _dataService.FindDetails(id);

            return View(transaction);
        }

     }
}
