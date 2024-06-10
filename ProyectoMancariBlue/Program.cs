var builder = WebApplication.CreateBuilder(args);

// Agregar servicios al contenedor.
builder.Services.AddControllersWithViews();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Configurar DbContext para MySQL
builder.Services.AddDbContext<DBContext>(options =>
    options.UseMySql(
        connectionString: connectionString,
        ServerVersion.AutoDetect(connectionString)
    ));


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateAudience = true,
		ValidAudience = "domain.com", // NOTE: USE THE REAL DOMAIN NAME
		ValidateIssuer = true,
		ValidIssuer = "domain.com", // NOTE: USE THE REAL DOMAIN NAME
		ValidateLifetime = true,
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("396B5DD9-CC75-411C-9311-5B6E1F391B89")) // NOTE: THIS SHOULD BE A SECRET KEY NOT TO BE SHARED; REPLACE THIS GUID WITH A UNIQUE ONE
	};
});

builder.Services.AddHttpClient();

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}"
);

app.Run();
