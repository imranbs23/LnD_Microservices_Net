using Play.Common.MongoDb;
using Play.Inventory.Service.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddMongo()
                .AddMongoRepository<InventoryItem>("inventoryItems");

builder.Services.AddControllers(options =>{
    options.SuppressAsyncSuffixInActionNames = false;
}); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Add this line
});

app.Run();

