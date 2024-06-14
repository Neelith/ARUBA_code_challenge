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
            group.MapDelete("/{id}", DeleteProcedureAsync);

            return group;
        }

        public static async Task<Procedure> GetProcedureByIdAsync(
            [FromServices] ISender sender, 
            [FromBody] GetProcedureQuery query)
        {
            return await sender.Send(query);
        }

        public static async Task<Procedure> CreateProcedureAsync(
            [FromServices] ISender sender, 
            [FromBody] CreateProcedureCommand createProcedureCommand)
        {
            return await sender.Send(createProcedureCommand);
        }

        public static async Task<IResult> UpdateProcedureAsync(ISender sender, int id)
        {
            throw new NotImplementedException();
        }

        public static async Task<IResult> DeleteProcedureAsync(ISender sender, int id)
        {
            throw new NotImplementedException();
        }
    }
}
