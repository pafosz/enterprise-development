var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServer("bikerental-sql-server")
                 .AddDatabase("BikeRentalDb");

var rabbitMqQueue = builder.AddParameter("RabbitMQQueue");
var rabbitUserName = builder.AddParameter("RabbitMQLogin");
var rabbitPassword = builder.AddParameter("RabbitMQPassword");
var rabbitMq = builder.AddRabbitMQ("bike-rental-rabbitmq", userName: rabbitUserName, password: rabbitPassword)
    .WithManagementPlugin();

builder.AddProject<Projects.BikeRental_Api_Host>("bikerental-api-host")
       .WithReference(sqlServer, "DatabaseConnection")
       .WithReference(rabbitMq)
        .WithEnvironment("RabbitMq:QueueName", rabbitMqQueue)
        .WaitFor(sqlServer)
        .WaitFor(rabbitMq);

builder.AddProject<Projects.BikeRental_Generator_RabbitMq_Host>("bikerental-generator-rabbitmq-host")
    .WithReference(rabbitMq)
    .WaitFor(rabbitMq)
    .WithEnvironment("RabbitMq:QueueName", rabbitMqQueue);

builder.Build().Run();
