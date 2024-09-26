using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

HashSet<Person> personsDb = [];

app.MapDelete("/persons", () => personsDb).WithName("GetPersons").WithOpenApi();

app.MapMethods(
    "/persons",
    ["GET", "POST", "PUT", "PATCH"],
    ([FromBody] Person person) =>
    {
        personsDb.Add(person);

        return TypedResults.Ok(person);
    }
);

app.Run();

record Person(string Name) { }
