using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TApi.WebApp.Features.Items.Domain;
using TApi.WebApp.Features.Items.Http;

namespace TApi.WebApp.Endpoints;

internal static class TestEndpoints
{
    internal static void MapTestEndpoints(this RouteGroupBuilder group)
    {
        group.RequireAuthorization();
        group.MapGet("items", GetTestItems);
        group.MapPost("items/validate", ValidateComplexItem);
    }

    private static Results<Ok<ItemListDto>, ValidationProblem> GetTestItems(
        [Range(1, 2)]
        int page = 1)
    {
        return TypedResults.Ok(new ItemListDto
        {
            Items =
            [
                new() { Name = $"Item {(page - 1) * 3 + 1}", Description = $"Description of Item {(page - 1) * 3 + 1}." },
                new() { Name = $"Item {(page - 1) * 3 + 2}", Description = $"Description of Item {(page - 1) * 3 + 2}." },
                new() { Name = $"Item {(page - 1) * 3 + 3}", Description = $"Description of Item {(page - 1) * 3 + 3}." }
            ]
        });
    }

    private static Results<Ok, ValidationProblem> ValidateComplexItem([FromBody]ComplexItemDto dto)
    {
        if (dto.Type == ItemType.NamedItem && string.IsNullOrWhiteSpace(dto.Name))
        {
            return TypedResults.ValidationProblem(new Dictionary<string, string[]>
            {
               { nameof(ItemDto.Name), ["The name value is required for named items."] } 
            });
        }
        
        if (dto.Type == ItemType.FullItem && (string.IsNullOrWhiteSpace(dto.Name) || string.IsNullOrWhiteSpace(dto.Description)))
        {
            var errors = new Dictionary<string, string[]>();

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
               errors.Add(nameof(ItemDto.Name), ["The name value is required for full items."]) ;
            }

            if (string.IsNullOrWhiteSpace(dto.Description))
            {
               errors.Add(nameof(ItemDto.Description), ["The description value is required for full items."]) ;
            }

            return TypedResults.ValidationProblem(errors);
        }

        return TypedResults.Ok();
    }
}
