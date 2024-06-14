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
            group.MapGet("/", GetProcedureByIdAsync);
            group.MapPost("/", CreateProcedureAsync);
            group.MapPut("/", UpdateProcedureAsync);
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
            [FromBody] CreateProcedureCommand createProcedureCommand)
        {
            return await sender.Send(createProcedureCommand);
        }

        public static async Task<IResult> UpdateProcedureAsync(
            [FromServices] ISender sender,
            [FromBody] UpdateProcedureCommand updateProcedureCommand)
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
