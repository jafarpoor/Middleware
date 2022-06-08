var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

////ex

//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("Before Invoke from 1st app.Use()\n");
//    await next();
//    await context.Response.WriteAsync("After Invoke from 1st app.Use()\n");
//});

//app.Use(async (context, next) =>
//{
//    await context.Response.WriteAsync("Before Invoke from 2nd app.Use()\n");
//    await next();
//    await context.Response.WriteAsync("After Invoke from 2nd app.Use()\n");
//});

//app.Run(async (context) =>
//{
//    await context.Response.WriteAsync("Hello from 1st app.Run()\n");
//});

//// the following will never be executed    
//app.Run(async (context) =>
//{
//    await context.Response.WriteAsync("Hello from 2nd app.Run()\n");
//});

///// end ex


//map
app.Map("/m1", HandleMapOne);

app.Map("/m2", appMap => {
    appMap.Run(async context =>
    {
        await context.Response.WriteAsync("Hello from 2nd app.Map()");
    });
});
app.Run(async (context) =>
{
    await context.Response.WriteAsync("Hello from app.Run()");
});

void HandleMapOne(IApplicationBuilder obj)
{
    app.Run(async context =>
    {
        await context.Response.WriteAsync("Hello from 1st app.Map()");
    });
}

//end map


//app.UseMyMiddleware();
app.Run();