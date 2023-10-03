using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<OrderConsumer>();
    config.UsingRabbitMq((ctx, config) =>
    {
        config.Host("amqp://razmik:Q!w2e3r4t5@localhost:5672");
        
        config.ReceiveEndpoint("order-queue", c => {
            c.ConfigureConsumer<OrderConsumer>(ctx);
        });
    });
});

builder.Services.AddMassTransitHostedService();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
