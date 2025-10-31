using IvalsHotel.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace IvalsHotel.Services;

public class AuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<IdentityResult> RegisterUserAsync(User user, string password) => await _userManager.CreateAsync(user, password);

    public async Task<bool> LoginUserAsync(string userName, string password, bool isPersistent = false)
    {
        var result = await _signInManager.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure: false);
        return result.Succeeded;
    }

    public async Task LogoutUserAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<bool> ChangePasswordAsync(User user, string currentPassword, string newPassword)
    {
        var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
        return result.Succeeded;
    }
}
