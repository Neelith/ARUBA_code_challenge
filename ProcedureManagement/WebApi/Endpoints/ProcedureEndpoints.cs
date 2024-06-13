using MediatR;

namespace WebApi.Endpoints
{
    public static class ProcedureEndpoints
    {
        internal static RouteGroupBuilder MapProcedureEndpoints(this RouteGroupBuilder group)
        {
            group.MapGet("/{id}", GetProcedureByIdAsync);
            group.MapPost("/", CreateProcedureAsync);
            group.MapPut("/", UpdateProcedureAsync);
            group.MapDelete("/{id}", DeleteProcedureAsync);

            return group;
        }

        public static async Task<IResult> GetProcedureByIdAsync(ISender sender, int id)
        {
            throw new NotImplementedException();
        }

        public static async Task<IResult> CreateProcedureAsync(ISender sender, int id)
        {
            throw new NotImplementedException();
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
