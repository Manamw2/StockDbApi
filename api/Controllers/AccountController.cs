using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _UserManager;
        private readonly ITokenService _TokenService;
        private readonly SignInManager<AppUser> _SignInManager;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _UserManager = userManager;
            _TokenService = tokenService;
            _SignInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] LogInDto logInDto){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var user = await _UserManager.Users.FirstOrDefaultAsync(u => u.UserName == logInDto.UserName.ToLower());
            if(user == null){
                return Unauthorized("Invalid username");
            }
            var PasswordCheckResult = await _SignInManager.CheckPasswordSignInAsync(user, logInDto.Password, false);
            if(PasswordCheckResult.Succeeded){
                return Ok(
                    new NewUserDto{
                        UserName = user.UserName,
                        Email = user.Email,
                        Token = _TokenService.CreateToken(user)
                    }
                );
            }
            else{
                return Unauthorized("Wrong Password");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto){
            try{
                if(!ModelState.IsValid){
                    return BadRequest(ModelState);
                }
                
                var user = new AppUser{
                    UserName = registerDto.UserName,
                    Email = registerDto.Email
                };
                
                var userObj = await _UserManager.CreateAsync(user, registerDto.Password);
                
                if(userObj.Succeeded){
                    var roleObj = await _UserManager.AddToRoleAsync(user, "user");
                    if(roleObj.Succeeded){
                        return Ok(
                            new NewUserDto{
                                UserName = user.UserName,
                                Email = user.Email,
                                Token = _TokenService.CreateToken(user)
                            }
                        );
                    }
                    else{
                        return StatusCode(500, roleObj.Errors);
                    }
                }
                else{
                    return StatusCode(500, userObj.Errors);
                }

            }catch(Exception e){
                return StatusCode(500, e);
            }
        }
    }
}