using Compendium.Filter;
using Compendium.Notify;
using Compendium.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Compendium.Controllers
{
  public class HomeController : Controller
  {
    private readonly INotification _notification;
    private readonly ISyscatSyncService _syscatSyncService;

    public HomeController(INotification notification, ISyscatSyncService syscatSyncService)
    {
      _notification = notification;
      _syscatSyncService = syscatSyncService;
    }

    public IActionResult Index()
    {
      return View();
    }

    public IActionResult Sync()
    {
      Task.Run(() => _syscatSyncService.Sync().Wait());

      return RedirectToAction(nameof(Index), "Table");
    }

    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = (int)decimal.Zero, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(CustomReturn.NotificationReturn(Request, _notification));
    }
  }
}
