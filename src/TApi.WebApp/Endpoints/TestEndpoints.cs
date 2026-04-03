using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TApi.WebApp.Features.Items.Http;

namespace TApi.WebApp.Endpoints;

internal static class TestEndpoints
{
    internal static void MapTestEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("items", GetTestItems).RequireAuthorization();
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
}
