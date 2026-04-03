using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TApi.WebApp.Features.Items.Http;

namespace TApi.WebApp.Endpoints;

internal static class TestEndpoints
{
    internal static void MapTestEndpoints(this RouteGroupBuilder group)
    {
        group.MapGet("items", GetTestItems);
    }

    private static Results<Ok<ItemListDto>, NotFound<ProblemDetails>, ValidationProblem> GetTestItems(
        [Range(1, 2)]
        int page = 1,
        bool simulateNotFound = false)
    {
        if (simulateNotFound)
        {
            return TypedResults.NotFound(new ProblemDetails
            {
                Title = "Simulated Not Found",
                Detail = "A simulated not found error.",
                Status = StatusCodes.Status404NotFound
            });
        }

        return TypedResults.Ok(new ItemListDto
        {
            Items =
            [
                new() { Name = "Item 1", Description = "Description of Item 1." },
                new() { Name = "Item 2", Description = "Description of Item 2." },
                new() { Name = "Item 3", Description = "Description of Item 3." }
            ]
        });
    }
}
