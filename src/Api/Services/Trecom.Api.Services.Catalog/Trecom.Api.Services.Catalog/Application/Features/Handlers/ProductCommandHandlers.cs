using AutoMapper;
using Elastic.Clients.Elasticsearch;
using MassTransit;
using MediatR;
using Trecom.Api.Services.Catalog.Application.Events;
using Trecom.Api.Services.Catalog.Application.Features.Commands;
using Trecom.Api.Services.Catalog.Constants;
using Trecom.Api.Services.Catalog.Extensions;
using Trecom.Api.Services.Catalog.Models.Dtos;
using Trecom.Api.Services.Catalog.Models.Entities;
using Trecom.Api.Services.Catalog.Persistance.EntityFramework;
using Trecom.Shared;
using Trecom.Shared.CCS.GlobalException;

namespace Trecom.Api.Services.Catalog.Application.Features.Handlers;

public class ProductCommandHandlers :
    IRequestHandler<CreateProductCommand, CreateProductResponseDto>
{
    //private readonly AppDbContext dbContext;
    private readonly ILogger<ProductCommandHandlers> logger;
    private readonly IMapper mapper;
    private readonly ElasticsearchClient client;
    private readonly ISendEndpointProvider sendEndpointProvider;

    public ProductCommandHandlers(ILogger<ProductCommandHandlers> logger, IMapper mapper, ElasticsearchClient client, ISendEndpointProvider sendEndpointProvider)
    {
        this.logger = logger;
        this.mapper = mapper;
        this.client = client;
        this.sendEndpointProvider = sendEndpointProvider;
    }

    public async Task<CreateProductResponseDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        Product toBeCreatedProduct = mapper.Map<Product>(request.CreateProductDto);

        toBeCreatedProduct.NullBusinessValidation();

        var result = await client.IndexAsync(toBeCreatedProduct, x => x.Index("catalog").Id(toBeCreatedProduct.Id));

        if (!result.IsValidResponse)
            throw new BusinessException($"{request.CreateProductDto.Name} cannot be added to Db");

        try
        {
            var endpoint =
                await sendEndpointProvider.GetSendEndpoint(
                    new Uri($"queue:{RabbitMqSettings.UpdateBrandAndSupplierForCreateProductEvent}"));

            await endpoint.Send(new UpdateRelatedPropsForCreateProductEvent(toBeCreatedProduct.BrandId, toBeCreatedProduct.SupplierId, toBeCreatedProduct.Id.ToString(),Guid.NewGuid()));

        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while sending message to RabbitMq");
            throw;
        }
        return mapper.Map<CreateProductResponseDto>(toBeCreatedProduct);
    }
}