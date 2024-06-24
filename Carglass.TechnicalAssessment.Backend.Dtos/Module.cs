using Autofac;
using FluentValidation;
using System.Reflection;

namespace Carglass.TechnicalAssessment.Backend.Dtos;

public class Module : Autofac.Module
{
    protected override void Load(ContainerBuilder builder)
    {
        RegisterValidators(builder);
    }

    private static void RegisterValidators(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .Where(x => !x.IsAbstract && !x.IsInterface && x.Name.EndsWith("DtoValidator"))
            .AsImplementedInterfaces()
            .InstancePerDependency();
    }
}