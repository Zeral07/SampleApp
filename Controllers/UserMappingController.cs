using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SampleApp.Attributes;
using SampleApp.Enums;
using SampleApp.Helpers;
using SampleApp.Interface;
using SampleApp.Models;
using SampleApp.Parameters;
using SampleApp.ViewModels;
using System.Net;
using System.Text;

namespace SampleApp.Controllers
{
    [ServiceFilter(typeof(AuthorizeValidateAttribute))]
    public class UserMappingController(IUserFacade userFacade, IMapper mapper) : Controller
    {
        private readonly IUserFacade _userFacade = userFacade;
        private readonly IMapper _mapper = mapper;

        #region Index
        public async Task<IActionResult> Index()
        {
            string condition = "RowStatus == @0";
            var parameters = new List<object> { (short)DBRowStatus.Active };
            PaginationFilter<UserParameter> data = new PaginationFilter<UserParameter>();
            var response = await _userFacade.GetByParameterWithPaging(condition, parameters, data.PageNumber, data.PageSize);
            data.Total = response.Total;
            data.Result = _mapper.Map<List<UserParameter>>(response.Result) ?? [];
            return View(data);
        }

        [HttpPost]
        [Route("UserMapping/Delete/{id:int}")]
        public async Task<JsonResult> Index(int id)
        {
            try
            {
                bool result = await _userFacade.Delete(id);

                var jsonData = new
                {
                    IsError = !result,
                    Message = result ? ConstantHelper.MESSAGE_DELETE : ConstantHelper.MESSAGE_NOTHING_CHANGES
                };

                return Json(jsonData);
            }
            catch (Exception e)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { IsError = true, Message = UtilityHelper.GetExceptionMessage(e) });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Index([Bind("PageNumber,PageSize,Parameter,Result,Total")] PaginationFilter<UserParameter> paginationFilter)
        {
            ViewBag.IsSuccess = false;
            ViewBag.Message = "";

            string condition = "RowStatus == @0";
            var parameters = new List<object> { (short)DBRowStatus.Active };
            if (paginationFilter.Parameter is not null)
            {
                if (!string.IsNullOrEmpty(paginationFilter.Parameter.FullName))
                {
                    parameters.Add(paginationFilter.Parameter.FullName);
                    condition += $" and FullName.Contains(@{parameters.Count - 1})";
                }

                if (!string.IsNullOrEmpty(paginationFilter.Parameter.Email))
                {
                    parameters.Add(paginationFilter.Parameter.Email);
                    condition += $" and Email.Contains(@{parameters.Count - 1})";
                }

                if (!string.IsNullOrEmpty(paginationFilter.Parameter.Position))
                {
                    parameters.Add(paginationFilter.Parameter.Position);
                    condition += $" and Position.Contains(@{parameters.Count - 1})";
                }
            }

            var response = await _userFacade.GetByParameterWithPaging(condition, parameters, paginationFilter.PageNumber, paginationFilter.PageSize);
            paginationFilter.Total = response.Total;
            paginationFilter.PageNumber = response.PageNumber;
            paginationFilter.Result = _mapper.Map<List<UserParameter>>(response.Result) ?? [];
            return View(paginationFilter);
        }
        #endregion

        #region Edit
        public async Task<IActionResult> Form(int id)
        {
            if (id == 0)
                return View(new UserDto());

            var data = await _userFacade.GetByID(id);
            var userDto = _mapper.Map<UserDto>(data);

            return View(userDto);
        }

        [HttpPost]
        public async Task<IActionResult> Form(UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = _mapper.Map<User>(userDto);
                    if (userDto.Id == 0)
                    {
                        user = await _userFacade.Create(user);
                        userDto = _mapper.Map<UserDto>(user);
                    }
                    else
                        await _userFacade.Update(user);

                    TempData["IsSuccess"] = true;
                    TempData["Message"] = ConstantHelper.MESSAGE_SAVE;
                }
                catch (Exception e)
                {
                    TempData["IsSuccess"] = false;
                    ViewBag.Message = UtilityHelper.GetExceptionMessage(e);
                }
            }
            else
            {
                StringBuilder msgBuilder = new();

                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        msgBuilder.AppendLine(error.ErrorMessage);
                    }
                }

                TempData["IsSuccess"] = false;
                TempData["Message"] = msgBuilder.ToString();
            }

            return RedirectToAction("Form", new { id = userDto.Id });
        }
        #endregion
    }
}
