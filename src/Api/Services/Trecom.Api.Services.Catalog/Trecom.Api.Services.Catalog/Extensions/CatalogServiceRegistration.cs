using System.Reflection;
using Elastic.Clients.Elasticsearch;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Trecom.Api.Services.Catalog.Application.Features.Validators;
using Trecom.Api.Services.Catalog.Constants;
using Trecom.Api.Services.Catalog.Persistance;
using Trecom.Api.Services.Catalog.Persistance.DataSeeding;
using Trecom.Api.Services.Catalog.Persistance.Elasticsearch.Repository;
using Trecom.Api.Services.Catalog.Persistance.EntityFramework;
using Trecom.Shared.Pipelines;
using Trecom.Shared.Pipelines.Catching;
using Trecom.Shared.Pipelines.Logging;

namespace Trecom.Api.Services.Catalog.Extensions;

public static class CatalogServiceRegistration
{
    public static void RegisterRequiredServices(this IServiceCollection services, IConfiguration configuration)
    {
            
        services.Configure<ElasticIndexSettings>(configuration.GetSection("ElasticIndexSettings"));

        services.AddMassTransitServices(configuration);

        services.AddScoped<ProductElasticRepository>();
        services.AddScoped<BrandElasticRepository>();  
        services.AddScoped<CategoryElasticRepository>();
        services.AddTransient(typeof(IPipelineBehavior<,>), (typeof(ValidationPipelineBehavior<,>)));
        services.AddTransient(typeof(IPipelineBehavior<,>), (typeof(LoggingPipelineBehavior<,>)));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingPipelineBehavior<,>));

        services.Configure<CacheSettings>(configuration.GetSection("CacheSettings"));

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(x =>
        {
            x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        services.AddValidatorsFromAssemblyContaining<CreateProductValidator>(ServiceLifetime.Scoped);

        var settings = new ElasticsearchClientSettings(new Uri(configuration.GetSection("ElasticSettings")["Url"]!));

        var client = new ElasticsearchClient(settings);

        services.AddSingleton(client);
        services.AddDbContext<AppDbContext>(x => x.UseSqlServer(configuration.GetConnectionString("SqlCon")));

    }
}