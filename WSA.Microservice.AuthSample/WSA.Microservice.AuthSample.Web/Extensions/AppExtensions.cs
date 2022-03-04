using WSA.Microservice.AuthSample.Web.Middlewares;

namespace WSA.Microservice.AuthSample.Web.Extensions
{
    public static class AppExtensions
    {
        public static void UseSwaggerExtension(this IApplicationBuilder app)
        {
            app.UseSwaggerUI();

            app.UseSwagger(x => x.SerializeAsV2 = true);
        }

        public static void UseErrorHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }

    }
}
