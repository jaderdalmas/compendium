using Compendium.Extension;
using Compendium.Models;
using Compendium.Notify;
using Compendium.Repository.Interfaces;
using Compendium.Repository.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace Compendium.Controllers
{
  public class TableController : Controller
  {
    private readonly INotification _notification;
    private readonly ITableRepository _tableRepository;

    public TableController(INotification notification, ITableRepository tableRepository)
    {
      _notification = notification;
      _tableRepository = tableRepository;
    }

    [HttpGet]
    public IActionResult Index(string name = Consts.NullString)
    {
      IEnumerable<TableEntity> result = null;
      try
      {
        if (string.IsNullOrWhiteSpace(name))
          result = _tableRepository.GetAll().Result;
        else
          result = _tableRepository.GetAll(name).Result;
      }
      catch (ExternalException e)
      {
        _notification.AddNotification(e.StackTrace, e.Message, LogLevel.Error);
      }

      if (result != null)
        return View(result.Select(x => x.GetViewModel));
      else
        return RedirectToAction(nameof(Index), "Home");
    }

    [HttpGet]
    public IActionResult Details(long id)
    {
      try
      {
        var tableEntity = _tableRepository.GetOne(id).Result;
        if (tableEntity is null) return RedirectToAction(nameof(Index));

        return View(tableEntity.GetViewModel);
      }
      catch (ExternalException e)
      {
        _notification.AddNotification(e.StackTrace, e.Message, LogLevel.Error);
        return RedirectToAction(nameof(Index), "Home");
      }
    }

    [HttpGet]
    public IActionResult Create()
    {
      return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Create([Bind("Id,Name,Type,Description")] TableViewModel viewModel)
    {
      try
      {
        if (ModelState.IsValid && viewModel != null)
        {
          _tableRepository.Insert(viewModel.GetEntity()).Wait();
          return RedirectToAction(nameof(Index));
        }
        return View(viewModel);
      }
      catch (ExternalException e)
      {
        _notification.AddNotification(e.StackTrace, e.Message, LogLevel.Error);
        return RedirectToAction(nameof(Index), "Home");
      }
    }

    [HttpGet]
    public IActionResult Edit(long id)
    {
      try
      {
        var tableEntity = _tableRepository.GetOne(id).Result;
        if (tableEntity is null) return RedirectToAction(nameof(Index));

        return View(tableEntity.GetViewModel);
      }
      catch (ExternalException e)
      {
        _notification.AddNotification(e.StackTrace, e.Message, LogLevel.Error);
        return RedirectToAction(nameof(Index), "Home");
      }
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Edit(long id, [Bind("Id,Description")] TableDescViewModel viewModel)
    {
      if (ModelState.IsValid && viewModel != null && id == viewModel.Id)
      {
        try
        {
          _tableRepository.Update(viewModel.GetEntity()).Wait();
        }
        catch (Exception)
        {
          if (_tableRepository.Exists(viewModel.Id).Result) throw;
        }
        return RedirectToAction(nameof(Index));
      }
      return View(viewModel);
    }

    [HttpGet]
    public IActionResult Delete(long id)
    {
      try
      {
        var tableEntity = _tableRepository.GetOne(id).Result;
        if (tableEntity is null) return RedirectToAction(nameof(Index));

        return View(tableEntity.GetViewModel);
      }
      catch (ExternalException e)
      {
        _notification.AddNotification(e.StackTrace, e.Message, LogLevel.Error);
        return RedirectToAction(nameof(Index), "Home");
      }
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(long id)
    {
      _tableRepository.Delete(id).Wait();
      return RedirectToAction(nameof(Index));
    }
  }
}
