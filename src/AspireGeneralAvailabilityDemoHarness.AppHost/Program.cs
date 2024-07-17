var builder = DistributedApplication.CreateBuilder(args);

var ollama = builder.AddOllama();
builder.AddRaygun().WithReference(ollama);

builder.AddProject<Projects.AspireGeneralAvailabilityDemoHarness_Web>("webfrontend")
    .WithExternalHttpEndpoints();

builder.Build().Run();
