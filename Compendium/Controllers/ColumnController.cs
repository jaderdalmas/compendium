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
  public class ColumnController : Controller
  {
    private readonly INotification _notification;
    private readonly IColumnRepository _columnRepository;

    public ColumnController(INotification notification, IColumnRepository columnRepository)
    {
      _notification = notification;
      _columnRepository = columnRepository;
    }

    [HttpGet]
    public IActionResult Index(string tableName, string name = Consts.NullString)
    {
      if (string.IsNullOrWhiteSpace(tableName))
        return RedirectToAction(nameof(Index), "Table");

      IEnumerable<ColumnEntity> result = null;
      try
      {
        result = _columnRepository.GetAll(tableName, name).Result;
      }
      catch (ExternalException e)
      {
        _notification.AddNotification(e.StackTrace, e.Message, LogLevel.Error);
      }

      if (result != null)
        return View(result.Select(x => x.GetViewModel));
      else
        return RedirectToAction(nameof(Index), "Table");
    }

    [HttpGet]
    public IActionResult Details(long id)
    {
      try
      {
        var columnEntity = _columnRepository.GetOne(id).Result;
        if (columnEntity is null) return RedirectToAction(nameof(Index), "Table");

        return View(columnEntity.GetViewModel);
      }
      catch (ExternalException e)
      {
        _notification.AddNotification(e.StackTrace, e.Message, LogLevel.Error);
        return RedirectToAction(nameof(Index), "Home");
      }
    }

    [HttpGet]
    public IActionResult Create(string tableName)
    {
      if (string.IsNullOrWhiteSpace(tableName))
        return RedirectToAction(nameof(Index), "Table");

      return View();
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Create([Bind("Id,TableName,Name,Number,Type,Length,Default,IsNull,IsIdentity,Description")] ColumnViewModel viewModel)
    {
      try
      {
        if (ModelState.IsValid && viewModel != null)
        {
          _columnRepository.Insert(viewModel.GetEntity()).Wait();
          return RedirectToAction(nameof(Index), new { tablename = viewModel.TableName });
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
        var columnEntity = _columnRepository.GetOne(id).Result;
        if (columnEntity is null) return RedirectToAction(nameof(Index), "Table");

        return View(columnEntity.GetViewModel);
      }
      catch (ExternalException e)
      {
        _notification.AddNotification(e.StackTrace, e.Message, LogLevel.Error);
        return RedirectToAction(nameof(Index), "Home");
      }
    }

    [HttpPost, ValidateAntiForgeryToken]
    public IActionResult Edit(string tableName, [Bind("Id,Description")] ColumnDescViewModel viewModel)
    {
      if (ModelState.IsValid && viewModel != null)
      {
        try
        {
          _columnRepository.Update(viewModel.GetEntity()).Wait();
        }
        catch (Exception)
        {
          if (_columnRepository.Exists(viewModel.Id).Result) throw;
        }
        return RedirectToAction(nameof(Index), new { tablename = tableName });
      }
      return View(viewModel);
    }

    [HttpGet]
    public IActionResult Delete(long id)
    {
      try
      {
        var columnEntity = _columnRepository.GetOne(id).Result;
        if (columnEntity is null) return RedirectToAction(nameof(Index), "Table");

        return View(columnEntity.GetViewModel);
      }
      catch (ExternalException e)
      {
        _notification.AddNotification(e.StackTrace, e.Message, LogLevel.Error);
        return RedirectToAction(nameof(Index), "Home");
      }
    }

    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(long id, string tableName)
    {
      _columnRepository.Delete(id).Wait();
      return RedirectToAction(nameof(Index), new { tablename = tableName });
    }
  }
}
