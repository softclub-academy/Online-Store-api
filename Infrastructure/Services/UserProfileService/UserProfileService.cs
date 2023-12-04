using System.ComponentModel.DataAnnotations;
using System.Net;
using Domain.Dtos.UserProfileDtos;
using Domain.Dtos.UserRoleDtos;
using Domain.Entities;
using Domain.Filters;
using Domain.Response;
using Infrastructure.Data;
using Infrastructure.Services.FileService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.UserProfileService;

public class UserProfileService(ApplicationContext context, UserManager<User> userManager,
    IFileService fileService) : IUserProfileService
{
    public async Task<PagedResponse<List<GetUserProfileDto>>> GetUserProfiles(UserProfileFilter filter)
    {
        try
        {
            var users = context.UserProfiles.AsQueryable();
            if (filter.UserName != null)
                users = users.Where(u => u.User.UserName == filter.UserName);
            var result = await (from u in users
                select new GetUserProfileDto()
                {
                    UserName = u.User.UserName,
                    UserId = u.UserId,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Dob = u.Dob,
                    Image = u.Image
                }).Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
            var totalRecord = await users.CountAsync();
            return new PagedResponse<List<GetUserProfileDto>>(result, filter.PageNumber, filter.PageSize, totalRecord);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetUserProfileDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<GetUserProfileDto>> GetUserProfileById([Required]string id)
    {
        try
        {
            var user = await context.UserProfiles.Select(u => new GetUserProfileDto()
            {
                UserName = u.User.UserName!,
                UserId = u.UserId,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Dob = u.Dob,
                Image = u.Image,
                UserRoles = u.IdentityRoles.Select(r => new GetUserRoleDto()
                {
                    Id = r.Id,
                    Name = r.Name!
                }).ToList()
            }).FirstOrDefaultAsync(u => u.UserId == id);
            if (user != null) return new Response<GetUserProfileDto>(user);
            return new Response<GetUserProfileDto>(HttpStatusCode.NotFound, "User not found!");
        }
        catch (Exception e)
        {
            return new Response<GetUserProfileDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> UpdateUserProfileDto(AddUserProfileDto addUserProfile, string userId)
    {
        try
        {
            var user = await context.UserProfiles.FindAsync(userId);
            user!.FirstName = addUserProfile.FirstName;
            user.LastName = addUserProfile.LastName;
            user.Email = addUserProfile.Email;
            user.PhoneNumber = addUserProfile.PhoneNumber;
            user.Dob = addUserProfile.Dob;
            fileService.DeleteFile(user.Image);
            var image = fileService.CreateFile(addUserProfile.Image!);
            user.Image = image.Data!;
            await context.SaveChangesAsync();
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteUser([Required]string id)
    {
        var user = await userManager.FindByIdAsync(id);
        if (user != null)
        {
            await userManager.DeleteAsync(user);
            return new Response<bool>(true);
        }

        return new Response<bool>(HttpStatusCode.NotFound, "User not found!");
    }

    public async Task<Response<bool>> AddOrDeleteRoleFromUser(AddOrRemoveRoleFromUserDto addOrRemoveRoleFromUser)
    {
        try
        {
            var user = await context.Users.FindAsync(addOrRemoveRoleFromUser.UserId);
            if (user == null) return new Response<bool>(HttpStatusCode.NotFound, "User not found!");
            var role = await context.Roles.FindAsync(addOrRemoveRoleFromUser.RoleId);
            if (role == null) return new Response<bool>(HttpStatusCode.NotFound, "Role not found!");
            var userRole = await userManager.IsInRoleAsync(user, role.Name!);
            if (userRole)
            {
                await userManager.RemoveFromRoleAsync(user, role.Name!);
                return new Response<bool>(true);
            }

            await userManager.AddToRoleAsync(user, role.Name!);
            return new Response<bool>(true);
        }
        catch (Exception e)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<List<GetUserRoleDto>>> GetUserRoles()
    {
        try
        {
            var roles = await context.Roles.Select(r => new GetUserRoleDto()
            {
                Id = r.Id,
                Name = r.Name!
            }).ToListAsync();
            return new Response<List<GetUserRoleDto>>(roles);
        }
        catch (Exception e)
        {
            return new Response<List<GetUserRoleDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}