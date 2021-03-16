using MediatR;
using Microsoft.Extensions.DependencyInjection;
using StudentManagement.Application;
using StudentManagement.Application.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagement.Api.Registration
{
    public static class ValidatorRegistration
    {
        public static void AddValidators(this IServiceCollection services)
        {
            var validatorTypes = GetValidatorTypes();

            foreach (var validatorType in validatorTypes)
            {
                var validatorInterfaceType = GetValidatorInterfaceType(validatorType);
                services.AddTransient(validatorInterfaceType, validatorType);

                var requestType = GetRequestType(validatorInterfaceType);
                var responseType = GetResponseType(requestType);
                var resultType = GetResultType(responseType);

                var pipelineBehaviourType = typeof(IPipelineBehavior<,>)
                    .MakeGenericType(requestType, responseType);
                var validationMiddlewareType = typeof(ValidationMiddleware<,>)
                    .MakeGenericType(requestType, resultType);

                services.AddTransient(pipelineBehaviourType, validationMiddlewareType);
            }
        }

        private static Type GetResultType(Type responseType)
        {
            return responseType.GetGenericArguments().First();
        }

        private static IEnumerable<Type> GetValidatorTypes()
        {
            return typeof(IValidator<>).Assembly
                .GetTypes()
                .Where(x =>
                {
                    return !x.IsAbstract
                        && x.GetInterfaces().Any(y =>
                        {
                            return y.IsGenericType &&
                                y.GetGenericTypeDefinition() == typeof(IValidator<>);
                        });
                });
        }

        private static Type GetValidatorInterfaceType(Type validatorType)
        {
            var validatorInterfaceType = validatorType
                .GetInterfaces()
                .Where(x => x.IsGenericType &&
                            x.GetGenericTypeDefinition() == typeof(IValidator<>))
                .First();

            return validatorInterfaceType;
        }

        private static Type GetRequestType(Type validatorInterfaceType)
        {
            return validatorInterfaceType.GetGenericArguments().First();
        }

        private static Type GetResponseType(Type requestType)
        {
            return requestType
                .GetInterfaces()
                .Where(x => x.IsGenericType
                            && x.GetGenericTypeDefinition() == typeof(IRequest<>))
                .Select(x => x.GetGenericArguments().First())
                .First();
        }
    }
}
