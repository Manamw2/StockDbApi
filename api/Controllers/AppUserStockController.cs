using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Extentions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/appuserStocks")]
    [ApiController]
    public class AppUserStockController : ControllerBase
    {
        private readonly UserManager<AppUser> _UserManager;
        private readonly IAppUserStockRepository _AppUserStockRepo;
        private readonly IStockRepository _StockRepo;
        public AppUserStockController(UserManager<AppUser> userManager, IStockRepository stockRepo, IAppUserStockRepository appUserStockRepo)
        {
            _UserManager = userManager;
            _AppUserStockRepo = appUserStockRepo;
            _StockRepo = stockRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserStocks(){
            var userName = User.GetUserName();
            var user = await _UserManager.FindByNameAsync(userName);
            var userStocks = await _AppUserStockRepo.GetUserStocks(user);
            return Ok(userStocks);
        }

        [HttpPost("{stockId:int}")]
        [Authorize]
        public async Task<IActionResult> AddUserStockAsync([FromRoute] int stockId){
            var userName = User.GetUserName();
            var user = await _UserManager.FindByNameAsync(userName);
            if(!await _StockRepo.StockExist(stockId)){
                return BadRequest("Stock not found");
            }
            var appuserStock = await _AppUserStockRepo.AddUserStockAsync(user, stockId);
            return Ok();
        }

        [HttpDelete("{stockId:int}")]
        [Authorize]
        public async Task<IActionResult> RemoveUserStockAsync([FromRoute] int stockId){
            var userName = User.GetUserName();
            var user = await _UserManager.FindByNameAsync(userName);
            if(!await _StockRepo.StockExist(stockId)){
                return BadRequest("Stock not found");
            }
            var appuserStock = await _AppUserStockRepo.RemoveUserStockAsync(user, stockId);
            if(appuserStock == null){
                return BadRequest("Not found");
            }
            return Ok();
        }
    }
}