using System.Security.Claims;
using Fireworks.Api.Configurations;
using Fireworks.Api.Configurations.ServiceRegistrations;
using Fireworks.Api.Endpoints;
using Fireworks.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddApplicationServices(builder.Configuration);


if (builder.Environment.EnvironmentName != "Testing")
{
    builder.Host.UseLoggingServices(builder.Configuration);
}

var app = builder.Build();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseSwaggerDocumentation();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapUserEndpoints();
app.MapRoleEndpoints();
app.MapUserRolesEndpoints();
app.MapLoginEndpoints();
// using (var scope = app.Services.CreateScope())
// {
//     var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
//     var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
//
//     // 创建Admin角色
//     if (!await roleManager.RoleExistsAsync("Admin"))
//     {
//         await roleManager.CreateAsync(
//             new ApplicationRole { Name = "Admin" }
//         );
//     }
//
//     // 创建初始管理员用户
//     var adminUser = await userManager.FindByEmailAsync("admin@example.com");
//     if (adminUser == null)
//     {
//         adminUser = new ApplicationUser { UserName = "admin", Email = "admin@example.com" };
//         await userManager.CreateAsync(adminUser, "123456");
//         await userManager.AddToRoleAsync(adminUser, "Admin");
//     }
// }

app.Run();