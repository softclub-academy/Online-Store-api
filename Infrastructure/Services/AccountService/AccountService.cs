using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Domain.Dtos.AccountDtos;
using Domain.Entities;
using Domain.Response;
using Infrastructure.Data;
using Infrastructure.Seed;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Services.AccountService;

public class AccountService(UserManager<User> userManager,
    IConfiguration configuration, ApplicationContext context) : IAccountService
{
    public async Task<Response<string>> Register(RegisterDto model)
    {
        try
        {
            var user = new User()
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };
            await userManager.CreateAsync(user, model.Password);
            await userManager.AddToRoleAsync(user, Role.User);
            var userProfile = new UserProfile()
            {
                UserId = user.Id,
                FirstName = "",
                LastName = "",
                Email = "",
                PhoneNumber = "",
                Image = ""
            };
            await context.UserProfiles.AddAsync(userProfile);
            await context.SaveChangesAsync();
            return new Response<string>(user.Id);
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<string>> Login(LoginDto model)
    {
        try
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            if (user != null)
            {
                var check = await userManager.CheckPasswordAsync(user, model.Password);
                if (check)
                {
                    return new Response<string>(await GenerateJwtToken(user));
                }
            }

            return new Response<string>("Your UserName or Password is incorrect!!!");
        }
        catch (Exception e)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
    
    private async Task<string> GenerateJwtToken(User user)
    {
        var userProfile = await context.UserProfiles.FindAsync(user.Id);
        var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!);
        var securityKey = new SymmetricSecurityKey(key);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>()
        {
            new(JwtRegisteredClaimNames.Sid, user.Id),
            new(JwtRegisteredClaimNames.Name, user.UserName!),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(JwtRegisteredClaimNames.Sub, userProfile!.Image!)
        };
        //add roles
        var roles = await userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var token = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(10),
            signingCredentials: credentials
        );

        var securityTokenHandler = new JwtSecurityTokenHandler();
        var tokenString = securityTokenHandler.WriteToken(token);
        return tokenString;
    }
}