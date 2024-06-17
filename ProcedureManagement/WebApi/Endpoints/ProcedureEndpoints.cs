using Application.Procedures.Commands;
using Application.Procedures.Queries;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Endpoints
{
    public static class ProcedureEndpoints
    {
        internal static RouteGroupBuilder MapProcedureEndpoints(this RouteGroupBuilder group)
        {
            //Disabled antiforgery because it is enabled by default in minimal APIs and threw some exceptions
            //but I don't need it since we don't have auth cookies in this case.
            //Of course this is an example project and it's not production ready.
            group.MapGet("/", GetProcedureByIdAsync);
            group.MapPost("/", CreateProcedureAsync)
                .DisableAntiforgery();
            group.MapPut("/", UpdateProcedureAsync)
                .DisableAntiforgery();
            group.MapDelete("/", DeleteProcedureAsync);

            return group;
        }

        public static async Task<Procedure> GetProcedureByIdAsync(
            [FromServices] ISender sender,
            [FromBody] GetProcedureByIdQuery query)
        {
            return await sender.Send(query);
        }

        public static async Task<Procedure> CreateProcedureAsync(
            [FromServices] ISender sender,
            [FromForm] CreateProcedureCommand createProcedureCommand)
        {
            return await sender.Send(createProcedureCommand);
        }

        public static async Task<IResult> UpdateProcedureAsync(
            [FromServices] ISender sender,
            [FromForm] UpdateProcedureCommand updateProcedureCommand)
        {
            await sender.Send(updateProcedureCommand);
            return Results.NoContent();
        }

        public static async Task<IResult> DeleteProcedureAsync(
            [FromServices] ISender sender,
            [FromBody] DeleteProcedureCommand deleteProcedureCommand)
        {
            await sender.Send(deleteProcedureCommand);
            return Results.NoContent();
        }
    }
}
