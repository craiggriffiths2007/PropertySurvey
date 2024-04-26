using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PropertySurveyService.Data;
using PropertySurveyService.Models;
using Microsoft.AspNetCore.Identity;
using PropertySurveyService.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PropertySurveyService.Data.AppDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PropertySurveyServiceContext") ?? throw new InvalidOperationException("Connection string 'PropertySurveyServiceContext' not found.")));

//builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false)
//    .AddEntityFrameworkStores<PropertySurveyService.Data.AppDBContext>();

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;

})
           .AddDefaultTokenProviders()
           .AddDefaultUI()
           .AddRoles<IdentityRole>()
           .AddEntityFrameworkStores<AppDBContext>();

// swagger
builder.Services.AddEndpointsApiExplorer();

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

app.UseSwagger();

app.MapPost("/GetSurveyJobs", (GetSurveysDTO gs, PropertySurveyService.Data.AppDBContext db) =>
{
    List<JobDTO> send_jobs = new List<JobDTO>();

    foreach (var j in db.Job.Where<Job>(x => x.Surveyor.SurveyorCode == gs.SurveyorCode).ToList<Job>())
    {
        Customer? c = db.Customer.FirstOrDefault<Customer>(x => x.CustomerId == j.CustomerId);
        
        if (c == null)
            c = new Customer();

        send_jobs.Add(new JobDTO(j, c));
    }

    return Task.FromResult<IResult>(Results.Ok(send_jobs));
});

app.MapPost("/SendSurveyHeader", (Header survey_header, PropertySurveyService.Data.AppDBContext db) =>
{
    OKRecordDTO return_record = new OKRecordDTO();

    if(survey_header.Id==0)
        db.Add<Header>(survey_header);
    else
        db.Update<Header>(survey_header);
    db.SaveChanges();
    return_record.DBId = survey_header.Id;
    return_record.comments = "Success";

    return Task.FromResult<IResult>(Results.Ok(return_record));
});

app.MapPost("/SendSurveyPanel", (PanelTable survey_panel, PropertySurveyService.Data.AppDBContext db) =>
{
    OKRecordDTO return_record = new OKRecordDTO();

    if(survey_panel.Id==0)
        db.Add<PanelTable>(survey_panel);
    else
        db.Update<PanelTable>(survey_panel);
    db.SaveChanges();
    return_record.comments = "Success";
    return_record.DBId = survey_panel.Id;

    return Task.FromResult<IResult>(Results.Ok(return_record));
});

app.MapPost("/SendSurveyAlum", (AluminiumTable survey_alum, PropertySurveyService.Data.AppDBContext db) =>
{
    OKRecordDTO return_record = new OKRecordDTO();

    if (survey_alum.Id == 0)
        db.Add<AluminiumTable>(survey_alum);
    else
        db.Update<AluminiumTable>(survey_alum);
    db.SaveChanges();
    return_record.comments = "Success";
    return_record.DBId = survey_alum.Id;

    return Task.FromResult<IResult>(Results.Ok(return_record));
});

app.MapPost("/SendSurveyBifold", (BifoldTable survey_bifold, PropertySurveyService.Data.AppDBContext db) =>
{
    OKRecordDTO return_record = new OKRecordDTO();

    if (survey_bifold.Id == 0)
        db.Add<BifoldTable>(survey_bifold);
    else
        db.Update<BifoldTable>(survey_bifold);
    db.SaveChanges();
    return_record.comments = "Success";
    return_record.DBId = survey_bifold.Id;

    return Task.FromResult<IResult>(Results.Ok(return_record));
});

app.MapPost("/SendSurveyComp", (CompositeTable survey_comp, PropertySurveyService.Data.AppDBContext db) =>
{
    OKRecordDTO return_record = new OKRecordDTO();

    if (survey_comp.Id == 0)
        db.Add<CompositeTable>(survey_comp);
    else
        db.Update<CompositeTable>(survey_comp);
    db.SaveChanges();
    return_record.comments = "Success";
    return_record.DBId = survey_comp.Id;

    return Task.FromResult<IResult>(Results.Ok(return_record));
});

app.MapPost("/SendSurveyCons", (ConsTable survey_cons, PropertySurveyService.Data.AppDBContext db) =>
{
    OKRecordDTO return_record = new OKRecordDTO();

    if (survey_cons.Id == 0)
        db.Add<ConsTable>(survey_cons);
    else
        db.Update<ConsTable>(survey_cons);
    db.SaveChanges();
    return_record.comments = "Success";
    return_record.DBId = survey_cons.Id;

    return Task.FromResult<IResult>(Results.Ok(return_record));
});

app.MapPost("/SendSurveyGar", (GarageTable survey_gar, PropertySurveyService.Data.AppDBContext db) =>
{
    OKRecordDTO return_record = new OKRecordDTO();

    if (survey_gar.Id == 0)
        db.Add<GarageTable>(survey_gar);
    else
        db.Update<GarageTable>(survey_gar);
    db.SaveChanges();
    return_record.comments = "Success";
    return_record.DBId = survey_gar.Id;

    return Task.FromResult<IResult>(Results.Ok(return_record));
});

app.MapPost("/SendSurveyGlass", (GlassTable survey_glass, PropertySurveyService.Data.AppDBContext db) =>
{
    OKRecordDTO return_record = new OKRecordDTO();

    if (survey_glass.Id == 0)
        db.Add<GlassTable>(survey_glass);
    else
        db.Update<GlassTable>(survey_glass);
    db.SaveChanges();
    return_record.comments = "Success";
    return_record.DBId = survey_glass.Id;

    return Task.FromResult<IResult>(Results.Ok(return_record));
});

app.MapPost("/SendSurveyGreen", (GreenTable survey_green, PropertySurveyService.Data.AppDBContext db) =>
{
    OKRecordDTO return_record = new OKRecordDTO();

    if (survey_green.Id == 0)
        db.Add<GreenTable>(survey_green);
    else
        db.Update<GreenTable>(survey_green);
    db.SaveChanges();
    return_record.comments = "Success";
    return_record.DBId = survey_green.Id;

    return Task.FromResult<IResult>(Results.Ok(return_record));
});

app.MapPost("/SendSurveyLock", (LockingTable survey_lock, PropertySurveyService.Data.AppDBContext db) =>
{
    OKRecordDTO return_record = new OKRecordDTO();

    if (survey_lock.Id == 0)
        db.Add<LockingTable>(survey_lock);
    else
        db.Update<LockingTable>(survey_lock);
    db.SaveChanges();
    return_record.comments = "Success";
    return_record.DBId = survey_lock.Id;

    return Task.FromResult<IResult>(Results.Ok(return_record));
});

app.MapPost("/SendSurveyTimber", (TimberTable survey_timb, PropertySurveyService.Data.AppDBContext db) =>
{
    OKRecordDTO return_record = new OKRecordDTO();

    if (survey_timb.Id == 0)
        db.Add<TimberTable>(survey_timb);
    else
        db.Update<TimberTable>(survey_timb);
    db.SaveChanges();
    return_record.comments = "Success";
    return_record.DBId = survey_timb.Id;

    return Task.FromResult<IResult>(Results.Ok(return_record));
});

app.MapPost("/SendSurveyUPVC", (UPVCTable survey_upvc, PropertySurveyService.Data.AppDBContext db) =>
{
    OKRecordDTO return_record = new OKRecordDTO();

    if (survey_upvc.Id == 0)
        db.Add<UPVCTable>(survey_upvc);
    else
        db.Update<UPVCTable>(survey_upvc);
    db.SaveChanges();
    return_record.comments = "Success";
    return_record.DBId = survey_upvc.Id;

    return Task.FromResult<IResult>(Results.Ok(return_record));
});

app.MapPost("/SendSurveyImage", (ImageDTO imageDTO, PropertySurveyService.Data.AppDBContext db) =>
{
    OKRecordDTO return_record = new OKRecordDTO();

    PhotoImage image = new PhotoImage();

    image.Filename = imageDTO.Filename;
    image.Data = imageDTO.Data;
    image.DateTime = DateTime.Now;
    image.ContractCode = imageDTO.Filename.Substring(0, 8);

    if (db.Images.Where<PhotoImage>(x => x.Filename == image.Filename).Count<PhotoImage>()==0)
        db.Add<PhotoImage>(image);

    db.SaveChanges();
    return_record.comments = "Success";

    return Task.FromResult<IResult>(Results.Ok(return_record));
});
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

// Seeding user roles
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<AppDBContext>();
        var userManager = services.GetRequiredService<UserManager<AppUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        await DbInitializer.SeedRolesAsync(userManager, roleManager);
        await DbInitializer.SeedSuperAdminAsync(userManager, roleManager);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

app.UseSwaggerUI();

app.Run();
