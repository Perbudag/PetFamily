using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Shared.Models;
using System.Linq.Expressions;

namespace PetFamily.Infrastructure.Extensions
{
    internal static class FluentApiExtensions
    {
        public static EntityTypeBuilder<TEntity> ValueObjectListToJson<TEntity, TValueEntity>(
            this EntityTypeBuilder<TEntity> builder,
            Expression<Func<TEntity, ValueObjectList<TValueEntity>?>> navigationExpression,
            Action<OwnedNavigationBuilder<ValueObjectList<TValueEntity>, TValueEntity>> buildAction
            )
            where TEntity : class
            where TValueEntity : class
        {
            return builder.OwnsOne(navigationExpression, navigationBuilder =>
            {
                navigationBuilder.ToJson();

                navigationBuilder.OwnsMany(x => x.Values, buildAction);
            });
        }
    }
}
