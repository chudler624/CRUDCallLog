using CallLogger.Data;
using CallLogger.Models;
using CallLogger.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CallLogger.Controllers
{
    public class CallsController : Controller
    {
        private readonly CallLogDbContext callLogDbContext;

        public CallsController(CallLogDbContext callLogDbContext)
        {
            this.callLogDbContext = callLogDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var callLog = await callLogDbContext.CallLogger.ToListAsync();
            return View(callLog);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddToCallLogModel addToCallLog)
        {
            var callLog = new CallLog()
            {
                Id = Guid.NewGuid(),
                TimeOfCall = addToCallLog.TimeOfCall,
                NameOfCaller = addToCallLog.NameOfCaller,
                CallerPhoneNumber = addToCallLog.CallerPhoneNumber,
                IntendedRecipient = addToCallLog.IntendedRecipient,
                MessageForRecipient = addToCallLog.MessageForRecipient,
            };

            await callLogDbContext.CallLogger.AddAsync(callLog);
            await callLogDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task <IActionResult> View(Guid Id)
        {
            var callLog = await callLogDbContext.CallLogger.FirstOrDefaultAsync(x => x.Id == Id);

            if (callLog != null)
            {
                var viewModel = new UpdateCallLogModel()
                {
                    Id = callLog.Id,
                    TimeOfCall = callLog.TimeOfCall,
                    NameOfCaller = callLog.NameOfCaller,
                    CallerPhoneNumber = callLog.CallerPhoneNumber,
                    IntendedRecipient = callLog.IntendedRecipient,
                    MessageForRecipient = callLog.MessageForRecipient,
                };

                return await Task.Run(() => View("View", viewModel));

            }

            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateCallLogModel model)
        {
            var callLog = await callLogDbContext.CallLogger.FindAsync(model.Id);

            if (callLog != null)
            {
                callLog.TimeOfCall = model.TimeOfCall;
                callLog.NameOfCaller = model.NameOfCaller;
                callLog.CallerPhoneNumber = model.CallerPhoneNumber;
                callLog.IntendedRecipient = model.IntendedRecipient;
                callLog.MessageForRecipient = model.MessageForRecipient;

                await callLogDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateCallLogModel model)
        {
            var callLog = await callLogDbContext.CallLogger.FindAsync(model.Id);

            if (callLog != null)
            {
                callLogDbContext.CallLogger.Remove(callLog);
                await callLogDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
